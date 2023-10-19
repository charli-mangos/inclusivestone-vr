using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Highlight : XRSimpleInteractable
{
    //Check if hand is hovering element
    private bool isHover = false;

    //Actual selected element
    private string actual = "None";

    //Hand inputs
    private GameObject[] inputs;

    //Layer
    public string m_Layer = "Highlight";
    public string m_LayerSelect = "Selectable";

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetString("actual_HighLight", "None");

        inputs = GameObject.FindGameObjectsWithTag("GameController");
    }

    // Update is called once per frame
    void Update()
    {
        actual = PlayerPrefs.GetString("actual_HighLight");

        if (isHover && actual == this.gameObject.name)
        {
            //if (LayerMask.NameToLayer(m_Layer) == LayerMask.NameToLayer("FarHighlight"))
            //{
            //    this.gameObject.GetComponent<ChangeMaterial>().ReverseMaterial();
            //}

            this.gameObject.layer = LayerMask.NameToLayer(m_Layer);

            foreach (var col in this.colliders)
            {
                col.gameObject.layer = LayerMask.NameToLayer(m_Layer);

                foreach (Transform child in this.transform)
                {
                    child.gameObject.layer = LayerMask.NameToLayer(m_Layer);
                }
            }
        }
        else
        {
            this.gameObject.layer = LayerMask.NameToLayer(m_LayerSelect);

            foreach (var col in this.colliders)
            {
                col.gameObject.layer = LayerMask.NameToLayer(m_LayerSelect); 
                
                foreach (Transform child in this.transform)
                {
                    child.gameObject.layer = LayerMask.NameToLayer(m_LayerSelect);
                }
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "GameController")
        {
            PlayerPrefs.SetString("actual_HighLight", this.gameObject.name);
            isHover = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.gameObject.tag == "GameController")
        {
            PlayerPrefs.SetString("actualHighLight", "None");
            isHover = false;
        }
    }

}
