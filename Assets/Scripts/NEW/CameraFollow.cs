using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Camera Camera2Follow;
    public float distance = 3.0F;
    public float smoothTime = 0.3F;
    public Vector3 rotationAngles = new Vector3(0f, 0f, 0f);
    public bool moveTowardsCamera = true; // Variable booleana para activar/desactivar el movimiento hacia la cámara

    private Vector3 velocity = Vector3.zero;
    private Transform target;
    public bool hasMargin;
    private float constXRight = 0f;
    public float distanceMargin = 0f;

    public bool desactivateClick = false;
    private bool desactivacionPermitida = false;

    //Hand inputs
    private GameObject[] inputs;

    void Awake()
    {
        target = Camera2Follow.transform;

        if (hasMargin)
        {
            constXRight = distanceMargin;
        }

        inputs = GameObject.FindGameObjectsWithTag("GameController");
    }

    void Update()
    {
        // Define my target position in front of the camera ->
        Vector3 targetPosition = target.TransformPoint(new Vector3(constXRight, 0, distance));
        var dir = Camera2Follow.transform.position - transform.position;

        Quaternion lookAtRotation = Quaternion.LookRotation(-dir);

        // Aplicar la rotación como un Vector3
        Quaternion rotation = Quaternion.Euler(rotationAngles);

        // Combina las rotaciones
        Quaternion finalRotation = lookAtRotation * rotation;

        // Aplica la rotación
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, finalRotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        if (moveTowardsCamera)
        {
            // Solo mueve el objeto hacia la cámara si moveTowardsCamera está activado
            // Smoothly move my object towards that position ->
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(targetPosition.x, transform.position.y, targetPosition.z), ref velocity, smoothTime);
        }

        if (desactivateClick && desactivacionPermitida)
        {
            desactivateScript();
        }
    }

    void desactivateScript()
    {
        if (inputs.Length == 0)
        {
            inputs = GameObject.FindGameObjectsWithTag("GameController");
        }
        else
        {

            foreach (var element in inputs)
            {
                HandController hand = element.GetComponent<HandController>();

                if (hand.gripValue > 0.5)
                {
                    this.enabled = false;
                    desactivacionPermitida = false;
                }
            }

        }
    }

    public void setDesactivation(bool value)
    {
        desactivacionPermitida = value;
    }
}