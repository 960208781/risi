using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class CallNative
{

    /// <summary>
    /// 景点数据详情  null 关闭
    /// </summary>
    public const string showPoiDetail = "showPoiDetail";

    /// <summary>
    /// 显示poi详情面板
    /// </summary>
    public const string openLocation = "openLocation";

    /// <summary>
    /// unity3d 启动成功回调
    /// </summary>
    public const string u3dStarted = "unityStarted";
    /// <summary>
    ///  扫描成功
    /// </summary>
    public const string scanSucess = "scanSucess";

    /// <summary>
    /// 更新信息回调
    /// </summary>
    public const string cardUpdateCallBack = "cardUpdateCallBack";

    public const string showLoading = "showLoading";

    public const string closeLoading = "closeLoading";

    public const string arLoaded = "arLoaded";

    public const string recordSucess = "recordSucess";

    public const string takePhotosSucess = "takePhotosSucess";

    public static void InvokeNative(string method, string data = "")
    {

        Debug.Log("InvokeNative:" + method + "--------" + data);
#if UNITY_EDITOR
        // Debug.Log("Call:" + method + "--------" + data);
#elif UNITY_ANDROID
        CallJava.CallAndroid(method, data);
#elif UNITY_IPHONE
var nativego = GameObject.Find("callnative");
        nativego.SendMessage("_"+method, data);
#endif
    }


    public void Call(string args)
    {
#if UNITY_EDITOR
        Debug.Log("Call:" + args);
#elif UNITY_ANDROID
        CallJava.CallAndroid("UnityCommonCall", args);
#elif UNITY_IPHONE
        CallIos.UnityCommonCall(args);
#endif
    }
}
