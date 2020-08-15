using UnityEngine;
using UnityEngine.UI;
namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Custom")]
    public class HighLight_off_on : FsmStateAction
    {
        [Tooltip("highlight")]
        public FsmOwnerDefault gameObject;
        public FsmBool On = true;
        public FsmBool SeeThrough = true;
        public FsmBool Occluder = false;
        public FsmBool flash;

        public FsmColor color;

        //public FsmBool 

        private GameObject lightTemp;
        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            var highlight = go.GetComponentInChildren<HighlightableObject>();
            var isui = go.GetComponent<Image>();
            var raw = go.GetComponent<RawImage>();

            if (highlight == null)
            {

                if (isui)
                {
                    highlight = AddChild(isui, ConvertSpriteToTexture(isui.sprite));
                }
                else
                {

                    if (raw)
                    {
                        highlight = AddChild(raw, raw.mainTexture);
                    }
                    else
                        highlight = go.AddComponent<HighlightableObject>();
                }
            }


            if (On.Value && flash.Value)
            {
                highlight.FlashingOn();

            }
            else if (On.Value && flash.Value == false)
            {
                highlight.ConstantOn(color.Value);
            }

            else
            {
                highlight.Off();
                if (Occluder.Value)
                {
                    highlight.OccluderOn();
                }
                else if (isui != null || raw != null)
                {
                    var light = go.transform.Find("light");
                    if (light) MonoBehaviour.Destroy(light.gameObject);
                }
                else
                {
                    highlight.Off();
                }
            }

            // if (SeeThrough.Value) highlight.SeeThroughOn();
            // else highlight.SeeThroughOff();
            if (Occluder.Value) highlight.OccluderOn();
            else highlight.OccluderOff();

            Finish();
        }

        HighlightableObject AddChild(MaskableGraphic isui, Texture tex)
        {
            var uu = GameObject.CreatePrimitive(PrimitiveType.Quad);
            uu.name = "light";
            uu.layer = isui.gameObject.layer;
            uu.transform.parent = isui.transform;
            uu.transform.localPosition = Vector3.zero;
            uu.transform.localRotation = Quaternion.identity;
            uu.transform.localScale = new Vector3(isui.rectTransform.rect.width, isui.rectTransform.rect.height, 1);
            var uishader = Shader.Find("Unlit/Transparent");
            var c = uu.GetComponent<Collider>();
            MonoBehaviour.Destroy(c);
            var mat = new Material(uishader);
            mat.SetTexture("_MainTex", tex);
            uu.GetComponent<Renderer>().material = mat;
            return uu.AddComponent<HighlightableObject>();
        }

        Texture2D ConvertSpriteToTexture(Sprite sprite)
        {
            try
            {
                if (sprite.rect.width != sprite.texture.width)
                {
                    Texture2D newText = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
                    Color[] colors = newText.GetPixels();
                    Color[] newColors = sprite.texture.GetPixels((int)Mathf.CeilToInt(sprite.textureRect.x),
                                                                 Mathf.CeilToInt(sprite.textureRect.y),
                                                                 Mathf.CeilToInt(sprite.textureRect.width),
                                                                 Mathf.CeilToInt(sprite.textureRect.height));
                    Debug.Log(colors.Length + "_" + newColors.Length);
                    newText.SetPixels(newColors);
                    newText.Apply();
                    return newText;
                }
                else
                    return sprite.texture;
            }
            catch
            {
                return sprite.texture;
            }
        }
    }


}
