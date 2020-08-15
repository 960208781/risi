// ScreenTap
using UnityEngine;
using UnityEngine.UI;

public class ScreenTap : MonoBehaviour
{
    private const float matchHeight = 1334f;

    private const float matchWidth = 750f;

    private float time;

    private Vector3 position;

    private bool isui;

    public InputAutosize autiosize;

    public GameObject closeBtn;

    private void Start()
    {
        this.autiosize = base.GetComponentInChildren<InputAutosize>(true);
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.time = Time.time;
            this.position = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            int num = 0;
            float num2 = Vector3.Distance(this.position, Input.mousePosition);
            if ((float)num < 1.5f && num2 < 10f)
            {
                if (this.autiosize.gameObject.activeSelf && this.autiosize.field.text.Length > 2 && !this.autiosize.pointerDown)
                {
                    this.autiosize.field.interactable = false;
                    ((Component)this.autiosize.field).GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
                    this.closeBtn.SetActive(false);
                }
                if (!this.autiosize.gameObject.activeSelf)
                {
                    this.autiosize.gameObject.SetActive(true);
                    ((Component)this.autiosize.field).GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                    this.closeBtn.SetActive(true);
                    this.DoTranslate();
                }
                else if (!this.autiosize.closeDown)
                {
                    this.autiosize.gameObject.SetActive(true);
                }
                else
                {
                    this.autiosize.field.text = string.Empty;
                    this.autiosize.gameObject.SetActive(false);
                    this.autiosize.closeDown = false;
                    Object.Destroy(base.gameObject);
                }
            }
            FingerGestures fingerGestures = Object.FindObjectOfType<FingerGestures>();
            fingerGestures.enabled = true;
        }
    }

    public void DoTranslate()
    {
        RectTransform rectTransform = (RectTransform)this.autiosize.transform;
        Vector2 vector = this.CalcOffset((RectTransform)this.autiosize.transform.parent);
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
