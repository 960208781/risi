using UnityEngine;
using System.Collections;

public class TG_TowardsCamera : MonoBehaviour
{
    public GameObject cameraTran;
	
	
	// Update is called once per frame
	void Update () {
        this.transform.eulerAngles = new Vector3(cameraTran.transform.eulerAngles.x, cameraTran.transform.eulerAngles.y, cameraTran.transform.eulerAngles.z);
	}
}
