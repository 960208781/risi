using message;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using ZGGame;
public class RadarUI : MonoBehaviour
{
    public Transform bg;
    public Transform group;
    public Transform arrayTrans;
    public Transform arrayContent;
    public Transform arr;
    public GameObject pointgo;

    private static RadarUI _radarUI;

    public static RadarUI instance
    {
        get
        {
            return _radarUI;
        }
    }

    void Awake()
    {
        _radarUI = this;
    }
    void Start()
    {
        EventLisenter.Get(bg.gameObject).OnClick = clickBg;
    }

    private Dictionary<POIData, GameObject> dicGo = new Dictionary<POIData, GameObject>();

    public void toSelect(POIData sd)
    {
        if (!dicGo.ContainsKey(sd))
        {
            return;
        }
        foreach (KeyValuePair<POIData, GameObject> keyvalue in dicGo)
        {
            Image img = keyvalue.Value.GetComponent<Image>();
            if (img == null)
            {
                continue;
            }
            if (keyvalue.Key == sd)
            {
                img.color = Color.red;
            }
            else
            {
                img.color = Color.white;
            }
        }
    }

    public void initAllData(GCAroundInfo gr)
    {
        foreach (KeyValuePair<POIData, GameObject> keyvalue in dicGo)
        {
            GameObject.Destroy(keyvalue.Value);
        }

        dicGo.Clear();

        //求出最大的精度
        float maxLa = 0f;
        float maxlo = 0f;

        foreach (POIData pd in gr.poiData)
        {
            float la = Math.Abs(float.Parse(pd.location.latitude) - float.Parse(gr.mapLocation.latitude));
            if (la > maxLa)
            {
                maxLa = la;
            }

            float lo = Math.Abs(float.Parse(pd.location.longitude) - float.Parse(gr.mapLocation.longitude));
            if (lo > maxlo)
            {
                maxlo = lo;
            }
        }

        foreach (POIData pd in gr.poiData)
        {
            GameObject poigo = GameObject.Instantiate(pointgo);
            poigo.transform.SetParent(group);
            dicGo[pd] = poigo;

            float dx = (float.Parse(pd.location.latitude) - float.Parse(gr.mapLocation.latitude)) / maxLa * 85f;
            float dy = (float.Parse(pd.location.longitude) - float.Parse(gr.mapLocation.longitude)) / maxlo * 85f;
            poigo.transform.localPosition = new Vector3(dy, dx, 0f);
        }

    }

    void Update()
    {
        Vector3 p = arrayTrans.localEulerAngles;//xz面投影
        arr.localEulerAngles = new Vector3(0, 0, -p.y);
    }

    private void clickBg(GameObject go, PointerEventData eventData)
    {
        CallNative.InvokeNative(CallNative.openLocation, ""); ;
    }
}