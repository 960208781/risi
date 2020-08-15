using UnityEngine;
using System.Collections;

public class dogCam : MonoBehaviour {

    public Transform childCam;
    private Transform curTrans;
	// Use this for initialization
	void Start () {
        curTrans = transform;
	}
	
	// Update is called once per frame
	void Update () {
	    if(childCam != null)
        {
            curTrans.localEulerAngles = new Vector3(0, childCam.localEulerAngles.y, 0);
        }
	}
}
