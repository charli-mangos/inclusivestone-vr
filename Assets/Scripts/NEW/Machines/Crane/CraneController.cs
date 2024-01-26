using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneController : MonoBehaviour
{
    [SerializeField]
    private GameObject motor;

    [SerializeField]
    private GameObject overhead;

    [SerializeField]
    private GameObject ropes;

    [SerializeField]
    private GameObject[] gancho;

    public int power = 0;
    public string state = "None";

    public float constForce = 0.01f;

    private bool isBack = false;
    private bool isFront = false;
    private bool isLeft = false;
    private bool isRight = false;
    public bool isDown = false;
    public bool isUp = false;

    public AudioSource audio;

    void Start()
    {
        audio = this.gameObject.GetComponent<AudioSource>();
        audio.Stop();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Check stop state and power value
        if (power != 0 && state != "Block")
        {
            //Lateral movement
            if (state == "left_right")
            {
                if (power > 0 && !isLeft)
                {
                    motor.transform.Translate(new Vector3(1f, 0, 0) * power * constForce);
                    isRight = false;
                }
                else if (power < 0 && !isRight)
                {
                    motor.transform.Translate(new Vector3(1f, 0, 0) * power * constForce);
                    isLeft = false;
                }
            }

            //Vertical movement
            if (state == "up_down")
            {
                if(power > 0 && !isUp)
                {
                    ropes.transform.localScale = ropes.transform.localScale + new Vector3(0, 0, 1f) * (-power * constForce * 0.5f);

                    foreach (var ele in gancho)
                    {
                        ele.transform.Translate(new Vector3(0, 0, 1f) * power * constForce);
                    }

                    isDown = false;
                }
                else if (power < 0 && !isDown)
                {
                    ropes.transform.localScale = ropes.transform.localScale + new Vector3(0, 0, 1f) * (-power * constForce * 0.5f);

                    foreach (var ele in gancho)
                    {
                        ele.transform.Translate(new Vector3(0, 0, 1f) * power * constForce);
                    }

                    isUp = false;
                }
            }

            //Frontal movement
            if (state == "back_front")
            {
                if (power > 0 && !isBack)
                {
                    overhead.transform.Translate(new Vector3(0, 1f, 0) * power * constForce);
                    isFront = false;
                }
                else if (power < 0 && !isFront)
                {
                    overhead.transform.Translate(new Vector3(0, 1f, 0) * power * constForce);
                    isBack = false;
                }
            }
        }

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "CraneCorp" && state == "back_front")
        {
            if (power > 0 && !isBack)
            {
                isBack = true;
            }

            if (power < 0 && !isFront)
            {
                isFront = true;
            }
        }

        if (col.gameObject.tag == "CraneEngine" && state == "left_right")
        {
            if (power > 0 && !isLeft)
            {
                isLeft = true;
            }

            if (power < 0 && !isRight)
            {
                isRight = true;
            }
        }
    }

}
