using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUp : MonoBehaviour
{
    [SerializeField]
    private GrabOffset grab;

    public float constForce;

    public bool goUp = true;
    public bool goDown = false;

    private float upForce;

    //Hand inputs
    private GameObject[] inputs;

    // Start is called before the first frame update
    void Start()
    {
        inputs = GameObject.FindGameObjectsWithTag("GameController");

    }


    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateForce();

        if (goUp && upForce > 0)
        {
            transform.Translate(new Vector3(0, 0, 1f) * upForce * constForce);
            goDown = true;
        }
        else if (goDown && upForce < 0)
        {
            transform.Translate(new Vector3(0, 0, 1f) * upForce * constForce);
            goUp = true;
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
                else if (!grab.isGrabbing)
                {
                    upForce = 0;
                }

            }
        }
    }
}
