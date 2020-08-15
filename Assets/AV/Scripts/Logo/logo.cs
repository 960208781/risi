using UnityEngine;
using System.Collections;

public class logo : MonoBehaviour {
    public Texture _logo;
    float Width = 0f;
    float Height = 0f;

    float ScreenWidth = 0f;
    void Awake()
    {
        ScreenWidth = Screen.width;

        Width = Screen.width / 3;
        Height = (_logo.width / Width) * _logo.height;
    }
  
    public static bool display = false;
    void OnGUI()
    {
        if (ScreenWidth!=Screen.width)
        {
            Width = Screen.width / 3;
            Height = (_logo.width / Width) * _logo.height; 
        }
        if (display==true)
        {
          
            GUI.Label(new Rect(Screen.width / 30, Screen.height / 48, Width, Height), _logo);
            
        }
        
    }

    public static void _Display(bool _display)
    {
        display = _display;
    }

}
