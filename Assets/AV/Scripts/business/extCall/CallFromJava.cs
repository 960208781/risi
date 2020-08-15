using LitJson;
using message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;
using System.IO;
using cn.sharerec;

public class CallFromJava : MonoBehaviour
{
    public GameObject roodObj;
    public GameObject pointRoot;
    public GameObject radarUI;
    public GameObject luopanRoot;
    public GameObject planeObject;
    public static float jingdu = 0f;
    void Start()
    {
        //Photograph("");
        //测试数据
#if UNITY_EDITOR
        var poi = Resources.Load<TextAsset>("poi").text;
        updateCard(poi);
#endif
        //OpenNavigation(str12);
        //opennavi(strRoad);
        //opennavi(strRoad);
        //TargetManager.i_TargetManager._ConversionMode(true,TargetManager.ARNumber.BirdsNest);



#if UNITY_EDITOR  || UNITY_ANDROID

        // yield return new WaitForSeconds(0.1f);
        ScanAR("");
#endif

#if UNITY_ANDROID
        ShareREC.OnRecorderStoppedHandler = OnRecorderStopped;
#endif
    }



    /// <summary>
    /// 自由AR   玩一玩
    /// </summary>
    /// <param name="number"></param>
    public void FreeAR(string number)
    {
        FlipCamera("0");
        roodObj.gameObject.SetActive(false);
        pointRoot.gameObject.SetActive(false);
        radarUI.gameObject.SetActive(false);
        luopanRoot.gameObject.SetActive(false);

        MainController.Ins.FreeAr(number);
    }
    /// <summary>
    /// 扫描Ar
    /// </summary>
    /// <param name="nul"></param>
    public void ScanAR(string nul)
    {
        MainController.Ins.ClearBuffer();
        MainController.Ins.curMod = Mode.AR;
        FlipCamera("0");
        roodObj.gameObject.SetActive(false);
        pointRoot.gameObject.SetActive(false);
        radarUI.gameObject.SetActive(false);
        luopanRoot.gameObject.SetActive(false);
        MainController.Ins.FreeCamera();
    }

    //结束AR
    public void ArEnd(string nul)
    {
        MainController.Ins.curMod = Mode.None;
        MainController.Ins.ClearBuffer();
        FlipCamera("0");
    }

    public void TuYa(string str)
    {
        Debug.LogError("------生成涂鸦--------" + gameObject.name);
        var tuyaObj = Resources.Load<GameObject>("tuya");
        var go = Instantiate(tuyaObj);
        MainController.Ins.PushBuffer(go);
    }

    public void Flip()
    {
        FlipCamera("0");
    }

    //拍照
    public void Photograph(string nul)
    {
        StartCoroutine(CaptureScreenshot2());
    }
    IEnumerator waitPhotograph(float time)
    {
        //开启logo

        yield return new WaitForSeconds(time);
        //关闭logo

    }
    //闪光灯
    public void FlashLamp(string number)
    {
        //if (number == "0")
        //{
        //    ARBuilder.Instance.CameraDeviceBehaviours[0].Device.SetFlashTorchMode(true);//闪光灯
        //}
        //else
        //{
        //    ARBuilder.Instance.CameraDeviceBehaviours[0].Device.SetFlashTorchMode(false);//关闭闪光灯
        //}
    }


    private int camIndex = 0;
    public void ChangeCam(int index)
    {
        if (camIndex == index)
            return;
        VoidAR.GetInstance().CloseCamera();
        int opend = 0;
        VoidAR.GetInstance().OpenCamera(index, ref opend);

        VoidAR.GetInstance().StartCapture();
        var vo = GameObject.FindObjectOfType<VoidARBehaviour>();
        if (vo == null) return;
        var r = vo.GetComponentInChildren<Renderer>();
        if (index == 1)
            r.sharedMaterial.shader = Shader.Find("Unlit/YUV420Flip");
        else r.sharedMaterial.shader = Shader.Find("Unlit/YUV420");
        MainController.Ins.ARCam.depth = 0;
        camIndex = index;
    }

    //翻转摄像头
    public void FlipCamera(string number)
    {
        if (number == "0")
        {
            ChangeCam(0);
        }
        else
        {
            ChangeCam(1);
        }
    }
    /// <summary>
    /// 选中卡片
    /// </summary>
    /// <param name="strJson"></param>
    public void selectCard(string strJson)
    {
        JsonData jd = JsonMapper.ToObject(strJson);

        string id = jd["id"].ToString();

        cb.showPoiDetail(id);


    }

    /// <summary>
    /// 关闭导航，返回到实景导览
    /// </summary>
    /// <param name="strJson"></param>
    public void closeNai()
    {

    }

    CreateCityBehaviour cb;
    /// <summary>
    /// 更新卡片信息
    /// </summary>
    /// <param name="strCard"></param>
    public void updateCard(string strCard)
    {
        MainController.Ins.navigationCamera();

        FlipCamera("0");
        roodObj.gameObject.SetActive(false);
        pointRoot.gameObject.SetActive(true);
        radarUI.gameObject.SetActive(true);
        luopanRoot.gameObject.SetActive(true);
        cb = planeObject.GetComponent<CreateCityBehaviour>();
        if (cb == null)
        {
            return;
        }


        // var map = JsonMapper.ToObject<Map.MapRoot>(strCard);

        //更新卡片
        GCAroundInfo gr = new GCAroundInfo();
        JsonData jd = JsonMapper.ToObject(strCard.Replace("'", "\""));
        gr.mapLocation = new Location();
        gr.mapLocation.latitude = jd["location"]["latitude"].ToString();
        gr.mapLocation.longitude = jd["location"]["longitude"].ToString();

        gr.poiData = new List<POIData>();
        JsonData itemsdata = jd["poiList"];
        for (int i = 0; i < itemsdata.Count; i++)
        {
            POIData poi = new POIData();
            poi.id = itemsdata[i]["id"].ToString();
            poi.name = itemsdata[i]["name"].ToString();
            poi.type = itemsdata[i]["type"].ToString();
            poi.biz_type = itemsdata[i]["biz_ext"].ToString();
            poi.address = itemsdata[i]["address"].ToString();
            poi.tel = itemsdata[i]["tel"].ToString();
            poi.distance = Convert.ToInt32(itemsdata[i]["distance"].ToString());
            JsonData tempdata = itemsdata[i]["biz_ext"];
            poi.rating = tempdata["rating"].ToString();
            if (poi.rating == "")
            {
                poi.rating = "0";
            }
            poi.desc = JsonMapper.ToJson(itemsdata[i]);// itemsdata["cityname"].ToString();
            poi.iconImage = "";//itemsdata["cityname"].ToString();
            poi.location = new Location();
            poi.location.longitude = itemsdata[i]["location"].ToString().Split(',')[0];
            poi.location.latitude = itemsdata[i]["location"].ToString().Split(',')[1];

            gr.poiData.Add(poi);
        }
        cb.initAllData(gr);

        RadarUI.instance.initAllData(gr);
        CallNative.InvokeNative(CallNative.cardUpdateCallBack);
    }

    /// <summary>
    /// 绘制路线
    /// </summary>
    /// <param name="json"></param>
    public void OpenNavigation(string json)
    {
        if (json == "")
        {
            N_Controller.__int__().DrawingRoute();
        }
        else
        {
            ArEnd("");
            FlipCamera("0");
            roodObj.gameObject.SetActive(true);
            pointRoot.gameObject.SetActive(false);
            radarUI.gameObject.SetActive(false);
            luopanRoot.gameObject.SetActive(false);
            N_Controller.__int__().DrawingRoute(json);
        }
    }
    /// <summary>
    /// 步进
    /// </summary>
    /// <param name="json"></param>
    public void setGPS_Stepping(string json)
    {
        JsonData _Json = JsonMapper.ToObject(json);
        N_Controller.__int__().GPS_Stepping(float.Parse(_Json["latitude"].ToString()), float.Parse(_Json["longitude"].ToString()));

    }
    /// <summary>
    /// 到达终点
    /// </summary>
    /// <param name="nul"></param>
    public void ReachTheEnd(string nul)
    {
        N_Controller.__int__().GPS_Dis();
        //开启陀螺仪
        //Input.compensateSensors = false;
        //Input.location.Start();
        //Input.compass.enabled = true;
        //Input.gyro.enabled = true;
        //if (Input.compass.magneticHeading != 0f)
        //{
        //    CallFromJava.jingdu = Input.compass.magneticHeading;

        //}
    }

    /// <summary>  
    /// Captures the screenshot2.  
    /// </summary>  
    /// <returns>The screenshot2.</returns>  
    /// <param name="rect">Rect.截图的区域，左下角为o点</param>  
    IEnumerator CaptureScreenshot2()
    {

        var obj = Resources.Load<GameObject>("waterMark");
        clone.Push(Instantiate(obj));

        string filename = "";
        logo._Display(true);
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

#if UNITY_STANDALONE_WIN || UNITY_EDITOR
        filename = "Screenshot.png";
        Application.CaptureScreenshot(filename);
#endif
#if UNITY_ANDROID
        filename = Application.persistentDataPath + "/Screenshot.png";

        int width = Screen.width;
        int height = Screen.height;

        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);//设置Texture2D
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);//获取Pixels           
        tex.Apply();//应用改变
        byte[] bytes = tex.EncodeToPNG();//转换为byte[]

        Destroy(tex);
        System.IO.File.WriteAllBytes(filename, bytes);
#elif UNITY_IPHONE
        filename = Application.persistentDataPath + "/Screenshot.jpg";

        int width = Screen.width;
        int height = Screen.height;

        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);//设置Texture2D
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);//获取Pixels           
        tex.Apply();//应用改变
        byte[] bytes = tex.EncodeToJPG();//转换为byte[]
        Debug.Log(bytes.Length);
        Destroy(tex);
        System.IO.File.WriteAllBytes(filename, bytes);
#endif

        Debug.Log(filename);
        logo._Display(false);

        CallNative.InvokeNative(CallNative.takePhotosSucess, filename);

        ClearClone();
    }

    /// <summary>
    /// OnGUI is called for rendering and handling GUI events.
    /// This function can be called multiple times per frame (one call per event).
    /// </summary>
    void OnGUI()
    {
        GUI.color = new Color(1, 1, 1, 0.1f);
        GUI.Box(new Rect(0, 0, 1, 1), "");

        // if (GUI.Button(new Rect(0, 0, 100, 100), "Tuya"))
        // {

        //     TuYa("");
        // }
        // GUI.color = Color.red;
        // if (GUI.Button(new Rect(0, 0, 100, 100), "Nav"))
        // {

        //     string json = Resources.Load<TextAsset>("route").text;
        //     OpenNavigation(json);
        // }
    }

    Stack<GameObject> clone = new Stack<GameObject>();

    private void ClearClone()
    {
        while (clone.Count > 0)
        {
            var go = clone.Pop();
            Destroy(go);
        }
    }

    //开始录屏
    public void StartRecorder(string nul)
    {
        var obj = Resources.Load<GameObject>("waterMark");
        clone.Push(Instantiate(obj));
#if UNITY_ANDROID
        if (ShareREC.IsAvailable())
        {
            ShareREC.StartRecorder();
        }
        else
        {
            //设备不支持
            CallNative.InvokeNative(CallNative.recordSucess, "0");
        }

#elif UNITY_IPHONE
        // com.mob.ShareREC.startRecoring();
#endif
    }

    //结束录屏
    public void StopRecorder(string nul)
    {
        ClearClone();
#if UNITY_ANDROID
        ShareREC.StopRecorder();
#elif UNITY_IPHONE
        // com.mob.ShareREC.stopRecording(e => { if (e == null) { string path = com.mob.ShareREC.lastRecordingPath(); UnityForNative.recordSucess(path); } });
#endif
    }

    void OnRecorderStopped()
    {
#if UNITY_ANDROID
        long[] videoNumber = ShareREC.ListLocalVideos();
        CallNative.InvokeNative(CallNative.recordSucess, ShareREC.GetLocalVideoPath(videoNumber[videoNumber.Length - 1]));
#endif
#if UNITY_IPHONE
        // UnityForNative.recordSucess("ok");
#endif
    }

}
