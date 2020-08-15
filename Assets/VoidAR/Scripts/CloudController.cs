using UnityEngine;
public class CloudController : CloudControllerBase {
    /* 二次开发
    void Awake()
    {
        //侦听云识别目标创建完成事件（开始跟踪）
        AddEventListener(VoidAREvent.COMPLETE, OnComplete);
        //AB资源下载进度
        AddEventListener(VoidAREvent.PROGRESS, OnDownload);
        //异常通知
        AddEventListener(VoidAREvent.ERROR, OnError);
    }

    void OnComplete(VoidAREvent evt) {
        GameObject target = evt.data as GameObject;
        Debug.Log("OnComplete target :" + target.name);
    }

    void OnDownload(VoidAREvent evt) {
        int progress = (int)evt.data; //下载进度值0-100
        Debug.Log("OnDownload progress :" + progress);
    }

    void OnError(VoidAREvent evt) {
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
        base.OnSuccess(url, name, metadata);
        Debug.Log("OnSuccess metadata:" + metadata);
    }

    /// <summary>
    /// 设置云视频组件
    /// 可以设置扩展后的ImageTargetBehaviour和VideoPlayBehaviour（使用Unity5.6原生播放器）
    /// </summary>
    /// <param name="markerTarget">marker目标对象</param>
    /// <param name="videoPlayTarget">视频播放器对象</param>
    /// <param name="markerName">marker图片</param>
    /// <param name="videoPath">云视频路径</param>
    /// <returns></returns>
    protected override IMarker SetCloudVideoComponent(GameObject markerTarget, GameObject videoPlayTarget, string markerName, string videoPath)
    {
        var itb = markerTarget.AddComponent<ImageTargetBase>();
        itb.AddEventListener(VoidAREvent.FIND, OnFind);
        itb.AddEventListener(VoidAREvent.LOST, OnFind);
        itb.SetPath(markerName);
		var player = videoPlayTarget.AddComponent<VoidVideoPlayer>();
        var vpb = videoPlayTarget.AddComponent<VideoPlayBehaviour>();
		player.url = videoPath;
        return itb;
    }

    void OnFind(VoidAREvent evt) {
        Debug.Log("Cloud video  OnFind Event target:" + evt.currentTarget + " data = " + evt.data + " type = " + evt.name);
    }
    */

    protected override IMarker SetCloudVideoComponent(GameObject markerTarget, GameObject videoPlayTarget, string markerName, string videoPath)
    {
        var itb = markerTarget.AddComponent<ImageTargetBase>();
        itb.SetPath(markerName);
        videoPlayTarget.AddComponent<VoidVideoPlayer>().url = videoPath;
        var vpb = videoPlayTarget.AddComponent<VideoPlayBehaviour>();
        return itb;
    }
}