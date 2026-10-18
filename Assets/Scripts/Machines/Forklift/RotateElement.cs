using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateElement : MonoBehaviour
{

    [SerializeField]
    private GrabOffset grab;

    [SerializeField]
    private Transform directionAxes;

    [SerializeField]
    private Vector3 upDirection;

    private bool rotateUp = true;
    private bool rotateDown = false;

    private float upForce;

    //Hand inputs
    private GameObject[] inputs;

    [SerializeField]
    private Transform toRotate;

    // Start is called before the first frame update
    void Start()
    {
        inputs = GameObject.FindGameObjectsWithTag("GameController");

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateForce();

        if (upForce > 0 && rotateUp)
        {
            float rotationAmount = 20f * Time.deltaTime * upForce;
            toRotate.localRotation *= Quaternion.AngleAxis(rotationAmount, Vector3.up);
            rotateDown = true;
        }
        else if (upForce < 0 && rotateDown)
        {
            float rotationAmount = 20f * Time.deltaTime * upForce;
            toRotate.localRotation *= Quaternion.AngleAxis(rotationAmount, Vector3.up);
            rotateUp = true;
        }
            /*if(upForce > 0 && rotateUp)
            {
                toRotate.RotateAround(directionAxes.position, upDirection, upForce);
                rotateDown = true;
            }
            else if(upForce < 0 && rotateDown)
            {
                toRotate.RotateAround(directionAxes.position, upDirection, upForce);
                rotateUp = true;
            }*/
        }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "RotationLimit" && upForce > 0)
        {
            rotateUp = false;
        }
        else if(col.gameObject.tag == "RotationLimit" && upForce < 0)
        {
            rotateDown = false;
        }
    }

    void UpdateForce()
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

                if (hand.isRight == "right" && grab.isGrabbing)
                {
                    upForce = hand.axisVal.y * 0.5f;
                }
                else if(!grab.isGrabbing)
                {
                    upForce = 0;
                }

            }
        }
    }
}
