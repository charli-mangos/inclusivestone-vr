using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialForklift : MonoBehaviour
{
    public Instructions tutoIns;
    private string currentIns = "";

    private List<string> completedPanels = new List<string>();

    public GameObject marcador;

    //Hand inputs
    private GameObject[] inputs;

    void Start() 
    { 
        inputs = GameObject.FindGameObjectsWithTag("GameController");
    }

    // Update is called once per frame
    void Update()
    {
        if (currentIns != tutoIns.GetCurrentInstructionsName())
        {
            currentIns = tutoIns.GetCurrentInstructionsName();

        }

        CheckPanels();
    }

    void CheckPanels()
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

                if (!completedPanels.Contains(currentIns))
                {
                    if (currentIns == "Frenar" && hand.primaryButton == true)
                    {
                        completedPanels.Add(currentIns);
                        Invoke("HidePanel", 2f);
                        break;
                    }

                    if (hand.isRight == "right")
                    {
                        if ((currentIns == "Acelerar" && hand.trigVal > 0.5) || (currentIns == "Cotrol Elevator 1" && hand.gripValue > 0.5) || (currentIns == "Cotrol Elevator 2" && hand.gripValue > 0.5 && hand.axisVal.y != 0))
                        {
                            completedPanels.Add(currentIns);
                            Invoke("HidePanel", 2f);
                            break;
                        }
                    }
                    else if (hand.isRight == "left")
                    {
                        if (currentIns == "Marcha Atras" && hand.trigVal > 0.5 )
                        {
                            completedPanels.Add(currentIns);
                            Invoke("HidePanel", 2f);
                            break;
                        }

                        if (currentIns == "Girar" && hand.axisVal.x != 0)
                        {
                            completedPanels.Add(currentIns);
                            Invoke("FinishTutorial", 2f);
                            break;
                        }
                    }

                }
                
            }    
        }
    }

    void HidePanel()
    {

        tutoIns.finishInstruction();

        if (tutoIns.GetCountOfUnanswered() > 0)
        {
            tutoIns.setNextInstructions();
        }
    }

    void FinishTutorial()
    {
        tutoIns.finishInstruction();

        marcador.SetActive(true);
    }

    public void cancelTutorial()
    {
        Destroy(this.gameObject);
    }
}
