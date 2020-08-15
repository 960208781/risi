using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using System.IO;

public class MainController : CloudControllerBase
{
    public static MainController Ins { get; private set; }

    public Camera ARCam;
    public GameObject mainLight;
    public Camera FreeCam;


    public Shader flip;
    public Shader normal;

    // /* 二次开发
    void Awake()
    {
        Ins = this;
        //侦听云识别目标创建完成事件（开始跟踪）
        AddEventListener(VoidAREvent.COMPLETE, OnComplete);
        //AB资源下载进度
        AddEventListener(VoidAREvent.PROGRESS, OnDownload);
        //异常通知
        AddEventListener(VoidAREvent.ERROR, OnError);

        var vo = GameObject.FindObjectOfType<VoidARBehaviour>();
        ARCam = vo.GetComponent<Camera>();
        camIndex = vo.CameraIndex;

        CallNative.InvokeNative(CallNative.u3dStarted, "");
    }

    void OnComplete(VoidAREvent evt)
    {
        // GameObject target = evt.data as GameObject;
        // Debug.Log("OnComplete target :" + target.name);
    }

    void OnDownload(VoidAREvent evt)
    {
        int progress = (int)evt.data; //下载进度值0-100

        Debug.Log("OnDownload progress :" + progress);
    }

    void OnError(VoidAREvent evt)
    {
        int errorCode = (int)evt.data;
        Debug.LogError("cloud error :" + errorCode);
    }

    /// <summary>
    /// 云识别数据成功响应
    /// </summary>
    /// <param name="url">资源URL</param>
    /// <param name="name">资源名称</param>
    /// <param name="metadata">扩展数据</param>
    protected override void OnSuccess(string url, string name, string metadata)
    {
        //默认AB模型资源Assetsbundle通过WWW加载，云视频创建视频播放器，
        //可以通过自定义数据实现云识别目标自定义创建（创建完成后，通过VoidAR.GetInstance().addCloudTarget()接口加入到SDK识别）
        // base.OnSuccess(url, name, metadata);
        // Debug.Log("OnSuccess metadata:" + metadata);
        if (VoidAR.GetInstance().isMarkerExist(name))
            return;

        Debug.LogError("cloud data:" + metadata);

        var me = strTOMeta(metadata);
        me.markID = name; ;

        // #if UNITY_EDITOR
        if (!string.IsNullOrEmpty(url))
        {
            me.markT = MarkType.Model;
            me.ResUrl = url;
        }

        CreateMarker(me, null);
    }


    public Meta strTOMeta(string metadata)
    {
        var content = LitJson.JsonMapper.ToObject<CloudContent>(metadata);
        if (content == null) return null;
        Meta me = new Meta();
        if (content.showType == "0")
            me.showT = ShowType.type_free;
        else if (content.showType == "1")
            me.showT = ShowType.type_push;
        else
            me.showT = ShowType.type_ar;

        me.isTrack = content.isTrack == "0" ? false : true;

        //image
        if (!string.IsNullOrEmpty(content.ar_picture))
        {
            me.markT = MarkType.Image;
            me.ResUrl = content.ar_picture;
        }
        else if (!string.IsNullOrEmpty(content.ar_video_alpha))
        {
            me.markT = MarkType.Video;
            me.ResUrl = content.ar_video_alpha + "|alpha";
        }
        //video
        else if (!string.IsNullOrEmpty(content.ar_video))
        {
            me.markT = MarkType.Video;
            me.ResUrl = content.ar_video;
        }

        else if (Application.platform == RuntimePlatform.Android)
        {
            //android AB
            if (!string.IsNullOrEmpty(content.ar_android_model))
            {
                me.markT = MarkType.Model;
                me.ResUrl = content.ar_android_model;
            }
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            //ios AB

            if (!string.IsNullOrEmpty(content.ar_ios_model))
            {
                me.markT = MarkType.Model;
                me.ResUrl = content.ar_ios_model;
            }
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            Debug.LogError("WindowsEditor");
            //android AB
            if (!string.IsNullOrEmpty(content.ar_android_model))
            {
                me.markT = MarkType.Model;
                me.ResUrl = content.ar_android_model;
            }
        }

        return me;

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
            r.sharedMaterial.shader = flip;
        else r.sharedMaterial.shader = normal;
        ARCam.depth = 0;
        camIndex = index;
    }

    private int progress;


    [NonSerialized]
    public List<GameObject> bufferGo = new List<GameObject>(5);
    public void ClearBuffer()
    {
        StopAllCoroutines();
        for (int i = 0; i < bufferGo.Count; i++)
        {
            var g = bufferGo[i];
            Destroy(g);
        }
        bufferGo.Clear();
        Resources.UnloadUnusedAssets();
        if (currentTarget)
            VoidAR.GetInstance().removeTarget(currentTarget.meta.markID);
        if (mainLight)
            mainLight.SetActive(true);
    }


    public void navigationCamera()
    {
        Debug.LogError("---------------导航模式开启-------------------");
        curMod = Mode.navigation;
        ARCam.cullingMask = 2;
        mainLight.SetActive(true);
        ClearBuffer();
    }

    public void FreeCamera()
    {
        Debug.LogError("---------------AR 模式开启-------------------");
        ARCam.cullingMask = 2;
        curMod = Mode.AR;
    }

    [NonSerialized]
    public Mode curMod = Mode.navigation;

    [NonSerialized]
    public ShowType showT = ShowType.type_push;
    public bool isTrack
    {
        get
        {
            return (currentTarget != null && currentTarget.meta.isTrack == true);
        }
    }

    public void FreeAr(string json)
    {
        ClearBuffer();

        mainLight.SetActive(true);
        var me = strTOMeta(json);
        showT = me.showT;
        switch (me.markT)
        {
            case MarkType.Image:
                currentARObj = LoadImage(me);
                currentARObj.name = me.markT.ToString();
                break;
            case MarkType.Video:
                currentARObj = LoadVideo(me);
                currentARObj.name = me.markT.ToString();
                break;
            case MarkType.Model:
                currentARObj = LoadModel(me);
                currentARObj.name = me.markT.ToString();
                break;
            default: break;
        }
    }

    private IEnumerator LoadModelAsync(string path, GameObject spawnPoint)
    {
        CallNative.InvokeNative(CallNative.showLoading, "请稍候");
        var index = path.LastIndexOf("/");
        var name = path.Substring(index + 1, path.Length - index - 1);
        var modelpath = BufferCtrl.modDir + "/" + name;
        var islocal = File.Exists(modelpath);
        if (islocal)
        {
            var ab = GetAB(name);
            if (ab == null)
            {
                ab = AssetBundle.LoadFromFile(modelpath);
                PushAB(name, ab);
            }
            var bundle_obj = (GameObject)Instantiate(ab.mainAsset);
            bundle_obj.transform.parent = spawnPoint.transform;
            bundle_obj.transform.localPosition = Vector3.zero;
            PushBuffer(spawnPoint);
        }
        else
        {
            WWW bundle = new WWW(path);
            this.progress = 0;
            while (!bundle.isDone)
            {
                var p = (int)(bundle.progress * 100f);
                if (p > this.progress)
                {
                    this.progress = p;
                    CallNative.InvokeNative(CallNative.showLoading, string.Format("请稍候... {0}%", progress));
                }
                yield return new WaitForEndOfFrame();
            }
            yield return bundle;
            if (bundle.error == null)
            {
                File.WriteAllBytes(modelpath, bundle.bytes);
                var bundle_obj = (GameObject)Instantiate(bundle.assetBundle.mainAsset);
                bundle_obj.transform.parent = spawnPoint.transform;
                bundle_obj.transform.localPosition = Vector3.zero;

                PushBuffer(spawnPoint);

                bundle.assetBundle.Unload(false);
            }

        }
        CallNative.InvokeNative(CallNative.closeLoading, "");
        CallNative.InvokeNative(CallNative.arLoaded);

    }

    public GameObject LoadModel(Meta me)
    {
        mainLight.SetActive(true);
        GameObject temp = new GameObject();
        StartCoroutine(LoadModelAsync(me.ResUrl, temp));
        return temp;
    }

    public GameObject LoadImage(Meta meta)
    {
        var vdobj = Resources.Load<GameObject>("image");
        GameObject image = Instantiate(vdobj);

        var imp = image.GetComponentInChildren<ImagePlayer>();
        imp.SetUrl(meta.ResUrl);
        PushBuffer(image);
        return image;
    }

    public void PushBuffer(GameObject go)
    {
        bufferGo.Add(go);
    }

    private Dictionary<string, AssetBundle> abBuffer = new Dictionary<string, AssetBundle>();
    private void PushAB(string key, AssetBundle ab)
    {
        abBuffer.Add(key, ab);
    }

    private AssetBundle GetAB(string key)
    {
        if (abBuffer.ContainsKey(key))
        {
            return abBuffer[key];
        }
        return null;
    }

    private GameObject LoadVideo(Meta me)
    {
        Debug.LogError("Load Video:" + me.ResUrl);
        GameObject temp = new GameObject();
        temp.transform.localPosition = Vector3.zero;
        temp.transform.localRotation = Quaternion.identity;
        temp.transform.localScale = Vector3.one;
        PlayVideo(me, temp);
        return temp;
    }

    public GameObject PlayVideo(Meta me, GameObject spawnPoint)
    {
        Debug.LogError("play video:" + me.ResUrl);

        var vdobj = Resources.Load<GameObject>("video");
        GameObject videoPlayTarget = Instantiate(vdobj);
        videoPlayTarget.name = me.markT.ToString();
        videoPlayTarget.transform.parent = spawnPoint.transform;
        videoPlayTarget.transform.localPosition = Vector3.zero;

        var vp = videoPlayTarget.GetComponentInChildren<VideoPlayer>();
        vp.SetUrl(me.ResUrl);
        vp.playOnAwake = true;
        vp.loop = true;
        vp.Play();

        CallNative.InvokeNative(CallNative.showLoading, "视频加载中...");
        vp.AddEventListener(VoidAREvent.READY, new UnityAction<VoidAREvent>(this.OnPlayerReady));
        bufferGo.Add(spawnPoint);

        return videoPlayTarget;
    }

    protected void OnPlayerReady(VoidAREvent evt)
    {
        var vd = (VideoPlayer)evt.currentTarget;
        CallNative.InvokeNative(CallNative.closeLoading, "");
        CallNative.InvokeNative(CallNative.arLoaded, "");
        vd.RemoveEventListener(VoidAREvent.READY, new UnityAction<VoidAREvent>(this.OnPlayerReady));
    }
    void OnGUI()
    {
#if UNITY_EDITOR // || UNITY_ANDROID
        var buttonSize = new Vector2(Screen.width * 0.1f, Screen.width * 0.1f);
        if (GUI.Button(new Rect(0, 0, buttonSize.x, buttonSize.y), "Scan"))
        {
            GameObject.Find("callUnity").GetComponent<CallFromJava>().ScanAR("");

            // var clone = Instantiate(Resources.Load("tw")) as GameObject;
            // PushBuffer(clone);

            // LoadTuya(null);
            // TuYa();
        }

        // if (GUI.Button(new Rect(0, 100, 100, 100), "Nav"))
        // {
        //     string json = Resources.Load<TextAsset>("poi_1").text;
        //     GameObject.Find("callUnity").GetComponent<CallFromJava>().updateCard(json);
        //     // FreeAr(json);
        // }

        if (GUI.Button(new Rect(0, 200, buttonSize.x, buttonSize.y), "Clear"))
        {
            ClearBuffer();
        }
#endif
    }


    protected void CreateMarker(Meta data, Func<Meta, GameObject> makerComplete)
    {
        GameObject markerTarget = new GameObject(data.markT.ToString())
        {
            transform = {
            localScale = Vector3.one,
            localPosition = Vector3.zero,
            localEulerAngles = Vector3.zero
        }
        };
        var itb = markerTarget.AddComponent<RealImageTarget>();
        itb.AddEventListener(VoidAREvent.FIND, OnFind);
        itb.AddEventListener(VoidAREvent.LOST, OnLost);
        itb.SetPath(data.markID);
        itb.Set(data);
        if (makerComplete != null)
        {
            var child = makerComplete(data);
            itb.go = child;
        }
        VoidAR.GetInstance().addCloudTarget(itb, markerTarget);
        base.DispatchEvent(VoidAREvent.COMPLETE, markerTarget);

        if (currentTarget == null)
            LoadAR(itb);

    }
    public RealImageTarget currentTarget { get; private set; }
    public GameObject currentARObj { get; private set; }
    public void OnFind(VoidAREvent evt)
    {
        LoadAR((RealImageTarget)evt.currentTarget);
    }

    private void LoadAR(RealImageTarget target)
    {
        if (curMod == Mode.navigation || curMod == Mode.None) return;
        currentTarget = target;
        if (currentTarget.go == null)
        {
            switch (currentTarget.meta.markT)
            {
                case MarkType.Image:
                    currentTarget.go = LoadImage(currentTarget.meta);
                    break;
                case MarkType.Video:
                    currentTarget.go = LoadVideo(currentTarget.meta);
                    break;
                case MarkType.Model:
                    currentTarget.go = LoadModel(currentTarget.meta);
                    break;
                default: break;
            }
            curMod = Mode.None;
            currentARObj = currentTarget.go;
        }
        CallNative.InvokeNative(CallNative.scanSucess, "");
    }

    public void OnLost(VoidAREvent evt)
    {

    }
    // */
}
