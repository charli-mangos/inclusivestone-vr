using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class isSorted : MonoBehaviour
{
    public CheckCol[] targetPoints;
    public static List<CheckCol> target;

    //Panel gameobjects
    public GameObject checkPanel;
    public GameObject congratsPanel;
    public GameObject failPanel;

    //Loading panel
    public GameObject loadingPanel;

    //Delay time
    public float delayTime = 2f;

    //Game controller component
    public GameController gameController;

    void Start()
    {
        InitList();
    }

    public void InitList()
    {
        checkPanel.SetActive(true);
        failPanel.SetActive(false);
        congratsPanel.SetActive(false);

        if (target == null || target.Count == 0)
        {
            target = targetPoints.ToList<CheckCol>();
        }
    }

    public void checkCols()
    {
        foreach(var element in target)
        {
            bool res = element.checkResult();

            if (res == false)
            {
                StartCoroutine(failTask());
                return;
            }
        }

        StartCoroutine(completedTask());
    }

    IEnumerator failTask()
    {
        yield return new WaitForSeconds(delayTime);

        if(loadingPanel)
        {
            loadingPanel.SetActive(false);
        }

        checkPanel.SetActive(false);
        failPanel.SetActive(true);
        congratsPanel.SetActive(false);


        gameController.addMistake();
    }

    IEnumerator completedTask()
    {
        yield return new WaitForSeconds(delayTime);

        if (loadingPanel)
        {
            loadingPanel.SetActive(false);
        }

        checkPanel.SetActive(false);
        failPanel.SetActive(false);
        congratsPanel.SetActive(true);
    }
}
