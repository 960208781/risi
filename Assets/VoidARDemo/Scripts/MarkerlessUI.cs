using UnityEngine;

public class MarkerlessUI : MonoBehaviour {
    void OnGUI()
    {
        var btnHeight = Screen.height * 0.1f;
        var btnWidth = btnHeight * 3.0f;
        var gap = 20;
        GUI.skin.button.fontSize = 36;
        if (GUI.Button(new Rect(Screen.width - btnWidth, gap, btnWidth, btnHeight), "Start"))
        {
            VoidAR.GetInstance().startMarkerlessTracking();
        }

        if (GUI.Button(new Rect(Screen.width - btnWidth, gap * 2 + btnHeight, btnWidth, btnHeight), "Reset"))
        {
			VoidAR.GetInstance().resetMarkerless();
        }

        if (!Application.isMobilePlatform) {
            GUI.color = Color.red;
            GUI.Label(new Rect(50, 0, Screen.width - 100, 60), "仅支持iOS、Android设备运行！");
        }
    }
}
