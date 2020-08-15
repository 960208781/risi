using UnityEngine;
using System.Collections;

public class PictureAcquisition : MonoBehaviour
{

    

    Vector3 Mouse_p;
    //三张
    public GameObject[] pictures = new GameObject[3];
    //全部图片
    public Texture[] t_pictures = new Texture[13];
    int number;
    // Use this for initialization
    void Start()
    {
        number = 1;
        CalculationNumber(t_pictures.Length - 1, NumberMode.DontMove);
    }

    void PictureRight()
    {
        CalculationNumber(t_pictures.Length - 1, NumberMode.Right);
    }
    void PictureLeft()
    {
        CalculationNumber(t_pictures.Length - 1, NumberMode.Left);
    }
    int CalculationNumber(int maxnum, NumberMode mode)
    {
        if (mode.GetHashCode() == 0 || mode.GetHashCode() == 2)
        {
            if (mode.GetHashCode() != 2)
            {
                if (number-1 < 0)
                {
                    number = maxnum;
                }
                else
                {
                    number = number - 1;
                }
            }

            
           
            
        }
        else if (mode.GetHashCode() == 1 || mode.GetHashCode() == 2)
        {
            if (mode.GetHashCode() != 2)
            {
                if (number + 1 > maxnum)
                {
                    number = 0;
                }
                else
                {
                    number = number + 1;
                }
            }
            Debug.Log(number);
            
        }
        if (number - 1 < 0)
        {
            pictures[0].GetComponent<MeshRenderer>().materials[0].SetTexture("_MainTex", t_pictures[maxnum]);
        }
        else
        {
            pictures[0].GetComponent<MeshRenderer>().materials[0].SetTexture("_MainTex", t_pictures[number - 1]);
        }

        pictures[1].GetComponent<MeshRenderer>().materials[0].SetTexture("_MainTex", t_pictures[number]);

        if (number + 1 > maxnum)
        {
            pictures[2].GetComponent<MeshRenderer>().materials[0].SetTexture("_MainTex", t_pictures[0]);
        }
        else
        {
            pictures[2].GetComponent<MeshRenderer>().materials[0].SetTexture("_MainTex", t_pictures[number + 1]);
        }
        return number;
    }

    void OnMouseDown()
    {
        Mouse_p = Input.mousePosition;
    }
    void OnMouseUp()
    {
        if (Input.mousePosition.x > Mouse_p.x)
        {
            PictureRight();
            Debug.Log("you");
        }
        else
        {
            PictureLeft();
            Debug.Log("zuo");
        }

    }
    // Update is called once per frame
    void Update()
    {

    }
    //模式枚举
    public enum NumberMode
    {
        Right = 0,
        Left = 1,
        DontMove=2
    };
}
