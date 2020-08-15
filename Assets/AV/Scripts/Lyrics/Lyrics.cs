using UnityEngine;
using System.Collections;

public class Lyrics : MonoBehaviour
{
    string LyricsTXT = @"
[01:20.70]北京欢迎你|为你开天辟地
[01:27.54]流动中的魅力|充满着朝气
[01:33.45]北京欢迎你|在太阳下分享呼吸
[01:39.97]在黄土地刷新成绩
[01:45.80]";

    public float time = 80.7f;

    public GameObject AscendingLyrics;
    public GameObject DescendingLyrics;

    public int LineNumber = 0;
    public bool AorD = true;
    // Update is called once per frame
    string[] LyricsTXT_;
    void Start()
    {
        Debug.Log(FormattingTime("[01:20.70]").ToString());
        LyricsTXT_ = (LyricsTXT.Split(new string[] { "\r\n" }, System.StringSplitOptions.RemoveEmptyEntries));


    }

    void Update()
    {
        time += Time.deltaTime;


        if (LyricsTXT_.Length > LineNumber && time >= FormattingTime(LyricsTXT_[LineNumber].ToString()))
        {
            TextMesh LyricsTextMesh;
            TextMesh LyricsTextMesh2;
            if (AorD)
            {
                LyricsTextMesh = AscendingLyrics.GetComponent<TextMesh>();
                //LyricsTextMesh2 = DescendingLyrics.GetComponent<TextMesh>();
            }
            else
            {
                LyricsTextMesh = AscendingLyrics.GetComponent<TextMesh>();
                //LyricsTextMesh2 = AscendingLyrics.GetComponent<TextMesh>();
            }

            LyricsTextMesh.text = isLyric(LyricsTXT_[LineNumber].ToString());
            //if (LyricsTextMesh.text.Length > 9)
            //{
            //    LyricsTextMesh.characterSize = 7f / LyricsTextMesh.text.Length;
            //        //((1f / 7f) - (1f / LyricsTextMesh.text.Length)) / (1f / 7f);
            //}
            //else
            //{
            //    LyricsTextMesh.characterSize = 1f;
            //}
            //LyricsTextMesh.color = new Color(255 / 255f, 37 / 255f, 0 / 255f);
            //LyricsTextMesh2.color = new Color(72 / 255f, 0 / 255f, 255 / 255f);
            LineNumber++;
            AorD = !AorD;
        }


        //Debug.Log(LyricsTXT_[LineNumber].ToString());
    }

    public float FormattingTime(string time)
    {
        string o = Between(time, "[", "]");
        float Minute = float.Parse(GetLeft(o, ":"));
        float Second = float.Parse(Between(o, ":", "."));
        float Millisecond = float.Parse(GetRight(o, "."));

        return Minute * 60 + Second + Millisecond / 100;
    }

    public string isLyric(string time)
    {
        //GetRight(time, "]").Replace("|", "\r\n");
        //Debug.Log(GetRight(time, "]").Split(new char[] { '|' }, System.StringSplitOptions.None).Length);


        return GetRight(time, "]").Replace("|", "\r\n"); ;

    }
    /// <summary>  
    /// 取文本左边内容  
    /// </summary>  
    /// <param name="str">文本</param>  
    /// <param name="s">标识符</param>  
    /// <returns>左边内容</returns>  
    public static string GetLeft(string str, string s)
    {
        string temp = str.Substring(0, str.IndexOf(s));
        return temp;
    }


    /// <summary>  
    /// 取文本右边内容  
    /// </summary>  
    /// <param name="str">文本</param>  
    /// <param name="s">标识符</param>  
    /// <returns>右边内容</returns>  
    public static string GetRight(string str, string s)
    {
        string temp = str.Substring(str.IndexOf(s) + 1, str.Length - str.Substring(0, str.IndexOf(s)).Length - 1);
        return temp;
    }

    /// <summary>  
    /// 取文本中间内容  
    /// </summary>  
    /// <param name="str">原文本</param>  
    /// <param name="leftstr">左边文本</param>  
    /// <param name="rightstr">右边文本</param>  
    /// <returns>返回中间文本内容</returns>  
    public static string Between(string str, string leftstr, string rightstr)
    {
        int i = str.IndexOf(leftstr) + leftstr.Length;
        string temp = str.Substring(i, str.IndexOf(rightstr, i) - i);
        return temp;
    }
}
