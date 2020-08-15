using UnityEngine;
using System.IO;
#if UNITY_5_6 || UNITY_2017
using UnityEngine.Video;
#endif
public class VideoPlayBehaviour : VideoPlayBase
{
#if UNITY_EDITOR
#if UNITY_5_6 || UNITY_2017 //5.6后支持Uinty Editor中用Unity自带VideoPlay运行
    private VideoPlayer _editorPlayer;
        private VideoPlayer editorPlayer {
            get {
                if (!_editorPlayer) {
                    _editorPlayer = gameObject.AddComponent<VideoPlayer>();
                }
                return _editorPlayer;
            }
        }
        protected override int GetVideoWidth()
        {
            return editorPlayer.texture.width;
        }

        protected override int GetVideoHeight()
        {
            return editorPlayer.texture.height;
        }

        protected override void VideoPlay()
        {
            editorPlayer.Play();
        }

        protected override void VideoPause()
        {
            editorPlayer.Pause();
        }

        protected override bool isPrepared()
        {
            return editorPlayer.isPrepared;
        }

        protected override Texture GetTexture()
        {
            return editorPlayer.texture;
        }

        protected override void VideoPlayerInit()
        {
            editorPlayer.source = VideoSource.Url;
            editorPlayer.playOnAwake = false;
            editorPlayer.renderMode = VideoRenderMode.MaterialOverride;
            editorPlayer.targetMaterialProperty = "_MainTex";
            var _vplayer = GetComponent<VoidVideoPlayerBase>();
            _vplayer.enabled = false;
            //相对路径转绝对路径
            if (!_vplayer.url.StartsWith("http"))
            {
                editorPlayer.url = Path.Combine(VoidARUtils.GetStreamDirForWWW(), _vplayer.url);
            }
            else {
                editorPlayer.url = _vplayer.url;
            }
            editorPlayer.prepareCompleted += (VideoPlayer vp) => {
                Debug.Log("Video play start");
                updateShaderProperties();
                if (GetActive())
                {
                    GetComponent<Renderer>().enabled = true;
                    Debug.Log("arplayer SetActive OnPlayerReady play");
                }
            };
        }
#else
    protected override void VideoPlayerInit() { }
    protected override bool isPrepared() { return false; }
    protected override void VideoPlay(){}
    protected override void VideoPause() { }
    #endif
#endif
}
