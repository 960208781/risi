// InputAutosize
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputAutosize : MonoBehaviour, IEndDragHandler, IPointerClickHandler, IPointerDownHandler, IEventSystemHandler
{
    public InputField field;

    public Button closeBtn;

    public Text heightTxt;

    public bool closeDown;

    private bool p;

    public bool pointerDown
    {
        get
        {
            return this.p;
        }
        set
        {
            this.p = value;
        }
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        ((Component)this.field).GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        this.closeBtn.gameObject.SetActive(true);
        this.field.interactable = true;
        this.field.OnSelect(eventData);
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        FingerGestures fingerGestures = Object.FindObjectOfType<FingerGestures>();
        fingerGestures.enabled = false;
        this.pointerDown = true;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        this.pointerDown = false;
    }

    private void Start()
    {
        this.field = base.GetComponentInChildren<InputField>();
        this.field.interactable = false;
        this.field.onValueChanged.AddListener(this.ResetSize);
        this.field.onEndEdit.AddListener(this.onEndEdit);
    }

    public void CloseText()
    {
        this.field.text = string.Empty;
        base.gameObject.SetActive(false);
        this.pointerDown = true;
    }

    private void onEndEdit(string endstr)
    {
        this.field.interactable = false;
        this.pointerDown = false;
    }

    public void ResetSize(string s)
    {
        this.heightTxt.text = s;
        RectTransform component = base.GetComponent<RectTransform>();
        RectTransform rectTransform = component;
        Vector2 sizeDelta = component.sizeDelta;
        rectTransform.sizeDelta = new Vector2(sizeDelta.x, this.heightTxt.preferredHeight + 20f);
    }

    /// <summary>
    /// 获取字符长度（中文为两个字符长度）
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static float GetSizeOfWord(string s)
    {
        int num = 0;
        for (int i = 0; i < s.Length; i++)
        {
            char c = s[i];
            Regex regex = new Regex("^[一-龥]$");
            num = ((!regex.IsMatch(c.ToString())) ? (num + 1) : (num + 2));
        }
        return (float)num;
    }
}
