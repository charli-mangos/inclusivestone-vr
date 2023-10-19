using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float destroyDelay = 10.0f; // Tiempo en segundos antes de autodestruirse.

    private void Start()
    {
        // Invocar la función para autodestruirse después de 'destroyDelay' segundos.
        Destroy(this.gameObject, destroyDelay);
    }
}