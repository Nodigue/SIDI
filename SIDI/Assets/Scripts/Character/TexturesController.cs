using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TexturesController : MonoBehaviour
{
    [SerializeField]
    private BodyPartsController body_part_controller;

    public Texture2D skin;
    private Sprite[] sprites;

    private void Awake()
    {
        this.sprites = Resources.LoadAll <Sprite> ("Characters/" + this.skin.name);
    }

    private void Start()
    {
        foreach (Sprite s in sprites)
            this.body_part_controller.GetBodyPart(s.name).GetComponent<SpriteRenderer>().sprite = s;
    }
}
