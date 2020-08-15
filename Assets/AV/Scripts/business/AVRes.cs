using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class AVRes : MonoBehaviour
{
    private static Dictionary<string,GameObject> resDic = new Dictionary<string,GameObject>();

    public static GameObject getRes(string uiName)
    {
        GameObject obj = null;
        resDic.TryGetValue(uiName,out obj);
        return obj;
    }
    void Awake()
    {
        int len = transform.childCount;
        for (int i = len - 1; i >= 0; i--)
        {
            Transform t = transform.GetChild(i);
            if(!resDic.ContainsKey(t.name))
            {
                resDic.Add(t.name,t.gameObject);
            }
        }
    }

}
