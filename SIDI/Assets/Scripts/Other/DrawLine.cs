using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    [SerializeField]
    private GameObject lineGeneratorPrefab;

    private void Start()
    {
        SpawnLineGenerator();
    }

    private void SpawnLineGenerator()
    {
        GameObject newLineGen = Instantiate(lineGeneratorPrefab);
        LineRenderer lRend = newLineGen.GetComponent<LineRenderer>();

        lRend.SetPosition(0, new Vector3(-2, 0, 0));
        lRend.SetPosition(1, new Vector3(2, 0, 0));

        Destroy(newLineGen);
    }
}
