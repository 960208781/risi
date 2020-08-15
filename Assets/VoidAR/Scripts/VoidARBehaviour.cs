using UnityEngine;
using System.Collections.Generic;

public class VoidARBehaviour : VoidARBase
{
    /*打开为调试模式，运行时在屏幕左上角显示日志
    protected override void Awake()
    {
        Application.logMessageReceived += HandleLog;
        base.Awake();
    }

    private Queue<string> queue = new Queue<string>(10);
    private string alertStr = "";
    void OnDestory()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string message, string stackTrace, LogType type)
    {
        message = System.DateTime.Now.ToString("HH:mm:ss.fff") + " - " + message;
        if (type == LogType.Log)
        {
            queue.Enqueue(message);
            if (queue.Count > 9)
            {
                queue.Dequeue();
            }
        }
        else if (type == LogType.Warning || type == LogType.Error)
        {
            alertStr += message + "\n";
        }
    }


    Vector2 scrollPos = Vector2.zero;
    public void OnGUI()
    {
        scrollPos = GUILayout.BeginScrollView(scrollPos);
        GUI.skin.label.fontSize = 24;
        foreach (string s in queue)
        {
            GUILayout.Label((string)s);
        }

        GUILayout.EndScrollView();

        if (alertStr != "")
        {
            GUI.Window(0, new Rect(Screen.width / 4, Screen.height / 4, Screen.width / 2, Screen.height / 2), DoAlertWindow, "Error");
        }
    }

    void DoAlertWindow(int windowID)
    {
        GUI.Label(new Rect(Screen.width / 12, 20, Screen.width / 3, Screen.height / 2 - 20 - Screen.height * 0.1f), alertStr);
        if (GUI.Button(new Rect(Screen.width / 8, Screen.height / 2 - 20 - Screen.height * 0.1f, Screen.width / 4, Screen.height * 0.1f), "Close"))
        {
            alertStr = "";
        }
    }*/
}