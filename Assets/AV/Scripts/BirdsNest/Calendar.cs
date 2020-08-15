using UnityEngine;
using System.Collections;

public class Calendar : MonoBehaviour
{
    public Texture[] textu;

    public Material mat;
    void OnMouseUp()
    {
        switch (this.gameObject.name)
        {
            case "7":
                {
                    mat.SetTexture("_MainTex", textu[0]);
                }
                break;
            case "8":
                {
                    mat.SetTexture("_MainTex", textu[1]);
                }
                break;
            case "9":
                {
                    mat.SetTexture("_MainTex", textu[2]);
                }
                break;
            case "GrabTicket":
                {
                    //回调java
                    // CallNative.bookTicket();
                }
                break;
            default:
                break;
        }

    }
}
