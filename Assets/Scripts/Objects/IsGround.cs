using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGround : MonoBehaviour
{
    public bool ground;

    private ObstacleCrash newOBS;

    public bool isObstacle = true;

    public Instructions tutorialPanel;
    public GameObject nextInstructionTutorial;


    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Floor")
        {
            ground = true;

            if(isObstacle) Destroy(newOBS);

            FixedJoint joint = transform.GetComponent<FixedJoint>();
            if (joint) Destroy(joint);
        }

        if (col.gameObject.tag == "Pantalla") 
        {
            if(tutorialPanel != null && tutorialPanel.GetCountOfUnanswered() > 0 && tutorialPanel.GetNextInstructionName() == nextInstructionTutorial.name)
            {
                Invoke("setNextTutorial", 2f);
            }

        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.gameObject.tag == "Floor")
        {
            ground = false;

            if(isObstacle) newOBS = this.gameObject.AddComponent<ObstacleCrash>();
        }

    }

    void setNextTutorial()
    {
        tutorialPanel.setNextInstructions();
    }
}
