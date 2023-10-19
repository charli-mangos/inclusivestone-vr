using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Instructions : MonoBehaviour
{
    //Instructions panel
    public InstructionUnit[] inst;
    private List<InstructionUnit> unanswered;

    //Current instruction active
    private InstructionUnit currentInst;

    // Start is called before the first frame update
    void Start()
    {
        if (unanswered == null || unanswered.Count == 0)
        {
            unanswered = inst.ToList<InstructionUnit>();
        }

        setNextInstructions();
    }

    public int GetCountOfUnanswered()
    {
        return unanswered.Count;
    }

    public string GetCurrentInstructionsName()
    {
        return currentInst.panel.name;
    }


    public void setNextInstructions()
    {
        currentInst = unanswered[0];

        foreach (var palID in currentInst.description)
        {
            if (!currentInst.canvasText.text.Contains(MainTranslate.Fields[palID]))
            {
                currentInst.canvasText.text = currentInst.canvasText.text + MainTranslate.Fields[palID] + "\n" + "\n";
            }
        }

        currentInst.panel.SetActive(true);

        if (currentInst.isTemporary)
        {
            StartCoroutine(DestroyInstruction());
        }
    }

    public void finishInstruction()
    {
        if (currentInst != null)
        {
            currentInst.isCompleted = true;

            currentInst.panel.SetActive(false);
            unanswered.Remove(currentInst);

            if (unanswered.Count == 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    IEnumerator DestroyInstruction()
    {
        yield return new WaitForSeconds(10f);

        finishInstruction();

    }
}
