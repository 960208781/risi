using UnityEngine;
using System.Collections;

public class Demo : MonoBehaviour {

    void OnGUI()
    {
        var btnHeight = Screen.height * 0.08f;
        var btnWidth = btnHeight * 8f;
        var gap = 17;
        GUI.skin.button.fontSize = (int)(btnHeight * 0.4f);
        if (GUI.Button(new Rect((Screen.width - btnWidth) * 0.5f, gap, btnWidth, btnHeight), "图形识别"))
        {
            Application.LoadLevel("ImageDemo");
        }

        if (GUI.Button(new Rect((Screen.width - btnWidth) * 0.5f, gap * 2 + btnHeight * 1, btnWidth, btnHeight), "云识别"))
        {
            Application.LoadLevel("CloudDemo");
        }

        if (GUI.Button(new Rect((Screen.width - btnWidth) * 0.5f, gap * 3 + btnHeight * 2, btnWidth, btnHeight), "视频播放"))
        {
            Application.LoadLevel("VideoDemo");
        }

        if (GUI.Button(new Rect((Screen.width - btnWidth) * 0.5f, gap * 4 + btnHeight * 3, btnWidth, btnHeight), "视频录制"))
        {
            Application.LoadLevel("RECDemo");
        }

        if (GUI.Button(new Rect((Screen.width - btnWidth) * 0.5f, gap * 5 + btnHeight * 4, btnWidth, btnHeight), "动态加载"))
        {
            Application.LoadLevel("DynamicLoadDemo");
        }

		if (GUI.Button(new Rect((Screen.width - btnWidth) * 0.5f, gap * 6 + btnHeight * 5, btnWidth, btnHeight), "Markerless"))
		{
			Application.LoadLevel("MarkerlessDemo");
		}

		if (GUI.Button(new Rect((Screen.width - btnWidth) * 0.5f, gap * 7 + btnHeight * 6, btnWidth, btnHeight), "VideoPlay Extension Tracking(SLAM)"))
		{
			Application.LoadLevel("VideoPlayExtensionTrackingDemo");
		}

		if (GUI.Button(new Rect((Screen.width - btnWidth) * 0.5f, gap * 8 + btnHeight * 7, btnWidth, btnHeight), "ImageTarget Extension Tracking(SLAM)"))
		{
			Application.LoadLevel("ImageExtensionTrackingDemo");
		}

    }
}
