using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCanvas : MonoBehaviour
{
    public Instructions instructions;
    public GameObject canvas;

    public GameObject nextCanvas;
    public string actualIns;
    public string collisionTag;

    public float time = 1.5f;

    void Start() { }
    void Update() { }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == collisionTag)
        {
            if (instructions != null && instructions.GetCurrentInstructionsName() == actualIns)
            {
                Invoke("finishInstruction", time);
            }
        }
    }

    void finishInstruction()
    {
        instructions.finishInstruction();
        canvas.SetActive(false);

        nextCanvas.SetActive(true);

        Destroy(this);
    }
}
