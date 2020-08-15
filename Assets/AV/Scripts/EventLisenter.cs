using UnityEngine;
using System;
using UnityEngine.EventSystems;
public class EventLisenter : MonoBehaviour, IPointerClickHandler
{

    public Action<GameObject, PointerEventData> OnClick;
    public Action<GameObject> OnDoubleClick;
    public static EventLisenter Get(GameObject go)
    {
        var ev = go.GetComponent<EventLisenter>();
        if (ev == null) ev = go.AddComponent<EventLisenter>();
        return ev;
    }
    private float time;
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        OnClick(gameObject, eventData);
        if (Time.time - time < 0.5f)
        {
            OnDoubleClick(gameObject);
        }
        time = Time.time;
    }
    void Update()
    {

    }


}
