using UnityEngine;
using UnityEngine.UI;

public class TemplateHandler : MonoBehaviour
{
    public GameObject[] templates;

    public Text template_name;

    private int current_template = 0;
    private bool change = false;

    private void Desactivate_all()
    {
        foreach (GameObject template in this.templates)
            template.SetActive(false);
    }

    private void Display_name()
    {
        this.template_name.text = this.templates[this.current_template].name;
    }

    private void Change_template()
    {
        Desactivate_all();
        this.templates[this.current_template].SetActive(true);

        this.change = true;

        Display_name();
    }

    private void Awake()
    {
        Change_template();
    }

    private void Update()
    {
        bool previous_template = Input.GetKey(KeyCode.A);
        bool next_template = Input.GetKey(KeyCode.E);

        if (previous_template || next_template)
        {
            if (previous_template && !this.change)
            {
                if (this.current_template > 0)
                    this.current_template--;

                else if (this.current_template == 0)
                    this.current_template = this.templates.Length - 1;

                Change_template();
            }

            else if (next_template && !this.change)
            {
                if (this.current_template < this.templates.Length - 1)
                    this.current_template++;

                else if (this.current_template == this.templates.Length - 1)
                    this.current_template = 0;

                Desactivate_all();
                this.templates[this.current_template].SetActive(true);
                this.change = true;

                Change_template();
            }
        }
        else
        {
            this.change = false;
        }
    }
}
