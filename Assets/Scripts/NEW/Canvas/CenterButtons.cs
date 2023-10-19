using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterButtons : MonoBehaviour
{
    // La referencia al contenedor que contiene los botones
    public RectTransform buttonContainer;

    void Start()
    {
        CenterButtonContainer();
    }

    void CenterButtonContainer()
    {
        if (buttonContainer != null)
        {
            // Centrar el contenedor en el centro del Canvas
            buttonContainer.localPosition = Vector3.zero;
        }
    }
}