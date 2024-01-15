using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CollectorObjects : MonoBehaviour
{
    //GameObjects to collect
    public GameObject[] toCollect;
    private List<GameObject> uncollected;

    public bool isCompleted = false;

    public CollectionManager collection;

    // Start is called before the first frame update
    void Start()
    {
        if (uncollected == null || uncollected.Count == 0)
        {
            uncollected = toCollect.ToList<GameObject>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto colisionado tiene la etiqueta correcta  
        if (uncollected.Contains(other.gameObject))
        {
            // Elimina el objeto de la lista 
            uncollected.Remove(other.gameObject);

            Debug.Log("Object collected");

            // Verifica si todos los objetos han sido recogidos  
            if (uncollected.Count == 0)
            {
                isCompleted = true;

                collection.checkIfCompleted();
            }
        }
    }
}
