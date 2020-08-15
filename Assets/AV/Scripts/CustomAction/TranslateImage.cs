// HutongGames.PlayMaker.Actions.TranslateImage
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using UnityEngine;

[ActionCategory("Custom")]
public class TranslateImage : FsmStateActionAdvanced
{
    private const float matchHeight = 1334f;

    private const float matchWidth = 750f;

    public FsmOwnerDefault gameObject;

    private Transform _transform;

    public override void Reset()
    {
        this.gameObject = null;
    }

    public override void OnEnter()
    {
        GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
        if ((Object)ownerDefaultTarget != (Object)null)
        {
            this._transform = ownerDefaultTarget.GetComponent<Transform>();
        }
        this.DoTranslate();
        if (!base.everyFrame)
        {
            base.Finish();
        }
    }

    public override void OnActionUpdate()
    {
        this.DoTranslate();
    }

    public void DoTranslate()
    {
        RectTransform rectTransform = (RectTransform)this._transform;
        Vector2 vector = this.CalcOffset((RectTransform)this._transform.parent);
        float num = (float)Screen.height / 1334f;
        float num2 = (float)Screen.width / 750f;
        Vector3 b = new Vector3((float)Screen.width / num, 1334f, 0f) / 2f;
        Vector3 vector2 = Input.mousePosition / num - b;
        RectTransform rectTransform2 = rectTransform;
        Vector3 mousePosition = Input.mousePosition;
        float x = mousePosition.x / num;
        Vector3 mousePosition2 = Input.mousePosition;
        rectTransform2.anchoredPosition3D = new Vector3(x, mousePosition2.y / num2, 0f);
    }

    public Vector2 CalcOffset(RectTransform parent)
    {
        Vector2 vector = Vector2.zero;
        Canvas component = ((Component)parent).GetComponent<Canvas>();
        if ((Object)parent.parent != (Object)null)
        {
            if ((Object)component != (Object)null)
            {
                return vector;
            }
            vector = this.CalcOffset((RectTransform)parent.parent);
        }
        parent.SetAsLastSibling();
        return vector + parent.anchoredPosition;
    }
}
