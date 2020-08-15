using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// 路线生成控制器
/// </summary>
public class N_Controller : MonoBehaviour
{
    /// <summary>
    /// 起始位置
    /// </summary>
    public GameObject StartingPosition;
    /// <summary>
    /// 用于绘制路线的物体
    /// </summary>
    public GameObject RoutePoint;
    /// <summary>
    /// 用于保存路线的父物体
    /// </summary>
    public GameObject RouteParent;
    /// <summary>
    /// 用户位置
    /// </summary>
    public GameObject player;
    /// <summary>
    /// 路点初始化的旋转
    /// </summary>
    public Vector3 RoutePointRotate;
    /// <summary>
    ///  路点初始化的缩放
    /// </summary>
    public Vector3 RoutePointScale;
    /// <summary>
    /// 偏移量
    /// </summary>
    Vector3 deviation = new Vector3(0f, 0f, 0f);
    /// <summary>
    /// 路点数量
    /// </summary>
    int RouteLength = 0;
    /// <summary>
    /// 路点集合
    /// </summary>
    ArrayList pathList = new ArrayList();
    /// <summary>
    /// 计算生成路点数组
    /// </summary>
    ArrayList resultList = new ArrayList();
    /// <summary>
    /// 路点材质缓存
    /// </summary>
    ArrayList pathMaterial = new ArrayList();
   

    #region 单例
    static N_Controller _N_Controller;
    public static N_Controller __int__()
    {
        if (_N_Controller == null)
        {
            _N_Controller = new N_Controller();
        }
        return _N_Controller;
    }
    void Awake()
    {
        _N_Controller = this;
    }
    #endregion
    /// //////////////////////  ceshi

    //bool liulan = false;
    //int liulan_i = 0;
    /// //////////////////////
    void OnGUI()
    {
        //if (GUI.Button(new Rect(0, 0, 100, 60), "重绘"))
        //{
        //    DrawingRoute();
        //}
        //if (GUI.Button(new Rect(0, 70, 100, 60), "移动1"))
        //{
        //    float x = 39.96404638888889f;
        //    float y = 116.45982972222222f;
        //    GPS_Stepping(x, y);
        //}
        //if (GUI.Button(new Rect(0, 140, 100, 60), "移动2"))
        //{
        //    float x = 39.97405638888889f;
        //    float y = 116.45982972222222f;
        //    GPS_Stepping(x, y);

        //}
        //if (GUI.Button(new Rect(0, 210, 100, 60), "浏览路线"))
        //{
        //    liulan_i=0;
        //    liulan=true;
        //}
        //if (GUI.Button(new Rect(0, 280, 100, 60), "暂停浏览"))
        //{
        //    liulan = false;
        //}
        //if (GUI.Button(new Rect(0, 350, 100, 60), "继续浏览"))
        //{
        //    liulan = true;
        //}
    }
    // Use this for initialization
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
    //    ////////////测试
    //    if (liulan==true)
    //    {
    //        if (resultList.Count > liulan_i)
    //        {
    //            GPS_Stepping((Vector3)resultList[liulan_i]);
    //            liulan_i++;
    //        }
    //        else
    //        {
    //            liulan = false;
    //        }
            
    //    }
    //    ///////////
    }
    /// <summary>
    /// 绘制路线
    /// </summary>
    /// <param name="json"></param>
    public void DrawingRoute(string json)
    {
        //更新json
        N_JsonData.__int__().Initialization(json);
        //获取点信息
        DrawingRoute();
    }
    /// <summary>
    /// 重绘路线
    /// </summary>
    public void DrawingRoute()
    {

        RouteLength = 0;
        pathMaterial = new ArrayList();
        //删除原组件
        foreach (Transform item in RouteParent.GetComponentsInChildren<Transform>())
        {
            if (item != RouteParent.transform)
            {
                Object.Destroy(item.gameObject);
            }

        }
        //清空缓存
        pathList = new ArrayList();
        //筛选重复，记录坐标点
        if (N_JsonData.NData.steps.Length >= 1)
        {


            for (int ii = 0; ii < N_JsonData.NData.steps[0].polylines.Length; ii++)
            {
                //精度
                float longitude = float.Parse(N_JsonData.NData.steps[0].polylines[ii].longitude);
                //纬度
                float latitude = float.Parse(N_JsonData.NData.steps[0].polylines[ii].latitude);
                //缩放
                longitude = longitude - float.Parse(N_JsonData.NData.origin.longitude);
                latitude = latitude - float.Parse(N_JsonData.NData.origin.latitude);
                Vector3 path = new Vector3(longitude * 10000, 0f, latitude * 10000);

                pathList.Add(path);
            }
            RouteLength += N_JsonData.NData.steps[0].distance;
        }

        for (int i = 1; i < N_JsonData.NData.steps.Length; i++)
        {
            for (int ii = 1; ii < N_JsonData.NData.steps[i].polylines.Length; ii++)
            {
                //精度
                float longitude = float.Parse(N_JsonData.NData.steps[i].polylines[ii].longitude);
                //纬度
                float latitude = float.Parse(N_JsonData.NData.steps[i].polylines[ii].latitude);

                //缩放
                longitude = longitude - float.Parse(N_JsonData.NData.origin.longitude);
                latitude = latitude - float.Parse(N_JsonData.NData.origin.latitude);

                //等比例放大
                Vector3 path = new Vector3(longitude * 10000, 0f, latitude * 10000);

                pathList.Add(path);
            }
            RouteLength += N_JsonData.NData.steps[i].distance;
        }

        //贝塞尔曲线优化
       // resultList = new ArrayList();
        resultList = ZyPointController.PointList((Vector3[])pathList.ToArray(typeof(Vector3)), (int)(RouteLength / N_JsonData.NData.steps.Length));
   

        if (resultList.Count > 0)
        {
            deviation = (Vector3)resultList[0];

        }
        Debug.Log(resultList.Count);
        //缓存
        GameObject temp = null;
        //绘制路线  优化朝向 (从后向前看)
        for (int i = 0; i < resultList.Count; i++)
        {
            GameObject gameObject = (GameObject)Instantiate(RoutePoint);
            gameObject.name = "Route_" + i.ToString();
            gameObject.transform.SetParent(RouteParent.transform);
            //调整偏差
            gameObject.transform.localPosition = (Vector3)resultList[i] - deviation;
            gameObject.transform.localScale = RoutePointScale;
            //开始朝向
            if (temp != null)
            {
                gameObject.transform.LookAt(temp.transform, Vector3.up);
                gameObject.transform.Rotate(RoutePointRotate, Space.Self);
            }
            else
            {
                
                gameObject.transform.localRotation = new Quaternion() { eulerAngles = RoutePointRotate };
            }
            if (i==1)
            {
               
                temp.transform.LookAt(gameObject.transform, Vector3.up);
                temp.transform.Rotate(new Vector3(0f, 180f,0f), Space.Self);
                
            }
            temp = gameObject;
            gameObject.SetActive(true);
           
            try
            {
                pathMaterial.Add(gameObject.GetComponent<MeshRenderer>());
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }

    }
    /// <summary>
    /// 步进
    /// </summary>
    /// <param name="latitude">纬度</param>
    /// <param name="longitude">经度</param>
    public void GPS_Stepping(float latitude, float longitude)
    {
        float latitude_ = latitude - float.Parse(N_JsonData.NData.origin.latitude);
        float longitude_ = longitude - float.Parse(N_JsonData.NData.origin.longitude);
        Vector3 path = new Vector3(longitude_ * 10000, 0f, latitude_ * 10000);
        path -= deviation;
        

        //for (int i = 0; i < resultList.Count; i++)
        //{
        //    Debug.Log(Vector3.Distance((Vector3)(resultList[i]), path));  
        //}
        if (player == null)
        {
            player = new GameObject();
        }
        GPS_Dis(path);
        player.transform.localPosition = path;
    }
    /// <summary>
    /// 步进
    /// </summary>
    /// <param name="latitude">纬度</param>
    /// <param name="longitude">经度</param>
    void GPS_Stepping(Vector3 path)
    {
        if (player == null)
        {
            player = new GameObject();
        }
        player.transform.localPosition = GPS_Dis(path);
    }
    /// <summary>
    /// 步进根方法
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    Vector3 GPS_Dis(Vector3 path)
    {
        int isOK = 0;
        float mani = 99999999f;
        for (int i = 0; i < resultList.Count; i++)
        {
            
            float mani_ = Vector3.Distance((Vector3)(resultList[i]), path);
            if (mani > mani_)
            {
                isOK = i;  
                mani = mani_;
            }
        }
        for (int i = 0; i < resultList.Count; i++)
        {
            if (i > isOK)
            {
                ((MeshRenderer)pathMaterial[i]).materials[0].SetColor("_Color", new Color(0f, 244f / 255f, 255f / 255f));
            }
            else
            {
                ((MeshRenderer)pathMaterial[i]).materials[0].SetColor("_Color", new Color(1f, 0f, 0f));
            }
            if (i==isOK)
            {
                GameObject one = ((MeshRenderer)pathMaterial[i]).gameObject;
                one.transform.localPosition = new Vector3(one.transform.localPosition.x, 0.001f, one.transform.localPosition.z);  
            }
        }
        return (Vector3)resultList[isOK];
    }
    public void GPS_Dis()
    {
        for (int i = 0; i < resultList.Count; i++)
        {
            ((MeshRenderer)pathMaterial[i]).materials[0].SetColor("_Color", new Color(1f, 0f, 0f));
        } 
    }

}
