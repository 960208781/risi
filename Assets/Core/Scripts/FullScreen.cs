using System.Collections;
using UnityEngine;

public class FullScreen : MonoBehaviour
{
    bool isSet = false;
    Camera cam;

    void Start()
    {
        if ((float)Screen.height / Screen.width > ((float)16 / 9))
        {
            isSet = true;
            //得到主Camera
            cam = GetComponent<Camera>();
            //设置为正交
            cam.orthographic = true;
        }
    }

    private void Update()
    {
        if (isSet == true)
        {
            if (transform.GetChild(0).GetComponent<Renderer>().material.name == "BackgroundMaterial (Instance)")
            {
                cam.orthographicSize = Mathf.Abs(transform.GetChild(0).localScale.y) / 2f;
                cam.fieldOfView = 40f;
                isSet = false;
            }
        }
    }
}