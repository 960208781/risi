using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using System;

class JPGUrl2ImageTargetUrl
{
    public bool isLocalImage;
    public string JpgNameURL;
    public string ImageTargetURL;
}


public class DynamicLoadTest : MonoBehaviour {

    List<JPGUrl2ImageTargetUrl> jpgUrl2targetUrl = new List<JPGUrl2ImageTargetUrl>();
    private string errorStr = "";
    void Start () {
        //方式一、Marker图片在StreamingAsset， 模型在本地
        JPGUrl2ImageTargetUrl obj1 = new JPGUrl2ImageTargetUrl();
        //Marker图片路径（jpg图片）
        obj1.JpgNameURL = VoidARUtils.GetStreamDirForWWW() + "1yuan.jpg";
        //模型assetbundle（需要区分平台）
        obj1.ImageTargetURL = VoidARUtils.GetStreamingAssetbuddleResourcePath() + "CubeTarget.assetbundle";

        Debug.Log("obj1.JpgNameURL " + obj1.JpgNameURL);
        Debug.Log("obj1.ImageTargetURL " + obj1.ImageTargetURL);
        obj1.isLocalImage = true;
        jpgUrl2targetUrl.Add(obj1);

        /*
        //方式二、Marker图片在StreamingAsset, 模型在HTTP远程
        JPGUrl2ImageTargetUrl obj2 = new JPGUrl2ImageTargetUrl();
        obj2.JpgNameURL = VoidARUtils.GetStreamDirForWWW() + "/10yuan_back.jpg";
        obj2.ImageTargetURL = "http://YOUR_ASSET_SERVER/YOUR_ASSETBUNDLE";
        obj2.isLocalImage = true;
        jpgUrl2targetUrl.Add(obj2);

        //方式三、Marker图片在HTTP远程， 模型在本地
        JPGUrl2ImageTargetUrl obj3 = new JPGUrl2ImageTargetUrl();
        obj3.JpgNameURL = "http://YOUR_ASSET_SERVER/IMAGE_NAME.jpg";
		obj3.ImageTargetURL = VoidARUtils.GetStreamingAssetbuddleResourcePath() + "ImageTarget.assetbundle";
        obj3.isLocalImage = false;
        jpgUrl2targetUrl.Add(obj3);

        //方式四、Marker图片和模型都在HTTP远程
        JPGUrl2ImageTargetUrl obj4 = new JPGUrl2ImageTargetUrl();
        obj4.JpgNameURL = "http://YOUR_ASSET_SERVER/IMAGE_NAME.jpg";
        obj4.ImageTargetURL = "http://YOUR_ASSET_SERVER/YOUR_ASSETBUNDLE";
        obj4.isLocalImage = false;
        jpgUrl2targetUrl.Add(obj4);
        */
    }

    void OnGUI()
    {
        var btnHeight = Screen.height * 0.1f;
        var btnWidth = btnHeight * 2f;
        var gap = 20;
        if (GUI.Button(new Rect(Screen.width - btnWidth, gap, btnWidth, btnHeight), "添加目标"))
        {
            Debug.Log(" add marker");
            StartCoroutine(LoadGameObjects());
        }

        if (GUI.Button(new Rect(Screen.width - btnWidth, gap * 2 + btnHeight, btnWidth, btnHeight), "清空目标"))
        {
            //删除1元的marker
            string markName = jpgUrl2targetUrl[0].JpgNameURL;
            string markBinName = jpgUrl2targetUrl[0].JpgNameURL+".bin";
            if (VoidAR.GetInstance().isMarkerExist(markName))
            {
                VoidAR.GetInstance().removeTarget(markName);
                errorStr = "";
            }

            if (VoidAR.GetInstance().isMarkerExist(markBinName))
            {
                VoidAR.GetInstance().removeTarget(markBinName);
                errorStr = "";
            }
        }

        if (errorStr != "") {
            GUI.color = Color.red;
            GUI.Label(new Rect(50, 0, Screen.width - 100, 60), errorStr);
        }
    }

    private IEnumerator LoadGameObjects()
    {
        List<VoidAR.Image2ImageTarget> image2ImageTargetes = new List<VoidAR.Image2ImageTarget>();
        foreach (JPGUrl2ImageTargetUrl url in jpgUrl2targetUrl)
        {
      
            VoidAR.Image2ImageTarget.targetType  resType    =   VoidAR.Image2ImageTarget.targetType.ImageType;
            string markerName    = url.JpgNameURL;
            string markerBinName = url.JpgNameURL + ".bin";

            VoidAR.Image2ImageTarget obj = new VoidAR.Image2ImageTarget();
            WWW file = new WWW(markerBinName);
            yield return file;

            if (!string.IsNullOrEmpty(file.error))
            {
             
                file = new WWW(markerName);
                yield return file;
                if (!string.IsNullOrEmpty(file.error))
                {
                    Debug.Log("Can not Load " + url.JpgNameURL);
                   // isResExist = false;
                    continue;
                }
                else
                {
                    Debug.Log("load image 3");
                    obj.type = VoidAR.Image2ImageTarget.targetType.ImageType;
                    obj.imageUrl = markerName;
                    Texture2D tex = new Texture2D(2, 2);
                    tex.LoadImage(file.bytes);
                    obj.isMarkerLocal = url.isLocalImage;
                    obj.imagewidth = tex.width;
                    obj.imageheight = tex.height;
                    obj.imagedata = VoidARUtils.Color32ArrayToByteArray(tex.GetPixels32());

                }
            }
            else
            {

                Debug.Log("load image 4");
                obj.isMarkerLocal = url.isLocalImage;
                obj.imagedata = file.bytes;
                obj.binarySize = file.bytes.Length;
                obj.type = VoidAR.Image2ImageTarget.targetType.BinaryType;
                obj.imageUrl = markerBinName;
            }

                      
            {
                WWW bundle = new WWW(url.ImageTargetURL);
                yield return bundle;

                if (!string.IsNullOrEmpty(bundle.error))
                {
                    Debug.Log("Can not Load " + url.ImageTargetURL);
                    errorStr = "加载assetbundle失败，请选中资源Assets/VoidARDemo/Prefabs/CubeTarget后，执行菜单VoidAR/AssetBundleBuilder生成对应平台的assetbundle后再运行";
                    continue;
                }
                else
                {
                    obj.ImageTarget = (GameObject)Instantiate(bundle.assetBundle.mainAsset);
                    image2ImageTargetes.Add(obj);
                    bundle.assetBundle.Unload(false);
                }

            }


        
        }
        VoidAR.GetInstance().addTargets(image2ImageTargetes);
    }
}
