using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class Painter : MonoBehaviour
{
    [SerializeField]
    Transform canvas;

    //[SerializeField]
    //private float brushSize = 24f;

    [SerializeField]
    private float brushSizeVariation = 1f;

    [SerializeField]
    private Image[] inks;
    private Image selectedInk;

    public GameObject brushCursor, brushContainer; //The cursor that overlaps the model and our container for the brushes painted
    public Camera sceneCamera, canvasCam;  //The camera that looks at the model, and the camera that looks at the canvas.
    public Sprite cursorPaint, cursorDecal; // Cursor for the differen functions 
    public RenderTexture canvasTexture; // Render Texture that looks at our Base Texture and the painted brushes
    public Material baseMaterial; // The material of our base texture (Were we will save the painted texture)

    float brushSize = 1.0f; //The size of our brush
    Color brushColor; //The selected color
    int brushCounter = 0, MAX_BRUSH_COUNT = 1000; //To avoid having millions of brushes
    bool saving = false; //Flag to check if we are saving the texture


    public void SetInk(Image selectedInk)
    {
        foreach (Image ink in this.inks)
        {
            if (ink == selectedInk)
                this.selectedInk = ink;
        }
    }

    public void SetBrushSize()
    {
        if (Input.mouseScrollDelta.y > 0)
            this.brushSize += this.brushSizeVariation;

        else if (Input.mouseScrollDelta.y < 0)
            this.brushSize -= this.brushSizeVariation;
    }

    public void Draw(Image image)
    {
        Image inkPoint = Instantiate(selectedInk, Input.mousePosition, Quaternion.identity, this.canvas);
        //inkPoint.rectTransform.sizeDelta = new Vector2(this.brushSize, this.brushSize);
    }

    private void saveTexture()
    {
        this.brushCounter = 0;

        RenderTexture.active = canvasTexture;

        Texture2D tex = new Texture2D(canvasTexture.width, canvasTexture.height, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(0, 0, canvasTexture.width, canvasTexture.height), 0, 0);
        tex.Apply();

        RenderTexture.active = null;

        baseMaterial.mainTexture = tex;

        foreach (Transform child in brushContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
