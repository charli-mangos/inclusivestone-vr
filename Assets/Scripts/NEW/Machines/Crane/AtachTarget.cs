using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtachTarget : MonoBehaviour
{
    private HingeJoint joint;
    private GameObject[] inputs;

    [SerializeField] private GameObject amarre;
    [SerializeField] private GameObject amarreModelo;

    private bool isGrip = false; 
    private bool gripButtonPressed = false;  // Bandera para rastrear si el botón de grip está presionado
    private bool isOnTarget = false;

    public  Instructions  panelMSG; 
    
    public float hookSpeed = 5.0f;
    public float pendulumLimit = 10.0f;

    private GameObject tablaUnit;

    void Start()
    {
        inputs = GameObject.FindGameObjectsWithTag("GameController");
    }

    void Update()
    {
        GetInput();

        if(isOnTarget && joint && this.GetComponent<CheckDistance>().isTooFar())
        {
            isGrip = true;
        }
        else if (!isOnTarget)
        {
            isGrip = false;
        }

    }

    private void OnTriggerStay(Collider col)
    {
        if(col.CompareTag("Target") && col.isTrigger)
        {
            tablaUnit = col.gameObject;

            if (tablaUnit.GetComponent<HingeJoint>() == null && isGrip)
            { 
                AttachObject(tablaUnit);
            }
            else if (joint && !isGrip && !this.GetComponent<CheckDistance>().isTooFar())
            {
                DetachObject();
            }
           
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Target") && col.isTrigger)
        {
            if (panelMSG.GetCountOfUnanswered() > 0)
            {
                panelMSG.setNextInstructions();
            }

            isOnTarget = true;
        }
    }


    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Target") && col.isTrigger)
        {
            isOnTarget = false;
        }
    }



    private void GetInput()
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

                if (hand.isRight == "right")
                {
                    if (hand.gripValue > 0.1f && !gripButtonPressed)
                    {
                        // Cambiar el valor de isHolding solo cuando el botón de grip se presiona por primera vez
                        isGrip = !isGrip;
                        gripButtonPressed = true;  // Marcar el botón de grip como presionado

                        break;

                    }
                    else if (hand.gripValue <= 0.1f)
                    {
                        // Reiniciar la bandera cuando el botón de grip se suelta
                        gripButtonPressed = false;
                    }
                }
            }
        }
    }

    private void AttachObject(GameObject target)
    {
        //Set amarrado texture
        target.transform.Find("Amarrado").gameObject.SetActive(true);
        amarreModelo.SetActive(true);

        joint = target.AddComponent<HingeJoint>();

        // Configura las propiedades del joint
        joint.connectedBody = amarre.GetComponent<Rigidbody>();

        // Establece el eje de giro para simular el movimiento pendular
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = new Vector3(-0.0005f, 0.12f, 0.003f);
        joint.anchor = new Vector3(0f, 0f,0.5f); 
        joint.axis = new Vector3(0f, 0.1f, 0f);

        // Configura los límites del movimiento pendular
        JointLimits limits = new JointLimits();
        limits.min = -pendulumLimit;
        limits.max = pendulumLimit;
        joint.limits = limits;

        // Activa el joint para comenzar a mover el objeto "target"
        joint.useLimits = true;
        joint.useMotor = true;

        // Configura la velocidad del motor para mover el objeto "target"
        JointMotor motor = joint.motor;
        motor.targetVelocity = hookSpeed;
        joint.motor = motor;


    }

    private void DetachObject()
    {
        Destroy(joint);
        joint = null;

        tablaUnit.transform.Find("Amarrado").gameObject.SetActive(false);
        amarreModelo.SetActive(false);
    }
}
