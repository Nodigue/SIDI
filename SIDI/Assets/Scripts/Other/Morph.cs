using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class Morph
{
    private static string GetPath(string name)
    {
        string[] searchResult = AssetDatabase.FindAssets(name);
        string path = AssetDatabase.GUIDToAssetPath(searchResult[0]);

        path = path.Substring(17, path.Length - (17 + 4));
        
        return path;
    }

    public static void morph(BodyPartsController bpcontroller, string name)
    {
        
        string path = GetPath(name);

        Sprite[] characterSprites = Resources.LoadAll<Sprite>(path);

        /*bpcontroller.head.GetComponent<SpriteRenderer>().sprite = characterSprites[5];
        bpcontroller.body.GetComponent<SpriteRenderer>().sprite = characterSprites[4];
        bpcontroller.armLeft1.GetComponent<SpriteRenderer>().sprite = characterSprites[0];
        bpcontroller.armLeft2.GetComponent<SpriteRenderer>().sprite = characterSprites[1];
        bpcontroller.armRight1.GetComponent<SpriteRenderer>().sprite = characterSprites[2];
        bpcontroller.armRight2.GetComponent<SpriteRenderer>().sprite = characterSprites[3];
        bpcontroller.legLeft1.GetComponent<SpriteRenderer>().sprite = characterSprites[6];
        bpcontroller.legLeft2.GetComponent<SpriteRenderer>().sprite = characterSprites[7];
        bpcontroller.legRight1.GetComponent<SpriteRenderer>().sprite = characterSprites[8];
        bpcontroller.legRight2.GetComponent<SpriteRenderer>().sprite = characterSprites[9];*/
    }
}
