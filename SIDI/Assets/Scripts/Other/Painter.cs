using UnityEngine;
using UnityEngine.UI;

public class Painter : MonoBehaviour
{
    [SerializeField]
    Transform canvas;

    [SerializeField]
    private float brushSize = 24f;

    [SerializeField]
    private Image[] inks;
    private Image selectedInk;

    public void SetInk(Image selectedInk)
    {
        foreach (Image ink in this.inks)
        {
            if (ink == selectedInk)
                this.selectedInk = ink;
        }
    }

    public void Draw()
    {
        Image inkPoint = Instantiate(selectedInk, Input.mousePosition, Quaternion.identity, this.canvas);
        inkPoint.rectTransform.sizeDelta = new Vector2(this.brushSize, this.brushSize);
    }
}
