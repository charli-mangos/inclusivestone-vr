using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDistance : MonoBehaviour
{
    public Transform CheckTransform; // El transform del punto de chequeo cerca del suelo.
    public LayerMask objLayer; // La capa que representa el suelo.
    public float minimumDistance = 1.0f; // La distancia mínima requerida al suelo.

    
    public bool isTooFar()
    {
        // Realizar un raycast desde el objeto hacia abajo para comprobar la distancia al suelo.
        RaycastHit hit;
        if (Physics.Raycast(CheckTransform.position, Vector3.down, out hit, Mathf.Infinity, objLayer))
        {
            float distanceToObj = hit.distance;

            // Verificar si la distancia al suelo es mayor que el valor mínimo.
            bool isAboveMinimumDistance = distanceToObj > minimumDistance;

            // Hacer algo con el resultado (por ejemplo, imprimir en la consola).
            //Debug.Log("¿Está el objeto por encima de la distancia mínima al suelo? " + isAboveMinimumDistance);

            return isAboveMinimumDistance;
        }
        else
        {
            return false;
        }
    }
}
