using UnityEngine;
using System.Collections;

public class TB_Rotation : MonoBehaviour
{
    public enum RotationAxis
    {
        // global/world axis
        WorldX,
        WorldY,
        WorldZ,

        // local axis
        ObjectX,
        ObjectY,
        ObjectZ,

        // camera axis
        CameraX,
        CameraY,
        CameraZ
    }
    public float Sensitivity = 1.0f;
    public TB_Rotation.RotationAxis Axis = RotationAxis.WorldY;
    public Vector3 GetRotationAxis()
    {
        switch (Axis)
        {
            case RotationAxis.WorldX:
                return Vector3.right;

            case RotationAxis.WorldY:
                return Vector3.up;

            case RotationAxis.WorldZ:
                return Vector3.forward;

            case RotationAxis.ObjectX:
                return transform.right;

            case RotationAxis.ObjectY:
                return transform.up;

            case RotationAxis.ObjectZ:
                return transform.forward;

                // case RotationAxis.CameraX:
                //     return ReferenceCamera.transform.right;

                // case RotationAxis.CameraY:
                //     return ReferenceCamera.transform.up;

                // case RotationAxis.CameraZ:
                //     return ReferenceCamera.transform.forward;
        }

        Debug.LogWarning("Unhandled rotation axis: " + Axis);
        return Vector3.forward;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, target, Time.deltaTime * 50); ;
    }
    void OnDrag(DragGesture gesture)
    {
        if (gesture.Selection) return;
        var length = gesture.DeltaMove.sqrMagnitude;
        var offset = gesture.DeltaMove.x >= 0 ? -1 : 1;
        Quaternion qRot = Quaternion.AngleAxis(Sensitivity * length * offset, GetRotationAxis());
        target = qRot * transform.rotation;
    }
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        target = transform.rotation;
    }

    private Quaternion target;
}
