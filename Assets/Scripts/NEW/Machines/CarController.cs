using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController: MonoBehaviour
{
    //private DriveController inputDrive;
    [SerializeField]
    private List<WheelCollider> wheels;
    [SerializeField]
    private List<GameObject> wheelsSteer;

    //[SerializeField]
    ///private TwoHandGrab steeringWheel;

    [SerializeField]
    private float strength = 200000f;
    [SerializeField]
    private float brakeStrength = 80000f;
    [SerializeField]
    private float maxSteering;

    public bool axisDir = true; 

    //Hand inputs
    private GameObject[] inputs;

    //General values
    private float accelVal;
    private float dampening;
    private bool isBreaking;
    private float currentSteerAngle;
    private float horizontalVal;
    private bool isBouncing = false;

    // Start is called before the first frame update
    void Start()
    {
        inputs = GameObject.FindGameObjectsWithTag("GameController");

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetValues();

        if (!isBouncing)
        {
            foreach (WheelCollider wheel in wheels)
            {
                wheel.motorTorque = strength * Time.deltaTime * accelVal;

                wheel.wheelDampingRate = dampening;

                wheel.brakeTorque = isBreaking ? brakeStrength : 0;
            }

            foreach (var wheel in wheelsSteer)
            {
                WheelCollider collider = wheel.GetComponent<WheelCollider>();
                Transform trans = wheel.transform;

                collider.steerAngle = currentSteerAngle;
                collider.wheelDampingRate = dampening;
                //UpdateWheelVisual(collider, trans);
            }
        }
        

    }

    void GetValues()
    {

        if(inputs.Length == 0)
        {
            inputs = GameObject.FindGameObjectsWithTag("GameController");
        }
        else
        {
            foreach (var element in inputs)
            {
                HandController hand = element.GetComponent<HandController>();

                if(hand.isRight == "left")
                {
                    horizontalVal = hand.axisVal.x;
                }

                if (!hand.primaryButton && hand.trigVal > 0.1)
                {
                    isBreaking = false;


                    //Right Hand
                    if (hand.isRight == "right")
                    {
                        if (axisDir)
                        {
                            accelVal = hand.trigVal;
                        }
                        else
                        {
                            accelVal = -hand.trigVal;
                        }
                        dampening = 600f;

                    }
                    //Left Hand
                    else if (hand.isRight == "left")
                    {
                        if (axisDir)
                        {
                            accelVal = -hand.trigVal;
                        }
                        else
                        {
                            accelVal = hand.trigVal;
                        }
                        dampening = 1000f;
                    }

                }
                else if(hand.primaryButton)
                {
                    isBreaking = true;
                }

                //Steering
                //horizontalVal = steeringWheel.rotEvent.y;
                //Debug.Log(horizontalVal);


                currentSteerAngle = maxSteering * horizontalVal;
            }
        }
        
    }

    private void UpdateWheelVisual(WheelCollider collider, Transform trans)
    {
        Vector3 pos;
        Quaternion rot;

        collider.GetWorldPose(out pos, out rot);

        trans.rotation = rot;
        trans.position = pos;
    }

    private void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Limit")
        {
            float bounce = 3f;
            this.GetComponent<Rigidbody>().AddForce(collisionInfo.contacts[0].normal * bounce);

            isBouncing = true;

            Invoke("StopBounce", 0.3f);
        }
    }


    void StopBounce()
    {
        isBouncing = false;
    }


}
