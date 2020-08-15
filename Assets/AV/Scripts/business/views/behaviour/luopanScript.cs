using UnityEngine;
using System.Collections;

public class luopanScript : MonoBehaviour
{
    public Transform luopanTransfrom;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    void Update()
    {

#if UNITY_ANDROID
        transform.localEulerAngles = new Vector3(luopanTransfrom.localRotation.eulerAngles.x, luopanTransfrom.localRotation.eulerAngles.y, luopanTransfrom.localRotation.eulerAngles.z);// new Vector3(0, -dy, 0);
#elif UNITY_IPHONE
        transform.localEulerAngles = new Vector3(luopanTransfrom.localRotation.eulerAngles.x, luopanTransfrom.localRotation.eulerAngles.y, luopanTransfrom.localRotation.eulerAngles.z);// new Vector3(0, -dy, 0);
#endif

    }

   
}
