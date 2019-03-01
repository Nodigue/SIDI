using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TexturesController : MonoBehaviour
{
    [SerializeField]
    private BodyPartsController BodyPartsController;

    [SerializeField]
    private Texture2D skin;
    private Sprite[] sprites;

    private void Awake()
    {
        if (skin != null)
            this.sprites = Resources.LoadAll <Sprite> ("Characters/" + this.skin.name);
    }

    private void Start()
    {
        if (skin != null) {
      
            foreach (Sprite s in sprites)
                this.BodyPartsController.GetBodyPart(s.name).GetComponent<SpriteRenderer>().sprite = s;
        }
    }
}
