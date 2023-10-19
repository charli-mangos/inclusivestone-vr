using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtachTarget : MonoBehaviour
{
    private FixedJoint joint;
    private GameObject[] inputs;

    [SerializeField] private GameObject amarre;
    [SerializeField] private GameObject amarrado;
    [SerializeField] private GameObject no_amarrado;

    private bool isGrip = false; 
    private bool gripButtonPressed = false;  // Bandera para rastrear si el botón de grip está presionado
    private bool isOnTarget = false;

    public  Instructions  panelMSG;

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
            //if (col.GetComponent<TargetWeight>().panelCompleted)
            //{
            if (col.GetComponent<FixedJoint>() == null && isGrip)
            { 
                AttachObject(col.gameObject);
            }
            else if (joint && !isGrip && !this.GetComponent<CheckDistance>().isTooFar())
            {
                DetachObject();
            }
            /*}
            else
            {
                col.GetComponent<TargetWeight>().setAPanel();
            }*/
           
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
        joint = target.AddComponent<FixedJoint>();
        joint.connectedBody = amarre.GetComponent<Rigidbody>();

        amarrado.SetActive(true);
        no_amarrado.SetActive(false);
    }

    private void DetachObject()
    {
        Destroy(joint);
        joint = null;


        amarrado.SetActive(false);
        no_amarrado.SetActive(true);
    }
}
