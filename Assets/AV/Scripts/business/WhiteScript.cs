using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class WhiteScript : MonoBehaviour
{
    public Transform cityTran;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {

            }
            else
            {

            }
        }
    }
}
