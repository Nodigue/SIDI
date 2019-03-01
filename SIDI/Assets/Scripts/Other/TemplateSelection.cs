using UnityEngine;
using UnityEngine.UI;

public class TemplateSelection : MonoBehaviour
{
    [SerializeField]
    private float translation = 0.2f;

    [SerializeField]
    private GameObject[] templates;

    public void TranslateRight(Transform button)
    {
        button.position = new Vector3(button.position.x + this.translation, button.position.y);
    }

    public void TranslateLeft(Transform button)
    {
        button.position = new Vector3(button.position.x - this.translation, button.position.y);
    }

    public void ActivateTemplate(GameObject activeTemplate)
    {
        foreach (GameObject template in this.templates)
        {
            template.SetActive(false);

            if (template == activeTemplate)
                template.SetActive(true);
        }
    }
}
