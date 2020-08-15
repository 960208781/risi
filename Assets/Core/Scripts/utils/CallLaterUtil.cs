using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// 延迟回调
/// </summary>
public class CallLaterUtil
{

    public delegate void laterCallback();

    public static Dictionary<DateTime, List<laterCallback>> dicCallback;

    /// <summary>
    /// 添加回调
    /// </summary>
    /// <param name="callback"></param>
    /// <param name="seconds"></param>
    public static void addFun(laterCallback callback, float seconds)
    {
        if (dicCallback == null)
        {
            dicCallback = new Dictionary<DateTime, List<laterCallback>>();
        }

        DateTime dt = DateTime.Now;
        dt = dt.AddMilliseconds(seconds * 1000);
        if (!dicCallback.ContainsKey(dt))
        {
            dicCallback[dt] = new List<laterCallback>();
        }
        dicCallback[dt].Add(callback);
    }

    public static void removeFun(laterCallback callback)
    {
        List<laterCallback> listCallback = null;
        foreach(KeyValuePair<DateTime,List<laterCallback>> keyvalue in dicCallback)
        {
            foreach(laterCallback lcb in keyvalue.Value)
            {
                if(lcb == callback)
                {
                    listCallback = keyvalue.Value;
                    break;
                }
            }
        }
        if(listCallback != null)
        {
            listCallback.Remove(callback);
        }
    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    public static void logicUpdate()
    {
        if (dicCallback == null)
        {
            return;
        }
        DateTime dt = DateTime.Now;
        List<DateTime> delList = null;
        try
        {
            Dictionary<DateTime, List<laterCallback>> cloneDic = new Dictionary<DateTime, List<laterCallback>>();
            foreach (KeyValuePair<DateTime, List<laterCallback>> keyvalue in dicCallback)
            {
                cloneDic[keyvalue.Key] = keyvalue.Value;
            }
            foreach (KeyValuePair<DateTime, List<laterCallback>> keyvalue in cloneDic)
            {
                DateTime curTime = keyvalue.Key;
                if (curTime < dt)
                {
                    foreach (laterCallback callback in keyvalue.Value)
                    {
                        if (callback == null)
                        {
                            continue;
                        }
                        if (delList == null)
                        {
                            delList = new List<DateTime>();
                        }
                        delList.Add(curTime);
                        callback();
                    }
                }
            }
        }
        catch (Exception e)
        {
        }

        if (delList == null)
        {
            return;
        }
        foreach (DateTime deldt in delList)
        {
            dicCallback.Remove(deldt);
        }
    }

    public static void clear()
    {
        if (dicCallback != null)
        {
            dicCallback.Clear();
        }
    }
}
