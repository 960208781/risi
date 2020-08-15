using UnityEngine;
using System.Collections;

public class InIsD : MonoBehaviour {
    public GameObject viewObj;
    /// <summary>
    /// 父物体
    /// </summary>
    public GameObject viewObj_p;

    public GameObject Left;
    public GameObject In;
    public GameObject Right;
    /// <summary>
    /// 被点击
    /// </summary>
    void OnMouseDown()
    {
        viewObj.GetComponent<NewViewObjScript>().isOnMouseDown();
        
    }

    

}
