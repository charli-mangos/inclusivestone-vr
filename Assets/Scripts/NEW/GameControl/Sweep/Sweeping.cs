using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sweeping : MonoBehaviour
{
    public ParticleSystem dustParticles; // Sistema de partículas  
    private int sweepCount = 0; // Contador de barridos  
    public int sweepThreshold = 5; // Número 'x' de veces que se debe barrer  
    public SweepManager gameManager; // Referencia al administrador de juego  
    public string typeOfResiduo;

    private void OnCollisionEnter(Collision other)
    {
        // Verifica si el objeto colisionado tiene la etiqueta correcta  
        if (other.gameObject.tag == typeOfResiduo)
        {
            Debug.Log("Entró");

            // Activa el sistema de partículas  
            if(dustParticles != null)
            {
                dustParticles.Play();
            }

            // Incrementa el contador de barridos  
            sweepCount++;

            // Verifica si el contador alcanzó el límite  
            if (sweepCount >= sweepThreshold)
            {
                // Desactiva el objeto  
                other.gameObject.SetActive(false);

                // Notifica al administrador de juego que un objeto ha sido barrido  
                gameManager.IncreaseSweptCount();
            }
        }
    }
}