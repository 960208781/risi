using UnityEngine;
using System.Collections;

public class roleScript : MonoBehaviour {

    private Animation ani;
    private AudioSource audioSource;
	// Use this for initialization
	void Start () {
        Application.targetFrameRate = 30;
        ani = gameObject.GetComponent<Animation>();
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.enabled = false;
	}

    private void OnEnable()
    {
        curFrame = 0;
        //旋转180度
        //transform.parent.eulerAngles = new Vector3(transform.parent.eulerAngles.x, 180, transform.parent.eulerAngles.z);
    }

    int curFrame = 0;
	// Update is called once per frame
	void Update () {
	    if(ani != null)
        {
            if(!ani.isPlaying)
            {
                ani.Play("run");
                //transform.parent.eulerAngles = new Vector3(transform.parent.eulerAngles.x, 0, transform.parent.eulerAngles.z);
            }
        }

        if(curFrame == 60)
        {
            if (audioSource != null)
            {
                audioSource.enabled = true;
                audioSource.Play();
            }
        }

        curFrame++;
    }
}
