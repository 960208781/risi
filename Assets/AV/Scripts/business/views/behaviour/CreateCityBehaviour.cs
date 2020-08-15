using UnityEngine;
using System.Collections;
using ZGGame;
using System.Collections.Generic;
using message;
using System;
using LitJson;
using System.Linq;
using UnityEngine.UI;


public class CreateCityBehaviour : MonoBehaviour
{
    public Transform parentObj;
    public Transform cameraTran;
    private GCAroundInfo gcAroundInfo;


    public GameObject boxItem;
    /// <summary>
    /// 缩放系数
    /// </summary>
    private float scale;

    private bool _isOpen = true;

    private List<Transform> listgo = new List<Transform>();
    // Use this for initialization
    void Start()
    {

    }

    private void removeAll(int type, object data = null)
    {
        _isOpen = false;
        foreach (Transform tran in listgo)
        {
            tran.gameObject.SetActive(false);
        }
    }

    private void showAll(int type, object data = null)
    {
        _isOpen = true;
        foreach (Transform tran in listgo)
        {
            tran.gameObject.SetActive(true);
        }
    }

    public static List<NewViewObjScript> listObjScript = new List<NewViewObjScript>();
    public void initAllData(GCAroundInfo _gcAroundInfo)
    {
        listObjScript.Clear();

        Clear();

        gcAroundInfo = _gcAroundInfo;

        refreshData();
    }

    public void refreshData()
    {
        //获取缩放系数
        getScale();

        foreach (Transform go in listgo)
        {
            if (go.gameObject.activeSelf)
            {
                GameObject.Destroy(go.gameObject);
            }
        }

        listgo.Clear();

        int len = gcAroundInfo.poiData.Count;
        for (int i = len - 1; i >= 0; i--)
        {
            POIData me = gcAroundInfo.poiData[i];
            CreateBoxItem(me);
        }

        BoxSort();

        foreach (var item in listgo)
        {
            item.gameObject.SetActive(false);
        }
    }


    public void CreateBoxItem(POIData poi)
    {
        var box = Instantiate(boxItem);
        box.SetActive(true);
        box.transform.SetParent(transform);

        box.transform.localPosition = Vector3.zero;
        var m = poi;
        Vector3 vecn = new Vector3(float.Parse(m.location.longitude) - float.Parse(gcAroundInfo.mapLocation.longitude), 0, float.Parse(m.location.latitude) - float.Parse(gcAroundInfo.mapLocation.latitude)).normalized;
        Transform tran = box.transform;
        tran.Translate(vecn * 150f);
        tran.localPosition = new Vector3(tran.localPosition.x, 0, tran.localPosition.z);

        var item = box.GetComponent<BoxItem>();
        item.data = poi;
        item.Set();
        SetBoxNormal(item);

        item.OnSelect = SelectChild;
        boxList.Add(box);
        stack.Push(box.transform);
    }

    private BoxItem lastgo;
    public void SelectChild(BoxItem go)
    {
        var selectcolor = new Color(226f / 255f, 246f / 255f, 224f / 255f);
        var normalcolor = new Color(28f / 255f, 43f / 255f, 55f / 255f);
        var normalcitycolor = new Color(1f, 1f, 1f);
        var citycolor = new Color(0, 0, 0);
        if (lastgo)
        {
            var bg = lastgo.bg.GetComponent<Image>();
            bg.color = normalcolor;
            lastgo.attrName.GetComponent<Text>().color = normalcitycolor;
            lastgo.distance.GetComponent<Text>().color = normalcitycolor;
        }
        if (go)
        {
            var sbg = go.bg.GetComponent<Image>();
            sbg.color = selectcolor;
            go.attrName.GetComponent<Text>().color = citycolor;
            go.distance.GetComponent<Text>().color = citycolor;

            var mo = go.data;
            RadarUI.instance.toSelect(mo);

            CallNative.InvokeNative(CallNative.showPoiDetail, mo.desc);


        }
        else
        {
            CallNative.InvokeNative(CallNative.showPoiDetail, "");
        }
        lastgo = go;
    }

    public void SetBoxNormal(BoxItem go)
    {
        var normalcolor = new Color(28f / 255f, 43f / 255f, 55f / 255f);
        var normalcitycolor = new Color(1f, 1f, 1f);
        go.bg.GetComponent<Image>().color = normalcolor;
        go.attrName.GetComponent<Text>().color = normalcitycolor;
        go.distance.GetComponent<Text>().color = normalcitycolor;
    }

    public void SetBoxSelect(BoxItem go)
    {
        var selectcolor = new Color(226f / 255f, 246f / 255f, 224f / 255f);
        var citycolor = new Color(0, 0, 0);
        var sbg = go.bg.GetComponent<Image>();
        sbg.color = selectcolor;
        go.attrName.GetComponent<Text>().color = citycolor;
        go.distance.GetComponent<Text>().color = citycolor;
    }

    public void showPoiDetail(string id)
    {
        var item = boxList.Find(x => x.GetComponent<BoxItem>().data.id.Equals(id));
        Debug.LogError("DESC:>>>>>>>>" + item.GetComponent<BoxItem>().data.desc);
        if (item != null)
        {
            var mo = item.GetComponent<BoxItem>();
            CallNative.InvokeNative(CallNative.showPoiDetail, mo.data.desc);
        }

    }

    private List<GameObject> boxList = new List<GameObject>();
    private Stack<Transform> stack = new Stack<Transform>();
    private Stack<Transform> stackTemp = new Stack<Transform>();
    private List<List<Transform>> buffer = new List<List<Transform>>();

    public void Clear()
    {
        for (int i = 0; i < boxList.Count; i++)
        {
            Destroy(boxList[i]);
        }

        boxList.Clear();
        stack.Clear();
        stackTemp.Clear();
        buffer.Clear();

    }
    public void BoxSort()
    {
        while (true)
        {
            var box = stack.Pop();
            var blist = new List<Transform>();
            while (stack.Count > 0)
            {
                var box2 = stack.Pop();
                if (isContact(box, box2))
                {
                    blist.Add(box2);
                }
                else
                {
                    stackTemp.Push(box2);
                }
            }
            if (blist.Count > 0)
                buffer.Add(blist);
            if (stackTemp.Count <= 0) break;
            stack = stackTemp;
            stackTemp = new Stack<Transform>();
        }

        foreach (var b in buffer)
        {
            b.Sort((x, y) => disCompare(x, y));

            for (int i = 0; i < b.Count; i++)
            {
                var item = b[i];
                var pos = new Vector3(0, (i + 1) * 13, 0);
                item.localPosition = item.localPosition + pos;
            }
        }
    }

    public static int disCompare(Transform t1, Transform t2)
    {
        return t1.GetComponent<BoxItem>().data.distance.CompareTo(t2.GetComponent<BoxItem>().data.distance);
    }

    public bool isContact(Transform b1, Transform b2)
    {
        Vector3 ver = b2.localPosition;
        Vector3 ver1 = b1.localPosition;
        var x = 60f; //30f;
        var z = 60f;// 45;
        var y = 8f;//6;
        if ((ver.x > ver1.x - x && ver.x < ver1.x + x) && (ver.z > ver1.z - z && ver.z < ver1.z + z))
        {
            if (ver1.y > ver.y - y && ver1.y < ver.y + y)
            {
                return true;
            }
        }
        //return Vector3.Distance(ver, ver1) < 120;
        return false;
    }

    /// <summary>
    /// 获取缩放系数
    /// </summary>
    private void getScale()
    {
        int len = gcAroundInfo.poiData.Count;
        //获取缩放系数
        for (int i = len - 1; i >= 0; i--)
        {
            POIData me = gcAroundInfo.poiData[i];
            //创建UI
            int distence = me.distance;
            //确定缩放系数
            float dis = Vector3.Distance(new Vector3(float.Parse(me.location.longitude), float.Parse(me.location.latitude), 0f), new Vector3(float.Parse(gcAroundInfo.mapLocation.longitude), float.Parse(gcAroundInfo.mapLocation.latitude), 0));
            scale = distence / dis;
            return;
        }
    }

    // /// <summary>
    // /// 碰撞检测
    // /// </summary>
    // private void addCollider(Transform tran)
    // {
    //     if (listgo.Count == 0)
    //     {
    //         return;
    //     }

    //     while (true)
    //     {
    //         if (findCollider(tran))
    //         {
    //             tran.position = new Vector3(tran.position.x, tran.position.y + 13, tran.position.z);
    //             continue;
    //         }
    //         break;
    //     }

    // }

    // private bool findCollider(Transform tran)
    // {

    //     foreach (Transform transform in listgo)
    //     {
    //         Vector3 ver = transform.position;
    //         Vector3 ver1 = tran.position;
    //         if ((ver.x > ver1.x - 30 && ver.x < ver1.x + 30) && (ver.z > ver1.z - 45 && ver.z < ver1.z + 45))
    //         {
    //             if (ver1.y > ver.y - 6 && ver1.y < ver.y + 6)
    //             {
    //                 return true;
    //             }
    //         }
    //     }
    //     return false;
    // }

    int frame;
    void Update()
    {
        // foreach (Transform go in listgo)
        // {
        //     go.eulerAngles = new Vector3(cameraTran.eulerAngles.x, cameraTran.eulerAngles.y, cameraTran.eulerAngles.z);
        // }

        for (int i = 0; i < boxList.Count; i++)
        {
            var item = boxList[i];
            item.transform.eulerAngles = new Vector3(cameraTran.eulerAngles.x, cameraTran.eulerAngles.y, cameraTran.eulerAngles.z);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (lastgo)
            {
                SelectChild(null);
                CallNative.InvokeNative(CallNative.showPoiDetail, "");
            }
        }

    }


}
