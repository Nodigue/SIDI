using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class Painter : MonoBehaviour
{
    //////////////////////////////
    /// Attributs de la classe ///
    //////////////////////////////

    //Image sur laquelle on dessine
    private Image image;
    //Sprite de l'image
    private Sprite sprite;
    //Texture du sprite (RGBA 32Bits)
    private Texture2D texture;

    //Tableau de pixel correspondant à la texture
    private Color32[] colors;

    //////////////////////////////
    /// Fonctions de coloriage ///
    //////////////////////////////

    //Application des changements
    private void Apply_changes()
    {
        //On associe le nouveau tableau de pixels à la texture
        texture.SetPixels32(colors);
        //On applique les changements
        texture.Apply();
    }

    //Colorie un pixel aux coordoonnés "x" et "y" de couleur "color"
    private void Color_pixel(int x, int y, Color color)
    {
        //On calcule l'index du pixel dans le tableau à partir de ses coordonnées
        int index = x + (y * this.texture.width);
        
        //On s'assure qu'on est dans les limites de la texture
        if (x > 0 && x < this.texture.width || y > 0 && y < this.texture.height)
        {
            //On s'assure que l'index soit dans les limites du tableau de pixel
            if (index > 0 && index < this.colors.Length)
            {
                //On change la couleur de ce pixel
                this.colors[index] = color;
            }
        }
    }

    //Colorie un cercle plein de centre "center", de rayon "radius" et de couleur "color"
    private void Color_Area(Vector2 center, int radius, Color color)
    {
        //On récupère les coodornées du pixels 'center" séparement
        int center_x = (int) center.x;
        int center_y = (int) center.y;

        //On parcourt les pixels présents dans un carré autour du point "center"
        for (int x = center_x - radius; x < center_x + radius; x++)
        {
            for (int y = center_y - radius; y < center_y + radius; y++)
            {
                //On ne dessine que les pixels qui sont dans le cercle de centre "center" et de rayon "radius"
                if ((x - center_x) * (x - center_x) + (y - center_y) * (y - center_y) < radius * radius)
                    Color_pixel(x, y, color);
            }
        }
    }

    ////////////////////////////////
    /// Fonctions pour la souris ///
    ////////////////////////////////

    //Calcule le pixel de l'image associé à la position de la souris à l'écran.
    private Vector2 Mouse_screen_to_image_pixel(Vector2 mouse_screen_position)
    {
        //Position de la souris en coordonnées locale.
        Vector2 mouse_local_position;

        //Coordonnées du pixel par rapport l'écran (coordonées relatives à la taille de l'écran).
        Vector2 image_pixel_screen;
        //Coordonnées du pixel par rapport à l'image (comprises entre 0 et 1 / (0, 0) étant en bas à droite).
        Vector2 image_pixel;

        //On calcule les coordonées locale de la souris dans le rectangle de l'image.
        //Retourne 1 si la souris est dans le rectangle de l'image, 0 sinon.
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(this.image.rectTransform, mouse_screen_position, null, out mouse_local_position))
        {
            //On en déduit l'offset du point de pivot du rectangle de l'image
            Vector2 offset = new Vector2(mouse_local_position.x - this.image.rectTransform.rect.x, mouse_local_position.y - this.image.rectTransform.rect.y);

            //Si l'image à son aspect préservée
            if (this.image.preserveAspect)
            {
                //On calcule le ratio du rectangle de l'image
                float rect_ratio = this.image.rectTransform.rect.height / this.image.rectTransform.rect.width;
                //On calcule le ratio du sprite de l'image (dans le cas d'un sprite carré, le ratio vaut 1)
                float image_ratio = this.sprite.rect.height / this.sprite.rect.width;
                
                Rect image_rect;

                //Si le ratio du sprite est supérieur à celui du rectangle, c'est que l'image est contrainte par sa hauteur
                if (image_ratio > rect_ratio)
                {
                    //On calcule la largeur de l'image
                    float image_width = (rect_ratio / image_ratio) * this.image.rectTransform.rect.width;
                    //On calcule l'excédent de largeur
                    float excess_width = this.image.rectTransform.rect.width - image_width;

                    //On définit alors un nouveau rectangle pour l'image
                    image_rect = new Rect(this.image.rectTransform.pivot.x * excess_width, 0, this.image.rectTransform.rect.height / image_ratio, this.image.rectTransform.rect.height);
                }
                //Si le ratio du sprite est inférieur à celui du rectangle, c'est que l'image est contrainte par sa largeur
                else
                {
                    //On calcule la hauteur de l'image
                    float image_height = (image_ratio / rect_ratio) * this.image.rectTransform.rect.height;
                    //On calcule l'excédent de hauteur
                    float excess_height = this.image.rectTransform.rect.height - image_height;

                    //On définit alors un nouveau rectangle pour l'image
                    image_rect = new Rect(0, this.image.rectTransform.pivot.y * excess_height, this.image.rectTransform.rect.width, image_ratio * this.image.rectTransform.rect.width);
                }

                //On peut alors définir les coordonées du pixel par rapport à l'écran
                image_pixel_screen = new Vector2(offset.x - image_rect.x, offset.y - image_rect.y);
                //Et les coordonées du pixel par rapport à l'image (comprises entre 0 et 1 / (0, 0) étant en bas à droite).
                image_pixel = new Vector2(image_pixel_screen.x / image_rect.width, image_pixel_screen.y / image_rect.height);
            }

            //Si l'image n'a pas son aspect préservé
            else
            {
                //On peut alors définir les coordonées du pixel par rapport à l'écran
                image_pixel_screen = new Vector2(offset.x, offset.y);
                //Et les coordonées du pixel par rapport à l'image (comprises entre 0 et 1 / (0, 0) étant en bas à droite).
                image_pixel = new Vector2(image_pixel_screen.x / this.image.rectTransform.rect.width, image_pixel_screen.y / this.image.rectTransform.rect.height);
            }

            //On retourne les coordonées du pixel comprises entre 0 et 1 multipliées par la largeur / la hauteur de la texture pour avoir ses coordonées en pixel.
            return new Vector2(image_pixel.x * this.texture.width, image_pixel.y * this.texture.height);
        }
        else
        {
            //Dans le cas ou la souris n'est pas dans le rectangle de l'image on retourne un Vector2 par defaut : (0f, 0f).
            return new Vector2(0f, 0f);
        }
    }

    //Retourne le pixel de l'image (le template) sur lequel l'utilisateur a cliqué
    private Vector2 Get_pixel()
    {
        //On récupère la position de la souris à l'écran
        Vector2 mouse_screen_position = Input.mousePosition;
        //On en déduit le pixel de l'image correspondant
        Vector2 pixel = Mouse_screen_to_image_pixel(mouse_screen_position);

        return pixel;
    }

    //////////////////////////////////
    /// Fonctions du MonoBehaviour ///
    //////////////////////////////////

    private void Awake()
    {
        //On récupère la texture sur laquelle on va dessiner
        this.image = GetComponent<Image>();
        this.sprite = this.image.sprite;
        this.texture = this.sprite.texture;

        //On récupère le tableau de pixel
        this.colors = texture.GetPixels32();
    }

    private void Update()
    {
        bool mouse_held_down = Input.GetMouseButton(0);
  
        if (mouse_held_down)
        {
            Vector2 pixel = Get_pixel();

            Color_Area(pixel, 10, Color.red);
            Apply_changes();
        }
    }
}