using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRetractil : MonoBehaviour
{

    [SerializeField]
    private GrabOffset grab;

    public float constForce;

    private MoveUp move;

    private bool goUp = false;
    private bool goDown = false;
    private bool isTop = false;
    private bool isBottom = true;

    [SerializeField]
    private List<Collider> mastil;

    private float upForce;

    //Hand inputs
    private GameObject[] inputs;

    [SerializeField]
    private Transform mastilTransform;


    void Start()
    {
        inputs = GameObject.FindGameObjectsWithTag("GameController");

        //Busca el hijo y su componente moveUp
        move = mastilTransform.Find("Pantalla").GetComponent<MoveUp>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateForce();

        if (goUp && upForce > 0 && !isTop)
        {
            mastilTransform.Translate(new Vector3(0, 0, 1f) * upForce * constForce);
            goDown = true;
            isBottom = false;

        }
        else if (goDown && upForce < 0 && !isBottom)
        {
            mastilTransform.Translate(new Vector3(0, 0, 1f) * upForce * constForce);
            goUp = true;
            isTop = false;
        }

    }

    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag == "Pantalla" && upForce > 0 && !isTop)
        {
            goUp = true;
            move.goUp = false;
            move.goDown = false;
        }
        else if (col.gameObject.tag == "Pantalla" && upForce < 0 && isBottom)
        {
            move.goDown = false;
        }


        if (col.gameObject.tag == "Mastil" && mastil.Contains(col))
        {
            if (upForce > 0)
            {
                move.goDown = false;
                goUp = false;
                isTop = true;
            }
            else if (upForce < 0)
            {
                move.goDown = true;
                goUp = false;
                goDown = false;
                isBottom = true;
            }
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
