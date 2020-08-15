using UnityEngine;
using System.Collections;

public class Finger : MonoBehaviour
{
    int dragFingerIndex = -1;

    public float pinchScaleFactor = 0.02f;
    bool Pinching;
    bool draging;
    Vector2 qishi;


    private GameObject current = null;
    //拖动
    void OnDrag(DragGesture gesture)
    {
        if (gesture.Fingers.Count != 1)
        {
            return;
        }

        // first finger
        FingerGestures.Finger finger = gesture.Fingers[0];

        if (gesture.Phase == ContinuousGesturePhase.Started)
        {
            // dismiss this event if we're not interacting with our drag object

            if (gesture.Selection == null)
                return;

            current = MainController.Ins.bufferGo.Find(i => i == gesture.Selection);

            qishi = gesture.Position;
            dragFingerIndex = finger.Index;
        }
        else if (finger.Index == dragFingerIndex)  // gesture in progress, make sure that this event comes from the finger that is dragging our CU
        {
            if (current == null) return;
            if (current.name.Contains(MarkType.Model + ""))
                return;
            if (gesture.Phase == ContinuousGesturePhase.Updated && !Pinching)
            {
                var move = GetWorldPos(gesture.Position) - GetWorldPos(qishi);
                current.transform.position += move;
                qishi = gesture.Position;

            }
            else
            {
                dragFingerIndex = -1;
                current = null;
            }
        }
    }



    //缩放
    void OnPinch(PinchGesture gesture)
    {
        if (gesture.Fingers.Count != 2)
        {
            return;
        }

        if (gesture.Phase == ContinuousGesturePhase.Started)
        {
            Pinching = true;
        }
        else if (gesture.Phase == ContinuousGesturePhase.Updated)
        {
            if (Pinching)
            {
                var cur = MainController.Ins.currentARObj;
                if (cur == null) return;
                if (cur.name.Contains(MarkType.Model + ""))
                    return;
                float Delta = gesture.Delta.Centimeters();
                if (Mathf.Abs(Delta) > 0.05f)
                {
                    var scaleObj = MainController.Ins.currentARObj;
                    if (scaleObj)
                    {
                        var target = scaleObj.GetComponentInChildren<VideoPlayer>();
                        if (target)
                        {
                            var value = target.transform.localScale.x + Delta * 0.07f;
                            if (value > 0.01f)
                            {
                                var v = target.transform.localScale.x / target.transform.localScale.z;
                                var z = value / v;
                                target.transform.localScale = new Vector3(value, 1, z);
                            }
                        }
                    }
                }
            }
        }
        else
        {
            if (Pinching)
            {
                Pinching = false;
            }
        }
    }

    //把Unity屏幕坐标换算成3D坐标
    Vector3 GetWorldPos(Vector2 screenPos)
    {
        var eventCam = MainController.Ins.FreeCam;
        var pos = eventCam.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, eventCam.transform.position.y));
        return pos;
    }
}
