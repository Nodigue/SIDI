using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class TemplateImporter
{
    private static string GetPath(string name)
    {
        string[] searchResult = AssetDatabase.FindAssets(name);
        string path = AssetDatabase.GUIDToAssetPath(searchResult[0]);

        return path;
    }

    private static List<Rect> SortRects(List<Rect> rects, float textureWidth)
    {
        List<Rect> list = new List<Rect>();
        while (rects.Count > 0)
        {
            Rect rect = rects[rects.Count - 1];
            Rect sweepRect = new Rect(0f, rect.yMin, textureWidth, rect.height);
            List<Rect> list2 = RectSweep(rects, sweepRect);
            if (list2.Count <= 0)
            {
                list.AddRange(rects);
                break;
            }
            list.AddRange(list2);
        }
        return list;
    }

    private static List<Rect> RectSweep(List<Rect> rects, Rect sweepRect)
    {
        List<Rect> result;
        if (rects == null || rects.Count == 0)
        {
            result = new List<Rect>();
        }
        else
        {
            List<Rect> list = new List<Rect>();
            foreach (Rect current in rects)
            {
                if (current.Overlaps(sweepRect))
                {
                    list.Add(current);
                }
            }
            foreach (Rect current2 in list)
            {
                rects.Remove(current2);
            }
            list.Sort((a, b) => a.x.CompareTo(b.x));
            result = list;
        }
        return result;
    }

    private static SpriteMetaData GetSpriteMetaData(string name, Rect rect, SpriteAlignment alignment)
    {
        SpriteMetaData meta = new SpriteMetaData
        {
            name = name,
            rect = rect,

            pivot = Vector2.down,
            alignment = (int) alignment
        };

        return meta;
    }
    
    public static void ImportCharacter(string name)
    {
        string path = GetPath(name);
        Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(path);

        var importer = AssetImporter.GetAtPath(path) as TextureImporter;

        importer.textureType = TextureImporterType.Sprite;
        importer.spriteImportMode = SpriteImportMode.Multiple;
        importer.mipmapEnabled = false;
        importer.filterMode = FilterMode.Point;
        importer.spritePivot = Vector2.down;
        importer.textureCompression = TextureImporterCompression.Uncompressed;

        var textureSettings = new TextureImporterSettings();
        importer.ReadTextureSettings(textureSettings);
        textureSettings.spriteMeshType = SpriteMeshType.Tight;
        textureSettings.spriteExtrude = 0;

        importer.SetTextureSettings(textureSettings);

        int minimumSpriteSize = 16;
        int extrudeSize = 0;

        Rect[] rects = InternalSpriteUtility.GenerateAutomaticSpriteRectangles(texture, minimumSpriteSize, extrudeSize);

        var rectsList = new List<Rect>(rects);
        rectsList = SortRects(rectsList, texture.width);

        var metas = new List<SpriteMetaData>
        {
            GetSpriteMetaData("head", rectsList[0], SpriteAlignment.BottomCenter),
            GetSpriteMetaData("body", rectsList[3], SpriteAlignment.Center),
            GetSpriteMetaData("arm_left_1", rectsList[4], SpriteAlignment.LeftCenter),
            GetSpriteMetaData("arm_left_2", rectsList[5], SpriteAlignment.LeftCenter),
            GetSpriteMetaData("arm_right_1", rectsList[2], SpriteAlignment.RightCenter),
            GetSpriteMetaData("arm_right_2", rectsList[1], SpriteAlignment.RightCenter),
            GetSpriteMetaData("leg_left_1", rectsList[6], SpriteAlignment.TopCenter),
            GetSpriteMetaData("leg_left_2", rectsList[8], SpriteAlignment.TopCenter),
            GetSpriteMetaData("leg_right_1", rectsList[7], SpriteAlignment.TopCenter),
            GetSpriteMetaData("leg_right_2", rectsList[9], SpriteAlignment.TopCenter),
        };

        importer.spritesheet = metas.ToArray();

        AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
    }
}