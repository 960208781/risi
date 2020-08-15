using UnityEngine;
using System.Collections;

public class CreateBalls : MonoBehaviour
{
    public Transform t;
    public GameObject ball;
    public float r = 10;
    public bool rr = false;
    // Use this for initialization
    void Start()
    {
        Input.gyro.enabled = true;
        for (int i = 0; i < 18; i++)
        {
            GameObject nb = Instantiate(ball);
            nb.transform.SetParent(transform);
            nb.transform.localPosition = new Vector3(r * Mathf.Sin(20f * i * Mathf.PI / 180f), 0,r * Mathf.Cos(20f * i * Mathf.PI / 180f));
            nb.SetActive(true);
        }
        Input.gyro.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
