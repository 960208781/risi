using UnityEngine;
using System.Collections;
using ZGGame;
using System.Collections.Generic;
using message;
using System;
public class NewViewObjScript : MonoBehaviour
{
    public GameObject viewObj_p;

    /// <summary>
    /// 预制体
    /// </summary>
    public GameObject BackGround;
    /// <summary>
    /// 中间背景
    /// </summary>
    public GameObject In;
    /// <summary>
    /// 中间右边
    /// </summary>
    public GameObject InRight;
    /// <summary>
    /// 左边背景
    /// </summary>
    public GameObject Left;
    /// <summary>
    /// 右边背景
    /// </summary>
    public GameObject Rigjt;
    /// <summary>
    /// 数据
    /// </summary>
    private POIData mo;
    /// <summary>
    /// 名字
    /// </summary>
    public TextMesh cityName;
    /// <summary>
    /// 距离
    /// </summary>
    public TextMesh lentext;
    /// <summary>
    /// ico
    /// </summary>
    public GameObject te_ico;
    /// <summary>
    /// 指针
    /// </summary>
    public GameObject Arrow;
    /// <summary>
    /// 星星数组
    /// </summary>
    public GameObject[] G_Stars;
    /// <summary>
    /// 星星贴图  0 全 1 半 2 无
    /// </summary>
    public Texture[] Stars;

    /// <summary>
    /// 图标
    /// </summary>
    public Texture[] ico;


    public void setMapElemVo(POIData m)
    {
        mo = m;
    }
    public POIData getPoiData()
    {
        return mo;
    }
    /// <summary>
    /// 被点击
    /// </summary>
    public void isOnMouseDown()
    {

        RadarUI.instance.toSelect(mo);
        openDetail();

    }

    public void openDetail()
    {
        CallNative.InvokeNative(CallNative.showPoiDetail, mo.desc);
        Choose();
    }


    // Use this for initialization
    void Start()
    {
        refresh();
        //Debug.Log(BackGround.transform.FindChild("Left").gameObject.name);

    }

    /// <summary>
    /// 刷新
    /// </summary>
    private void refresh()
    {
        if (mo == null)
        {
            return;
        }
        //显示名字
        cityName.text = mo.name;
        this.gameObject.name = mo.id;
        //显示距离
        if (mo.distance <= 1000)
        {
            lentext.text = mo.distance + " m";
        }
        else
        {
            lentext.text = (int)(mo.distance * 100 / 1000) / 100f + " km";
        }


        //显示ico
        if (mo.type.IndexOf("出入口") > -1)//出入口
        {
            Arrow.GetComponent<MeshRenderer>().materials[0].color = new Color(142f / 255f, 212f / 255f, 150f / 255f, 1f);

            te_ico.GetComponent<MeshRenderer>().materials[0].SetTexture("_MainTex", ico[0]);
            Debug.Log(ico[0].name);
        }
        else if (mo.type.IndexOf("地铁") > -1)//地铁
        {
            Arrow.GetComponent<MeshRenderer>().materials[0].color = new Color(57f / 255f, 110f / 255f, 222f / 255f, 1f);
            te_ico.GetComponent<MeshRenderer>().materials[0].SetTexture("_MainTex", ico[1]);
        }
        else if (mo.type.IndexOf("电影院") > -1)//电影院
        {
            Arrow.GetComponent<MeshRenderer>().materials[0].color = new Color(43f / 255f, 43f / 255f, 43f / 255f, 1f);
            te_ico.GetComponent<MeshRenderer>().materials[0].SetTexture("_MainTex", ico[2]);
        }
        else if (mo.type.IndexOf("厕所") > -1) //厕所
        {
            Arrow.GetComponent<MeshRenderer>().materials[0].color = new Color(65f / 255f, 197f / 255f, 201f / 255f, 1f);
            te_ico.GetComponent<MeshRenderer>().materials[0].SetTexture("_MainTex", ico[3]);
        }
        else if (mo.type.IndexOf("停车场") > -1)//停车场
        {
            //Debug.Log(mo.type);
            Arrow.GetComponent<MeshRenderer>().materials[0].color = new Color(118f / 255f, 138f / 255f, 188f / 255f, 1f);
            te_ico.GetComponent<MeshRenderer>().materials[0].SetTexture("_MainTex", ico[4]);
        }
        else if (mo.type.IndexOf("景点") > -1)//景点  
        {
            Arrow.GetComponent<MeshRenderer>().materials[0].color = new Color(71f / 255f, 175f / 255f, 16f / 255f, 1f);
            te_ico.GetComponent<MeshRenderer>().materials[0].SetTexture("_MainTex", ico[5]);
        }
        else if (mo.type.IndexOf("服务中心") > -1)//服务中心
        {
            Arrow.GetComponent<MeshRenderer>().materials[0].color = new Color(245f / 255f, 142f / 255f, 76f / 255f, 1f);
            te_ico.GetComponent<MeshRenderer>().materials[0].SetTexture("_MainTex", ico[6]);
        }
        else if (mo.type.IndexOf("餐饮") > -1)//餐饮
        {
            Arrow.GetComponent<MeshRenderer>().materials[0].color = new Color(255f / 255f, 78f / 255f, 0f / 255f, 1f);
            te_ico.GetComponent<MeshRenderer>().materials[0].SetTexture("_MainTex", ico[7]);
        }
        else if (mo.type.IndexOf("商场") > -1)//商铺
        {
            Arrow.GetComponent<MeshRenderer>().materials[0].color = new Color(237f / 255f, 89f / 255f, 125f / 255f, 1f);
            te_ico.GetComponent<MeshRenderer>().materials[0].SetTexture("_MainTex", ico[8]);
        }
        else if (mo.type.IndexOf("售票处") > -1)//售票处
        {
            Arrow.GetComponent<MeshRenderer>().materials[0].color = new Color(231f / 255f, 32f / 255f, 25f / 255f, 1f);
            te_ico.GetComponent<MeshRenderer>().materials[0].SetTexture("_MainTex", ico[9]);
        }
        else if (mo.type.IndexOf("自动提款机") > -1)//ATM
        {
            Arrow.GetComponent<MeshRenderer>().materials[0].color = new Color(46f / 255f, 76f / 255f, 164f / 255f, 1f);
            te_ico.GetComponent<MeshRenderer>().materials[0].SetTexture("_MainTex", ico[10]);
        }
        else if (mo.type.IndexOf("超市") > -1)//超市
        {
            Arrow.GetComponent<MeshRenderer>().materials[0].color = new Color(222f / 255f, 186f / 255f, 98f / 255f, 1f);
            te_ico.GetComponent<MeshRenderer>().materials[0].SetTexture("_MainTex", ico[11]);
        }
        else if (mo.type.IndexOf("交通") > -1)//交通
        {
            Arrow.GetComponent<MeshRenderer>().materials[0].color = new Color(236f / 255f, 187f / 255f, 37f / 255f, 1f);
            te_ico.GetComponent<MeshRenderer>().materials[0].SetTexture("_MainTex", ico[12]);
        }
        else if (mo.type.IndexOf("酒店") > -1)//酒店
        {
            Arrow.GetComponent<MeshRenderer>().materials[0].color = new Color(98f / 255f, 222f / 255f, 174f / 255f, 1f);
            te_ico.GetComponent<MeshRenderer>().materials[0].SetTexture("_MainTex", ico[13]);
        }
        //设置名称
        setBJLen(cityName.text);
        //显示星级 获取小数点前面的数为 整的星星   小数点后面的数不等于0为半颗星星
        if (mo.rating != "JsonData array")
        {
            float rating = float.Parse(mo.rating.ToString());
            int i = 0;
            while (i < G_Stars.Length)
            {
                for (int ii = 0; ii < (int)rating; ii++)
                {
                    G_Stars[i].GetComponent<MeshRenderer>().materials[0].SetTexture(0, Stars[0]);
                    i++;
                }
                if (rating % (int)rating > 0)
                {
                    G_Stars[i].GetComponent<MeshRenderer>().materials[0].SetTexture(0, Stars[1]);
                    i++;
                }
                else
                {
                    G_Stars[i].GetComponent<MeshRenderer>().materials[0].SetTexture(0, Stars[2]);
                    i++;
                }
                i++;
            }
        }

        //Debug.Log(rating + "  " + (int)rating + "   " + rating % (int)rating);


    }
    /// <summary>
    /// 设置白色底图长度
    /// 根据名字长度
    /// </summary>
    void setBJLen(string name)
    {
        //字符个数
        int nameLength = name.Length;
        //多了几个字符
        int nameManyLength = 0;
        //加上扩大的缩放
        float ManyMultiple = 0f;
        //现在X轴位置
        float X_Position = 0f;
        //大于6个字符
        if (nameLength > 6)
        {
            //计算多出的字符
            nameManyLength = nameLength - 6;
            //计算多出倍数  多出的字符数乘以原先单个字符所占比例
            ManyMultiple = nameManyLength * (In.transform.localScale.x / 6);
            //赋值现在倍数 
            In.transform.localScale = new Vector3(In.transform.localScale.x + ManyMultiple, In.transform.localScale.y, In.transform.localScale.z);
            //计算出现在的位置
            X_Position = In.transform.localPosition.x + (ManyMultiple / 2f);
            //赋值现在位置
            In.transform.localPosition = new Vector3(X_Position, In.transform.localPosition.y, In.transform.localPosition.z);
            //设置右边
            Rigjt.transform.SetParent(InRight.transform);
            Rigjt.transform.localPosition = new Vector3(0f, 0f, 0f);
            Rigjt.transform.SetParent(BackGround.transform);
        }

    }


    public void Choose()
    {
        ShutDown();
        Left.GetComponent<MeshRenderer>().materials[0].SetColor("_Color", new Color(226f / 255f, 246f / 255f, 224f / 255f));
        In.GetComponent<MeshRenderer>().materials[0].SetColor("_Color", new Color(226f / 255f, 246f / 255f, 224f / 255f));
        Rigjt.GetComponent<MeshRenderer>().materials[0].SetColor("_Color", new Color(226f / 255f, 246f / 255f, 224f / 255f));
        cityName.color = new Color(19f / 255f, 26f / 255f, 32f / 255f);
        lentext.color = new Color(0f, 0f, 0f);
    }

    public void ShutDown()
    {
        foreach (Transform item in viewObj_p.transform)
        {
            NewViewObjScript InIsD_ = item.gameObject.GetComponent<NewViewObjScript>();
            InIsD_.cityName.color = new Color(1f, 1f, 1f);
            InIsD_.lentext.color = new Color(1f, 1f, 1f);
            InIsD_.Left.GetComponent<MeshRenderer>().materials[0].SetColor("_Color", new Color(28f / 255f, 43f / 255f, 55f / 255f));
            InIsD_.In.GetComponent<MeshRenderer>().materials[0].SetColor("_Color", new Color(28f / 255f, 43f / 255f, 55f / 255f));
            InIsD_.Rigjt.GetComponent<MeshRenderer>().materials[0].SetColor("_Color", new Color(28f / 255f, 43f / 255f, 55f / 255f));

        }
    }

}
