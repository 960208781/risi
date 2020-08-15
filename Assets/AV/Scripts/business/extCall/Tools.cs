using UnityEngine;
using System.Collections;

public class Tools : MonoBehaviour
{

    public static Vector2 GetPlaneSize(Camera camera, Transform tran)
    {
        var distance = Vector3.Distance(camera.transform.position, tran.position);
        var corners = GetCorners(camera, distance);
        var width = Vector3.Distance(corners[0], corners[1]);
        var height = Vector3.Distance(corners[0], corners[2]);
        return new Vector2(width, height);
    }
    static Vector3[] GetCorners(Camera theCamera, float distance)
    {
        var tx = theCamera.transform;
        Vector3[] corners = new Vector3[4];

        float halfFOV = (theCamera.fieldOfView * 0.5f) * Mathf.Deg2Rad;
        float aspect = theCamera.aspect;

        float height = distance * Mathf.Tan(halfFOV);
        float width = height * aspect;

        // UpperLeft
        corners[0] = tx.position - (tx.right * width);
        corners[0] += tx.up * height;
        corners[0] += tx.forward * distance;

        // UpperRight
        corners[1] = tx.position + (tx.right * width);
        corners[1] += tx.up * height;
        corners[1] += tx.forward * distance;

        // LowerLeft
        corners[2] = tx.position - (tx.right * width);
        corners[2] -= tx.up * height;
        corners[2] += tx.forward * distance;

        // LowerRight
        corners[3] = tx.position + (tx.right * width);
        corners[3] -= tx.up * height;
        corners[3] += tx.forward * distance;

        return corners;
    }
}
