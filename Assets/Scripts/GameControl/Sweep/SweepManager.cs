using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweepManager : MonoBehaviour
{
    private int totalSweepables; // Total de objetos 'Sweepable'  
    private int sweptCount = 0; // Contador de objetos barridos  
    public GameController gameController; // Referencia al GameController  

    private void Start()
    {
        // Obtiene el total de objetos 'Sweepable' en la escena al inicio  
        totalSweepables = GameObject.FindGameObjectsWithTag("Sweep").Length + GameObject.FindGameObjectsWithTag("Scrub").Length;
    }

    public void IncreaseSweptCount()
    {
        // Incrementa el contador de objetos barridos  
        sweptCount++;

        // Comprueba si todos los objetos 'Sweepable' han sido barridos  
        if (sweptCount >= totalSweepables)
        {
            // Llama a finishMission en GameController  
            gameController.finishMission();
        }
    }
}
