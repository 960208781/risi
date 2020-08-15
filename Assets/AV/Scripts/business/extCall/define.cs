using System;

public enum MarkType
{
    None,
    Model,
    Video,
    Image

}

public enum Mode
{
    navigation,
    AR,
    None
}

public enum ShowType
{
    type_free = 0,
    type_push = 1,
    type_ar = 3
}


public class Meta
{
    public string markID;
    public string name;
    public MarkType markT;
    public string ResUrl;
    public ShowType showT;
    public bool isTrack = false;
}

public class CloudContent
{
    public string id;
    public string isTrack = "0";
    public string ar_picture;
    public string ar_android_model;
    public string ar_ios_model;
    public string ar_video;
    public string ar_video_alpha;
    public string name;
    public String showType = "0";
}