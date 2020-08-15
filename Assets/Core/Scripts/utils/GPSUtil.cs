using UnityEngine;
using System;

namespace ZGGame
{

    public class GPSUtil
    {

        public static float getVecAngle(Vector2 v)
        {
            float a = 0;
            if (v.y == 0.0f)
            {
                a = v.x > 0 ? 90f : -90f;
            }
            else
                a = Mathf.Atan(v.x/v.y)*180f/Mathf.PI;
            if(a<0)
                a += 360f;
            return a;
        }

        public static double gps2d(double lat_a, double lng_a, double lat_b, double lng_b)
        {
            double d = 0;
            lat_a = lat_a * Math.PI / 180;
            lng_a = lng_a * Math.PI / 180;
            lat_b = lat_b * Math.PI / 180;
            lng_b = lng_b * Math.PI / 180;

            d = Math.Sin(lat_a) * Math.Sin(lat_b) + Math.Cos(lat_a) * Math.Cos(lat_b) * Math.Cos(lng_b - lng_a);
            d = Math.Sqrt(1 - d * d);
            d = Math.Cos(lat_b) * Math.Sin(lng_b - lng_a) / d;
            d = Math.Asin(d) * 180 / Math.PI;

            return d;
        }

        /// <summary>
        /// 获取两个经纬点的夹角
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static double getAngle(Vector2 source, Vector2 target)
        {
            double dRotateAngle = Math.Atan2(Math.Abs(target.x - source.x), Math.Abs(target.y - source.y));
            if (target.x >= source.x)//next在东边
            {

                if (target.y >= source.y)//高纬度
                {
                }
                else
                {
                    dRotateAngle = Math.PI - dRotateAngle;
                }
            }
            else
            {
                if (target.y >= source.y)
                {
                    dRotateAngle = 2 * Math.PI - dRotateAngle;
                }
                else
                {
                    dRotateAngle = Math.PI + dRotateAngle;
                }
            }
            dRotateAngle = dRotateAngle * 180 / Math.PI;
            return dRotateAngle;
        }


        public static double AngleBetween(Vector2 from, Vector2 to)
        {
            double sin = from.x * to.y - to.x * from.y;
            double cos = from.x * to.x + from.y * to.y;
            return Math.Atan2(sin, cos) * (180 / Math.PI);
        }

        private const double EARTH_RADIUS = 6378137;//地球半径米
        public static double getDistance(double lat1, double lng1, double lat2, double lng2)
        {
            double radLat1 = rad(lat1);
            double radLat2 = rad(lat2);
            double a = radLat1 - radLat2;
            double b = rad(lng1) - rad(lng2);

            double s = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) +
             Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2)));
            s = s * EARTH_RADIUS;
            s = Math.Round(s * 10000) / 10000;
            return s;
        }

        private static double rad(double d)
        {
            return d * Math.PI / 180.0;
        }

    }
}
