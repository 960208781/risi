using UnityEngine;
using System.Collections;
using ZGGame;
using System.Collections.Generic;
using message;
using System;

public class viewObjScript : MonoBehaviour
{

    public GameObject plane;
    public GameObject planeSelect;
    public TextMesh ts;
    public TextMesh len;
    private static POIData mo;

    public GameObject g1;
    public GameObject g2;
    public GameObject g3;
    public GameObject g4;
    public GameObject g5;
    public GameObject g6;
    public GameObject g7;
    public GameObject g8;
    public GameObject g9;
    public GameObject g10;
    public GameObject ar;
    public GameObject star;
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    public GameObject star4;
    public GameObject starFull;
    public GameObject starHalf;

    private List<GameObject> listStar = new List<GameObject>();
    private List<GameObject> listicon = new List<GameObject>();
    // Use this for initial ization
    void Start()
    {
        listStar.Add(star);
        listStar.Add(star1);
        listStar.Add(star2);
        listStar.Add(star3);
        listStar.Add(star4);

        listicon.Add(g1);
        listicon.Add(g2);
        listicon.Add(g3);
        listicon.Add(g4);
        listicon.Add(g5);
        listicon.Add(g6);
        listicon.Add(g7);
        listicon.Add(g8);
        listicon.Add(g9);
        listicon.Add(g10);

        refresh();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setMapElemVo(POIData m)
    {
        mo = m;
        if (listStar.Count == 0)
        {
            return;
        }
    }

    public POIData getPoiData()
    {
        return mo;
    }

    private void refresh()
    {
        if (mo == null)
        {
            return;
        }
        setText(mo.name);
        //setText("111");
        if (mo.distance <= 1000)
        {
            len.text = mo.distance + " m";
        }
        else
        {
            len.text = (int)(mo.distance * 100 / 1000) / 100f + " km";
        }

        if (mo.typecode.StartsWith("19"))//出入口
        {
            showIcon(0);
        }
        else if (mo.typecode.StartsWith("150500"))//地铁
        {
            showIcon(1);
        }
        else if (mo.typecode.StartsWith("0806"))//电影院
        {
            showIcon(2);
        }
        else if (mo.typecode.StartsWith("11"))//景点
        {
            showIcon(3);
        }
        else if (mo.typecode.StartsWith("070000"))//服务中心
        {
            showIcon(4);
        }
        else if (mo.typecode.StartsWith("05"))//餐饮
        {
            showIcon(5);
        }
        else if (mo.typecode.StartsWith("1509"))//停车场
        {
            showIcon(6);
        }
        else if (mo.typecode.StartsWith("06"))//商铺
        {
            showIcon(7);
        }
        else if (mo.typecode.StartsWith("0703"))//售票处
        {
            showIcon(8);
        }
        else if (mo.typecode.StartsWith("200300"))//厕所
        {
            showIcon(9);
        }
        else
        {
            showIcon(1);
        }
        ar.SetActive(false);

        setStar();
    }

    private void showIcon(int idx)
    {
        for (int i = 0; i < listicon.Count; i++)
        {
            if (i == idx)
            {
                listicon[i].SetActive(true);
            }
            else
            {
                listicon[i].SetActive(false);
            }
        }
    }

    //设置星星
    public void setStar()
    {
        int sfc = Convert.ToInt32(float.Parse(mo.rating));
        int hfc = Convert.ToInt32((float.Parse(mo.rating) * 10) % 2) == 0 ? 0 : 1;



        for (int i = 0; i < sfc; i++)
        {
            GameObject go = GameObject.Instantiate(starFull);
            Transform tgo = listStar[i].transform;
            go.transform.SetParent(tgo.parent);
            go.transform.localPosition = new Vector3(tgo.localPosition.x, 0.4f, -0.02f);
            go.transform.localRotation = star1.transform.localRotation;

            go.SetActive(true);
        }

        if (hfc != 0)
        {
            GameObject go = GameObject.Instantiate(starHalf);
            Transform tgo = listStar[sfc].transform;
            go.transform.SetParent(tgo.parent);
            go.transform.localPosition = new Vector3(tgo.localPosition.x, 0.4f, -0.02f);
            go.transform.localRotation = star1.transform.localRotation;
            go.SetActive(true);
        }
    }
    //设置文字
    public void setText(string strText)
    {
        int iLen = strText.Length;
        if (iLen < 8)
        {
            iLen = 8;
        }

        ts.text = strText;
        float sca = (iLen * 3 + 11) / 10f;
        plane.transform.localScale = new Vector3(sca, 1, 1.2f);

        planeSelect.transform.localScale = new Vector3(sca, 1, 1.2f);

        foreach (GameObject go in listicon)
        {
            go.transform.localPosition = new Vector3(-sca * 5 + 6, 0, -0.01f);
        }
        ar.transform.localPosition = new Vector3(sca * 5, 5.5f, -0.01f);
        ts.transform.localPosition = new Vector3(-sca * 5 + 11 + 1, 4.5f, -0.01f);
        len.transform.localPosition = new Vector3(-sca * 5 + 11 + 1, -1.9f, -0.01f);
        float startX = -sca * 5 + 11 + 1 + 1.1f;
        star.transform.localPosition = new Vector3(startX, 0.4f, -0.01f);
        startX += 2.5f;
        star1.transform.localPosition = new Vector3(startX, 0.4f, -0.01f);
        startX += 2.5f;
        star2.transform.localPosition = new Vector3(startX, 0.4f, -0.01f);
        startX += 2.5f;
        star3.transform.localPosition = new Vector3(startX, 0.4f, -0.01f);
        startX += 2.5f;
        star4.transform.localPosition = new Vector3(startX, 0.4f, -0.01f);
    }


    void OnMouseDown()
    {
        Debug.Log("onMouseDown");
        RadarUI.instance.toSelect(mo);
        openDetail();
    }

    public void openDetail()
    {
        CallNative.InvokeNative(CallNative.showPoiDetail, mo.desc);
    }
}
