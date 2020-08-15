using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using message;
using UnityEngine.EventSystems;
using System;
using System.Text.RegularExpressions;

public class BoxItem : MonoBehaviour, IPointerClickHandler
{
    public Text attrName;
    public Text distance;
    public RawImage attrIcon;
    public Image attrIconbg;
    public RectTransform bg;

    public List<Image> starList;
    public List<Sprite> starIconList;
    public List<Texture2D> iconList;
    public POIData data { get; set; }

    public void Set()
    {
        attrName.text = data.name;
        var len = data.name.Length;
        if (len > 6)
        {
            var num = len - 6;
            bg.sizeDelta = new Vector2(bg.sizeDelta.x + num * 70, bg.sizeDelta.y);
        }

        this.name = data.id;

        if (data.distance <= 1000)
        {
            distance.text = data.distance + " m";
        }
        else
        {
            distance.text = (int)(data.distance * 100 / 1000) / 100f + " km";
        }

        SetImage(data.type, data.name);

        //设置名称
        //setBJLen(cityName.text);
        //显示星级 获取小数点前面的数为 整的星星   小数点后面的数不等于0为半颗星星
        if (data.rating != "JsonData array")
        {
            float rating = float.Parse(data.rating.ToString());
            int i = 0;
            for (i = 0; i < (int)rating; i++)
            {
                if (i < starList.Count)
                    starList[i].sprite = starIconList[0];
            }
            if (rating % (int)rating > 0)
            {
                if (i < starList.Count)
                    starList[i].sprite = starIconList[2];
            }
        }

    }


    public void SetImage(string type, string name)
    {
        var st_name = NPinyin.Pinyin.GetPinyin(name);
        st_name = Regex.Replace(st_name, @"\s", "");

        var st_type = NPinyin.Pinyin.GetPinyin(type);
        st_type = Regex.Replace(st_type, @"\s", "");
        var im = iconList.Find(s => st_type.Contains(s.name));
        if (im != null)
        {
            attrIcon.texture = im;
        }
        else
        {
            im = iconList.Find(s => st_name.Contains(s.name));
            if (im != null)
            {
                attrIcon.texture = im;
            }
            else
            {

                // Debug.LogError(st_name + "  " + st_type);
            }
        }
    }


    public Action<BoxItem> OnSelect;
    private int state = 0;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (OnSelect != null)
            OnSelect(this);
    }

    public Color StringToColor(string hex)
    {
        Color color = Color.white;
        ColorUtility.TryParseHtmlString(hex, out color);
        return color;
    }

}
