using UnityEngine;
using System.Collections;
using System.IO;

public class ImagePlayer : MonoBehaviour
{

    public Camera renderCam;
    private bool fullScreen = false;
    private Vector3 origin_size = Vector3.zero;
    private Quaternion origin_Rota = Quaternion.identity;

    public void SetUrl(string url)
    {
        CallNative.InvokeNative(CallNative.showLoading, "请稍候");
        GetComponent<Renderer>().material = (Material)Resources.Load("image_material", typeof(Material));
        StartCoroutine(SetNetImage(gameObject, url));
    }

    void FitPlane(Transform vTran, int width, int height)
    {
        var wh = width / (height * 1.0f);
        if (MainController.Ins.currentTarget != null && MainController.Ins.currentTarget.meta.isTrack == true)
        {
            vTran.transform.localScale = new Vector3(0.1f, 1, 0.1f / wh);
            renderCam.gameObject.SetActive(false);
        }
        else
        {
            var size = Tools.GetPlaneSize(renderCam, vTran);
            vTran.localScale = new Vector3(size.x * 0.1f, 1, size.x / wh * 0.1f);
        }
    }

    private IEnumerator SetNetImage(GameObject mark, string imgurl)
    {
        var index = imgurl.LastIndexOf("/");
        var name = imgurl.Substring(index + 1, imgurl.Length - index - 1);
        var path = BufferCtrl.imgDir + "/" + name;
        var islocal = File.Exists(path);
        if (islocal)
        {
            imgurl = "file:///" + path;
        }
        WWW imgwww = new WWW(imgurl);
        yield return imgwww;
        var render = mark.GetComponent<Renderer>();
        Texture2D tex = new Texture2D(1, 1);
        imgwww.LoadImageIntoTexture(tex);

        if (!islocal)
        {
            File.WriteAllBytes(path, imgwww.bytes);
        }
        FitPlane(transform, tex.width, tex.height);
        origin_size = transform.localScale;
        origin_Rota = transform.localRotation;

        render.material.SetTexture("_MainTex", tex);
        mark.GetComponent<Renderer>().enabled = true;
        imgwww.Dispose();

        CallNative.InvokeNative(CallNative.closeLoading, "");
        CallNative.InvokeNative(CallNative.arLoaded);
    }
    public void OnTap(TapGesture tap)
    {
        if (MainController.Ins.currentTarget.meta.isTrack)
            return;

        if (fullScreen == false)
        {
            var size = Tools.GetPlaneSize(renderCam, transform);
            var wh = origin_size.z / origin_size.x;
            var s = Screen.width / (Screen.height * 1.0f);
            if (wh > s)
            {
                transform.localScale = new Vector3(size.x / wh * 0.1f, 0, size.x * 0.1f);
            }
            else
            {
                transform.localScale = new Vector3(size.y * 0.1f, 0, size.y * wh * 0.1f);
            }
            transform.localRotation = Quaternion.Euler(0, -90, 0);
            transform.localPosition = Vector3.zero;
            fullScreen = true;
            GetComponent<TBPinchToScale>().enabled = false;
        }
        else
        {
            transform.localRotation = origin_Rota;// Quaternion.Euler(0, 180, 0);
            transform.localScale = origin_size;
            fullScreen = false;
            GetComponent<TBPinchToScale>().enabled = true;
        }
    }

}
