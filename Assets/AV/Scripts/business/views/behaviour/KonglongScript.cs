using UnityEngine;
using System.Collections;

public class KonglongScript : MonoBehaviour {
    private Animation ani;
    public AudioSource huxi;
    public AudioSource hou;
    public AudioSource run;
    private string lastName;
	// Use this for initialization
	void Start () {
        ani = GetComponent<Animation>();

        run.gameObject.SetActive(false);
        hou.gameObject.SetActive(false);
        huxi.gameObject.SetActive(true);
	}
	
	// Update is called once per frame
    void Update()
    {
        return;
        string curAni = getAni();
        if(curAni == "")
        {
            if (lastName == "chuchang")
            {
                ani.Play("run");
                run.gameObject.SetActive(true);
                hou.gameObject.SetActive(false);
            }
            else
            {
                ani.Play("chuchang");
                hou.gameObject.SetActive(true);
                run.gameObject.SetActive(false);
            }
        }
        lastName = curAni;
    }

    public string getAni()
    {
        if(ani == null)
        {
            return "";
        }
        foreach (AnimationState anim in ani)
        {
            if (ani.IsPlaying(anim.name))
            {
                return anim.name;
            }
        }
        return "";
    }
}
