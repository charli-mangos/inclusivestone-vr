using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public string floorTag = "Floor";    // Etiqueta del objeto con el que colisionará para activar el imán.
    public float attractionForce = 10f;  // Fuerza de atracción.

    private bool isAttracted = false;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isAttracted)
        {
            // Buscar el objeto "barrote" más cercano dentro del rango.
            GameObject[] barrotes = GameObject.FindGameObjectsWithTag("Barrote");
            GameObject closestBarrote = null;
            float closestDistance = float.MaxValue;

            foreach (GameObject barrote in barrotes)
            {
                float distance = Vector3.Distance(transform.position, barrote.transform.position);
                if (distance < closestDistance)
                {
                    closestBarrote = barrote;
                    closestDistance = distance;
                }
            }

            if (closestBarrote != null)
            {
                Vector3 attractionDirection = (closestBarrote.transform.position - transform.position).normalized;
                Vector3 attractionForceVector = attractionDirection * attractionForce;

                rb.AddForce(attractionForceVector);
            }
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag(floorTag))
        {
            isAttracted = true;
        }
    }
}
