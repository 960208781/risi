using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CallJava
{
    public static void CallAndroid(string method, string param)
    {
#if UNITY_ANDROID
        try
        {

            AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
            jo = jo.Get<AndroidJavaObject>("functionUnity");
            if (jo != null)
            {
                jo.Call(method, param);
            }
        }
        catch (Exception)
        {
            Debug.LogError(method + " 不存在");
        }
#endif
    }
}
