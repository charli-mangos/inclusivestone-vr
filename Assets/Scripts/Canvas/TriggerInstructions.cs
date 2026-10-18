using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerInstructions : MonoBehaviour
{
    public Instructions instructions;
    public GameObject canvas;

    public SelectObject selectObjs;

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "GameController")
        {
            if(instructions != null && instructions.GetCurrentInstructionsName() == canvas.name)
            {
                Invoke("finishInstruction", 2f);
            }
        }
    }

    void finishInstruction()
    {
        instructions.finishInstruction();
        instructions.setNextInstructions();

        if (selectObjs)
        {
            selectObjs.enabled = true;
        }

        this.enabled = false;
    }
}
