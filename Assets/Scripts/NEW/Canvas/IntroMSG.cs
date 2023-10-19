using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class IntroMSG : MonoBehaviour
{
    //Instructions panel
    public InstructionUnit[] inst;
    private List<InstructionUnit> unanswered;

    //Current instruction active
    private InstructionUnit currentInst;

    //Game controller component
    public GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        if (unanswered == null || unanswered.Count == 0)
        {
            initInstructions();
        }

    }

    public void initInstructions()
    {
        currentInst = null;

        unanswered = inst.ToList<InstructionUnit>();

        setNextInstructions();

    }

    public void setNextInstructions()
    {
        if (currentInst != null)
        {
            currentInst.panel.SetActive(false);
            unanswered.Remove(currentInst);
        }

        currentInst = unanswered[0];

        if(currentInst.canvasText != null)
        {
            currentInst.canvasText.text = "";

            foreach (var palID in currentInst.description)
            {
                currentInst.canvasText.text = currentInst.canvasText.text + MainTranslate.Fields[palID] + "\n" + "\n";
            }
        }
      
        currentInst.panel.SetActive(true);
    }

    public void startMission()
    {
        gameController.startScene = true;
        gameController.setNextMission();

        this.gameObject.SetActive(false);
    }
}