using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetWeight : MonoBehaviour
{
    public enum rangoPeso { Bajo, Medio, Alto };

    public rangoPeso pesoRange;

    public string selectedItem;

    private float weight;

    public bool panelCompleted = false;

    //WeightPanel
    public GameObject weightPanel;
    private GameObject wPanel;

    //AgarrePanel
    public GameObject agarrePanel;
    private GameObject aPanel;

    private bool farFromFloor = false;

    void Start()
    {
        switch (pesoRange)
        {
            case rangoPeso.Bajo:
                weight = Random.Range(30, 50);
                selectedItem = "Bajo";
                break;
            case rangoPeso.Medio:
                weight = Random.Range(50, 100);
                selectedItem = "Medio";
                break;
            case rangoPeso.Alto:
                weight = Random.Range(100, 150);
                selectedItem = "Alto";
                break;
        }
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            if (wPanel == null)
            {
                wPanel = Instantiate(weightPanel, new Vector3(this.transform.position.x, 0.8f, this.transform.position.z + 0.3f), Quaternion.identity) as GameObject;
                wPanel.transform.Rotate(0f, 180f, 0f);
                wPanel.transform.parent = this.transform;

                changeTextComponentInChildren(wPanel.transform);
            }
            else
            {
                wPanel.SetActive(true);
            }
        }

    }

    public void setAPanel()
    {
        if (!panelCompleted)
        {
            if (aPanel == null)
            {
                aPanel = Instantiate(agarrePanel, new Vector3(this.transform.position.x, 1.5f, this.transform.position.z + 0.3f), Quaternion.identity) as GameObject;
                aPanel.transform.Rotate(0f, 180f, 0f);
                aPanel.transform.parent = this.transform;

                SetResponse(aPanel.transform);
            }
            else
            {
                aPanel.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player") && wPanel != null)
        {
            wPanel.SetActive(false);
        }

        if (col.CompareTag("Amarre") && aPanel != null)
        {
            aPanel.SetActive(false);
        }
    }

    private void changeTextComponentInChildren(Transform currentTransform)
    {
        // Buscamos el componente Text en el objeto actual
        Text foundText = currentTransform.GetComponent<Text>();

        // Si encontramos el componente Text, lo devolvemos
        if (foundText != null)
        {
            foundText.text = MainTranslate.Fields["weight"] + weight + " kg";
            return;
        }
        // Si no encontramos el componente Text en el objeto actual, buscamos en sus hijos
        foreach (Transform child in currentTransform)
        {
            changeTextComponentInChildren(child);
        }

        // Si no encontramos el componente Text en la jerarquía, devolvemos
        return;
    }

    private void SetResponse(Transform currentTransform)
    {
        // Buscamos el componente  en el objeto actual
        MultipleSelectionQuiz foundQuiz = currentTransform.GetComponent<MultipleSelectionQuiz>();

        // Si encontramos el componente, lo devolvemos
        if (foundQuiz != null)
        {
            foundQuiz.setExternalResponse(selectedItem);
            return;
        }

        // Si no encontramos el componente en el objeto actual, buscamos en sus hijos
        foreach (Transform child in currentTransform)
        {
            SetResponse(child);
        }

        // Si no encontramos el componente en la jerarquía, devolvemos null
        return;
    }

}
