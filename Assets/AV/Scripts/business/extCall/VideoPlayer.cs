using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class VideoPlayer : VoidVideoPlayerBase
{
    public bool isAlpha = false;
    public Camera renderCam;
    private bool fullScreen = false;
    private Vector3 origin_size = Vector3.zero;
    private Quaternion origin_Rota = Quaternion.identity;
    public void SetUrl(string url)
    {
        isAlpha = url.Contains("alpha");
        string matName = isAlpha ? "video_alpha" : "videoplay_material";
        Material vmat = (Material)Resources.Load(matName, typeof(Material));
        GetComponent<Renderer>().sharedMaterial = vmat;
        GetComponent<Renderer>().enabled = false;

        char[] separator = new char[] { '|' };
        string[] strArray = url.Split(separator);
        this.url = strArray[0];
        this.AddEventListener(VoidAREvent.READY, new UnityAction<VoidAREvent>(this.OnPlayerReady));

    }
    protected void OnPlayerReady(VoidAREvent evt)
    {
        Debug.LogError("VideoPlay: Ready");
        var vd = (VideoPlayer)evt.currentTarget;
        var vdRender = vd.GetComponent<Renderer>();
        vdRender.enabled = true;
        vdRender.sharedMaterial.mainTexture = vd.texture;
        var vwidth = vd.isAlpha ? vd.videoWidth / 2 : vd.videoWidth;
        FitPlane(vd.transform, vwidth, vd.videoHeight);

        origin_size = transform.localScale;
        origin_Rota = transform.localRotation;

        AddScaleFinger();
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

    private void AddScaleFinger()
    {
        var scale = GetComponent<TBPinchToScale>();
        if (scale)
            Destroy(scale);
        gameObject.AddComponent<TBPinchToScale>();
    }

    public void OnTap(TapGesture tap)
    {
        if (MainController.Ins.isTrack)
            return;
        Debug.LogError("xxxxxxxxxxxxxxxxxxxxxxx");
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
        }
        else
        {
            transform.localRotation = origin_Rota;
            transform.localScale = origin_size;
            fullScreen = false;
        }
        AddScaleFinger();
    }

    void OnDestroy()
    {
        _helper.VideoPlayerExit();
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (!pauseStatus)
        {
            this._helper.VideoPlay();
        }
        else
        {
            this._helper.VideoPause();
        }
    }
}