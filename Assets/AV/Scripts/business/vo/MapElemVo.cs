using UnityEngine;
using System.Collections;
using System;
using ZGGame;

public class MapElemVo
{
    //public static int maxDistance;

    public string id;
    public int index;
    public string name;
    public string code;

    public string desc;
    public string address;
    public string phone;

    public int distance;

    public double latitude;
    public double longitude;

    public float rate;

    public string image;

    public bool arEnabled;

    public double dirAngle;

    public Vector2 anglePos;

    public Vector2 radarPos;

    public Transform trans;

    public double selfAngle;

    public Vector2 uiPos;

    public bool visible;
    public float selfLatitude;
    public float selfLongitude;

    public void init(message.POIData d, float _selfLatitude, float _selfLongitude)
    {
        id = d.id;
        name = d.name;
        distance = d.distance;
        code = d.typecode;
        address = d.address;
        phone = d.tel;
        selfLatitude = _selfLatitude;
        selfLongitude = _selfLongitude;
        //只取小数部分
        double.TryParse(d.location.latitude, out latitude);
        double.TryParse(d.location.longitude, out longitude);

        if (d.rating.Length > 0)
            rate = float.Parse(d.rating);

        arEnabled = d.isAR == 1;

        image = d.iconImage;

        desc = d.desc;

        dirAngle = GPSUtil.getAngle(new Vector2(selfLongitude, selfLatitude), new Vector2((float)longitude, (float)latitude));
    }
   
}
