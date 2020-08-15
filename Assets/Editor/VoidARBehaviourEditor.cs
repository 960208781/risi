using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;


[CustomEditor(typeof(VoidARBehaviour))]
public class VoidARBehaviourEditor : Editor
{

#if UNITY_IPHONE
    const string dllName = "__Internal";
#elif UNITY_ANDROID
    const string dllName = "VoidAR-Plugin";
#else
    const string dllName = "VoidAR-Plugin";
#endif
    [DllImport(dllName)]
    private static extern void _getCameraList(byte[] names);

    // Use this for initialization
    VoidARBehaviourEditor() {
    }

    public override void OnInspectorGUI()
    {
        VoidARBehaviour obj = target as VoidARBehaviour;

        string[] markerTypes = { "Shape", "Image" , "Markerless", "Extension Tracking" };
        string[] TrackNum = { "1", "2" , "3", "4", "5" , "6","7", "8" , "9","10"};
        obj.markerType = (VoidARBehaviour.EMarkerType)EditorGUILayout.Popup("MarkerType", (int)(obj.markerType), markerTypes );
        if (obj.markerType == VoidARBehaviour.EMarkerType.Shape) {
            obj.shapeMatchAccuracy = EditorGUILayout.IntField ("Match Accuracy", obj.shapeMatchAccuracy);
            obj.tracking = EditorGUILayout.Toggle ("Is Tracking", obj.tracking);
            obj.is_use_cloud = false;
            obj.simultaneousTrackingMax = 1;
        } else if (obj.markerType == VoidARBehaviour.EMarkerType.Image) {
            obj.simultaneousTrackingMax = EditorGUILayout.Popup ("Simultaneous Tracking", obj.simultaneousTrackingMax - 1, TrackNum) + 1;
            obj.tracking = false;
            obj.shapeMatchAccuracy = 5;

            obj.is_use_cloud = EditorGUILayout.Toggle ("Use Cloud", obj.is_use_cloud);
            if (obj.is_use_cloud) {
                obj.accessKey = EditorGUILayout.TextField ("Access Key", obj.accessKey);
                obj.secretKey = EditorGUILayout.TextField ("Secret Key", obj.secretKey);
                obj.UseExtensionTracking = false;
            } else {
                obj.UseExtensionTracking = EditorGUILayout.Toggle ("Extension Tracking", obj.UseExtensionTracking);
            }

            //Debug.Log (obj.simultaneousTrackingMax);
        } else if (obj.markerType == VoidARBehaviour.EMarkerType.Markerless) {
            obj.simultaneousTrackingMax = 1;
            EditorStyles.textField.wordWrap = true;
            obj.appKey = EditorGUILayout.TextField("App License Key", obj.appKey, new GUILayoutOption[] { GUILayout.MinHeight(30f), GUILayout.MaxHeight(50f) });
            obj.markerlessParent = EditorGUILayout.ObjectField ("MarkerlessNode", obj.markerlessParent, typeof(GameObject), true) as GameObject;
        } else if (obj.markerType == VoidARBehaviour.EMarkerType.MarkerSlam) {
            obj.tracking = false;
            obj.shapeMatchAccuracy = 5;
            EditorStyles.textField.wordWrap = true;
            obj.appKey = EditorGUILayout.TextField("App License Key", obj.appKey, new GUILayoutOption[] { GUILayout.MinHeight(30f), GUILayout.MaxHeight(50f) });
        }

#if UNITY_IPHONE || UNITY_ANDROID
        string[] deviceNames = new string[2];
        deviceNames[0] = "后置摄像头";
        if (obj.markerType == VoidARBehaviour.EMarkerType.Image)
        {
            deviceNames[1] = "前置摄像头";
        }
        obj.CameraIndex = EditorGUILayout.Popup("Camera", obj.CameraIndex, deviceNames);
        EditorUtility.SetDirty(target);
#else
        byte[] names = new byte[1024];
        _getCameraList(names);
        string result = System.Text.Encoding.UTF8.GetString(names);
        char[] delimiterChars = { ',' };
        string[] deviceNames = result.Split(delimiterChars);
        obj.CameraIndex = EditorGUILayout.Popup("Camera Device", obj.CameraIndex, deviceNames);

#endif

        //EditorUtility.SetDirty(target);
        Undo.RecordObject(target,"change property");
    }

}
