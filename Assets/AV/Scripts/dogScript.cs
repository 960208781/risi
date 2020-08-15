using UnityEngine;
using System.Collections;

public class dogScript : MonoBehaviour {
    private static int startX = -600;
    public GameObject dog;
    public GameObject scrollbar;
    private RectTransform recttransform;
    private Animation dogAnimation;
    private Transform dogTransform;
    private int minLen;
    private int maxLen = 400;
	// Use this for initialization
	void Start () {
        dogAnimation = dog.GetComponent<Animation>();
        dogAnimation.Play("run");
        dogTransform = transform;
        dogTransform.localPosition = new Vector3(startX, -135, 95);
        minLen = startX;
        recttransform = scrollbar.GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
        if (minLen >= maxLen)
        {
            minLen = startX;
        }
        else
        {
            minLen += 10;
        }
        dogTransform.localPosition = new Vector3(minLen, -135, 95);

        recttransform.anchoredPosition = new Vector2(minLen/4, recttransform.anchoredPosition.y);
	}
}
