using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
public class CallIos : MonoBehaviour
{
#if UNITY_IPHONE
    /// <summary>
    /// unity 启动成功
    /// </summary>
    [DllImport("__Internal")]
    private static extern void unityStarted(string nul);
    public static void _unityStarted(string nul)
    {
        unityStarted(nul);
    }
    /// <summary>
    /// 拍照成功
    /// </summary>
    [DllImport("__Internal")]
    private static extern void takePhotosSucess(string path);
    public static void _takePhotosSucess(string path)
    {
        Debug.Log("反馈ok");
        takePhotosSucess(path);
    }
    /// <summary>
    /// 扫描成功
    /// </summary>
    /// <param name="path"></param>
    [DllImport("__Internal")]
    private static extern void scanSucess(string nul);
    public static void _scanSucess(string nul)
    {
        scanSucess(nul);
    }
    /// <summary>
    /// 我要订票
    /// </summary>
    /// <param name="path"></param>
    [DllImport("__Internal")]
    private static extern void bookTicket(string nul);
    public void _bookTicket(string nul)
    {
        bookTicket(nul);
    }
    /// <summary>
    /// 右上角点击
    /// </summary>
    /// <param name="path"></param>
    [DllImport("__Internal")]
    private static extern void openLocation(string nul);
    public static void _openLocation(string nul)
    {
        openLocation(nul);
    }

    [DllImport("__Internal")]
    private static extern void showPoiDetail(string strjson);
    public static void _showPoiDetail(string strjson)
    {
        showPoiDetail(strjson);
    }

    [DllImport("__Internal")]
    private static extern void getLocation(string nul);
    public static void _getLocation(string nul)
    {
        getLocation(nul);
    }


    [DllImport("__Internal")]
    private static extern void cardUpdateCallBack(string nul);
    public static void _cardUpdateCallBack(string nul)
    {
        cardUpdateCallBack(nul);
    }

    [DllImport("__Internal")]
    public static extern void showLoading(string nul);

    [DllImport("__Internal")]
    public static extern void closeLoading(string nul);

    [DllImport("__Internal")]
    public static extern void arLoadedCallBack(string nul);

    [DllImport("__Internal")]
    public static extern void UnityCommonCall(string nul);

#endif
}
