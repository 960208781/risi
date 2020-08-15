using UnityEngine;
using System.Collections;
using System;

public class RealImageTarget : ImageTargetBase
{
    public GameObject go;
    public Meta meta { get; private set; }
    public Action<VoidAREvent> FoundActon;
    public Action<VoidAREvent> LostActon;


    public void Set(Meta data)
    {
        meta = data;
    }
    void Start()
    {
        AddEventListener(VoidAREvent.FIND, OnFind);
        AddEventListener(VoidAREvent.LOST, OnLost);
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
        RemoveEventListener(VoidAREvent.FIND, OnFind);
        RemoveEventListener(VoidAREvent.LOST, OnFind);
    }

    void OnFind(VoidAREvent evt)
    {
        if (FoundActon != null) FoundActon(evt);
        // Debug.Log("Cloud video  OnFind Event target:" + evt.currentTarget + " data = " + evt.data + " type = " + evt.name);
    }

    void OnLost(VoidAREvent evt)
    {
        if (LostActon != null) LostActon(evt);
        // Debug.Log("Cloud video  OnLost Event target:" + evt.currentTarget + " data = " + evt.data + " type = " + evt.name);
    }

}
