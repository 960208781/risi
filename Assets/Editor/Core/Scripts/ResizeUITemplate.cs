using UnityEngine;
using System.Collections;

public class ResizeUITemplate : MonoBehaviour
{
    public RectTransform rt;
    // Use this for initialization
    void Start()
    {
        Debug.Log(rt.sizeDelta);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
