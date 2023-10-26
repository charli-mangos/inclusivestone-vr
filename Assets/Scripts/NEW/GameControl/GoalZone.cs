using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class GoalZone : MonoBehaviour
{
    public GameObject[] targets;
    private List<GameObject> uncompletedTargets;

    private List<GameObject> completedTargets;

    //Num of goals to complete the mission
    [SerializeField] private int maxOfGoals;

    //GameController 
    [SerializeField] private GameController gameController;

    public Instructions tutorialPanel;

    private int numOfGoals = 0;

    public Text marcador;

    public Mission mission;

    // Start is called before the first frame update
    void Start()
    {
        if (uncompletedTargets == null || uncompletedTargets.Count == 0)
        {
            uncompletedTargets = targets.ToList<GameObject>();

        }
    }

    void Update()
    {
        marcador.text = numOfGoals.ToString() + '/' + maxOfGoals.ToString();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Target" && uncompletedTargets.Contains(col.gameObject) && col.GetComponent<HingeJoint>() == null && col.GetComponent<FixedJoint>() == null && gameController.getCurrentMission() == mission)
        {
            uncompletedTargets.Remove(col.gameObject);
            numOfGoals += 1;

            if (numOfGoals == maxOfGoals)
            {
                gameController.finishMission();

                marcador.transform.parent.gameObject.SetActive(false);

                Destroy(this);
            }
            else if (tutorialPanel != null && tutorialPanel.GetCountOfUnanswered() > 0)
            {
                tutorialPanel.setNextInstructions();
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Target" && !uncompletedTargets.Contains(col.gameObject))
        {
            uncompletedTargets.Add(col.gameObject);
            numOfGoals -= 1;
        }

    }

}
