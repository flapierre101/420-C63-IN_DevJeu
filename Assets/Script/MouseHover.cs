using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Text TextLabel;
    private Color defaultColor;
    private Font defaultFont;
    private Color selectedColor;


    private void Awake()
    {
        TextLabel = gameObject.GetComponent<Text>();
        defaultFont = TextLabel.font;
        defaultColor = TextLabel.color;
        selectedColor = Color.red;
        //SelectedFont = (Font)Resources.Load("Fonts/GhastlyPixe");
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        //TextLabel.font = SelectedFont;
        TextLabel.color = selectedColor;
        TextLabel.text += "<";

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TextLabel.font = defaultFont;
        TextLabel.color = defaultColor;
        TextLabel.text = TextLabel.text.Remove(TextLabel.text.Length - 1);

    }
}
