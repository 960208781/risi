// Pointer
using UnityEngine;
using UnityEngine.EventSystems;

public class Pointer : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        base.GetComponentInParent<InputAutosize>().closeDown = true;
    }

    private void Start()
    {
    }

    private void Update()
    {
    }
}
