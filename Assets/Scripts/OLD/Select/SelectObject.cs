using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
//using TMPro;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SelectObject : MonoBehaviour
{
    //Possible objs to select
    public Selectable[] selectObjs;
    public static List<Selectable> totalSelectables;

    //List of selected objs
    private List<Selectable> selected;

    //Text element
    public Text instrText;

    public string checkID;
    private string initPhrase;

    //Total of needed elements
    private int totalNeeded = 0;

    //Btn gameobjects
    public GameObject checkPanel;
    public GameObject congratsPanel;
    public GameObject failPanel;

    //Animation controller
    public Animator workerAnimator;

    //Loading element
    public GameObject loadingImg;

    //Check bool
    private bool fail;

    //Game Controller element
    public GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        initPhrase = instrText.text;

        InitLists();
        InitHighight();
    }

    void Update()
    {
        //Update number of selections
        if (instrText.text.Contains(initPhrase) && totalNeeded > 1)
        {
            instrText.text = initPhrase + "\n" + selected.Count + "/" + totalNeeded;
        }
    }

    public void InitHighight()
    {
        foreach(Selectable sel in totalSelectables)
        {
            sel.objSelectable.GetComponent<Highlight>().enabled = true;
        }
    }

    public void InitLists()
    {
        //Init conditions
        instrText.text = initPhrase;
        fail = false;
        totalNeeded = 0;

        //Play idle animation if there is an NPC
        if (workerAnimator != null)
        {
            workerAnimator.Play("Idle");
        }

        checkPanel.SetActive(true);
        failPanel.SetActive(false);
        congratsPanel.SetActive(false);

        if (totalSelectables == null || totalSelectables.Count == 0)
        {
            totalSelectables = selectObjs.ToList<Selectable>();
        }

        foreach (Selectable element in totalSelectables)
        {
            element.objSelectable.SetActive(true);
            element.objSelectable.layer = LayerMask.NameToLayer("Selectable");

            if (element.isNeeded)
            {
                totalNeeded += 1;
            }
        }

        selected = new List<Selectable>();
    }

    public void selectObject(GameObject obj)
    {
        if(totalSelectables.Count > 0)
        {
            foreach (var element in totalSelectables)
            {
                if (element.objSelectable == obj)
                {
                    selected.Add(element);

                    obj.SetActive(false);
                }
            }
        }
    }

    public void checkSelection()
    {
        instrText.text = MainTranslate.Fields[checkID] + "\n" + "\n";

        if(selected.Count < totalNeeded || totalNeeded < selected.Count)
        {
            StartCoroutine(failedMission());
        }
        else
        {
            foreach (Selectable element in selected)
            {
                int index = totalSelectables.IndexOf(element);

                if (totalSelectables[index] == element)
                {
                    if (totalSelectables[index].isNeeded == false)
                    {
                        fail = true;

                        break;
                    }
                }
            }

            if (selected.Count == 0 || fail == true)
            {
                StartCoroutine(failedMission());
            }
            else
            {
                StartCoroutine(completedMission());
            }
        }

        
    }

    IEnumerator failedMission()
    {
        yield return new WaitForSeconds(2f);

        loadingImg.SetActive(false);

        if (workerAnimator)
        {
            workerAnimator.Play("Shaking Head No");
        }

        gameController.addMistake();

        checkPanel.SetActive(false);
        congratsPanel.SetActive(false);
        failPanel.SetActive(true);
    }

    IEnumerator completedMission()
    {
        yield return new WaitForSeconds(2f);

        loadingImg.SetActive(false);

        if (workerAnimator)
        {
            workerAnimator.Play("Head Nod Yes");
        }

        checkPanel.SetActive(false);
        congratsPanel.SetActive(true);
        failPanel.SetActive(false);
    }
}
