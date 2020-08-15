using UnityEngine;
using System.Collections;

public class TG_Gyroscope : MonoBehaviour
{
    float jingdu = 0f;
    private Transform MyTransform;
    void Start()
    {
        //开启陀螺仪
        Input.location.Start();
        Input.compensateSensors = true;
        Input.gyro.enabled = true;
        Input.compass.enabled = true;
        
        MyTransform = transform;
    }

    void Update()
    {
        if (Input.compass.magneticHeading != 0f && jingdu == 0)
        {
            jingdu = Input.compass.magneticHeading;
        }
        Quaternion Gyro = Input.gyro.attitude;
        Gyro.x *= -1.0f;
        Gyro.y *= -1.0f;
        Vector3 gyro =  (Quaternion.Euler(90, 0, 0) * Gyro).eulerAngles;
#if UNITY_ANDROID
        MyTransform.localRotation = Quaternion.Euler(new Vector3(gyro.x, gyro.y , gyro.z));
#elif UNITY_IPHONE
        MyTransform.localRotation = Quaternion.Euler(new Vector3(gyro.x, gyro.y + jingdu, gyro.z));
        #endif
    }
}
