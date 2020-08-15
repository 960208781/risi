using UnityEngine;
using System.Collections;

public class pointScript : MonoBehaviour {
    public Transform pointTransfrom;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

#if UNITY_ANDROID
        transform.localEulerAngles = new Vector3(pointTransfrom.localRotation.eulerAngles.x, pointTransfrom.localRotation.eulerAngles.y, pointTransfrom.localRotation.eulerAngles.z);// new Vector3(0, -dy, 0);
#elif UNITY_IPHONE
        transform.localEulerAngles = new Vector3(pointTransfrom.localRotation.eulerAngles.x, pointTransfrom.localRotation.eulerAngles.y, pointTransfrom.localRotation.eulerAngles.z);// new Vector3(0, -dy, 0);
#endif

	}

}
