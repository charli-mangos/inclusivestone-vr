using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectObject : MonoBehaviour
{
    public Selectable[] selectObjs;
    public static List<Selectable> totalSelectables;

    private List<Selectable> selected;
    private int totalNeeded = 0;

    public GameObject checkPanel;
    public Animator workerAnimator;

    public Color highlightColor = Color.white;
    public Color notSelectedColor = Color.white;

    private Instructions ins;
    private bool fail;
    private bool finished;

    void Start()
    {
        finished = false;
        InitLists();
        InitHighight();
        ins = checkPanel.GetComponent<Instructions>();
    }

    void FixedUpdate()
    {
        if (!finished && selected.Count == totalNeeded)
        {
            checkSelection();
        }
    }

    public void InitHighight()
    {
        foreach (Selectable sel in totalSelectables)
        {
            sel.objSelectable.GetComponent<Highlight>().enabled = true;
        }
    }

    public void InitLists()
    {
        fail = false;
        totalNeeded = 0;

        if (workerAnimator != null)
        {
            workerAnimator.Play("Idle");
        }

        if (totalSelectables == null || totalSelectables.Count == 0)
        {
            totalSelectables = selectObjs.ToList<Selectable>();
        }

        foreach (Selectable element in totalSelectables)
        {
            //SetSelectableState(element, true, "Selectable");
            SetSelectableState(element, notSelectedColor, true);

            if (element.isNeeded)
            {
                totalNeeded += 1;
            }
        }

        selected = new List<Selectable>();
    }

    public void selectObject(GameObject obj)
    {
        int highlightLayer = LayerMask.NameToLayer("Highlight");

        if (totalSelectables.Count > 0 && obj.layer == highlightLayer)
        {
            var foundElement = totalSelectables.FirstOrDefault(element => element.objSelectable == obj);

            if (foundElement != null)
            {
                selected.Add(foundElement);
                obj.SetActive(false);
                SetSelectableState(foundElement, highlightColor, false);

                if (selected.Count == 1 && ins.GetCurrentInstructionsName() != "SelectedProducts")
                {
                    ins.finishInstruction();
                    ins.setNextInstructions();
                }
            }
        }
    }

    public void cancelObject(GameObject obj)
    {
        if (totalSelectables.Count > 0)
        {
            var foundElement = totalSelectables.FirstOrDefault(element => element.objSelectable == obj);
            if (foundElement != null)
            {
                SetSelectableState(foundElement, notSelectedColor, true);
                obj.SetActive(true);
                selected.Remove(foundElement);
                // Deseleccionar cualquier elemento seleccionado en el EventSystem
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
    }


    public void checkSelection()
    {
        foreach (Selectable element in selected)
        {
            if (totalSelectables.Contains(element) && !element.isNeeded)
            {
                fail = true;
                break;
            }
        }

        if (!fail)
        {
            StartCoroutine(completedMission());
            finished = true;
        }
        else
        {
            fail = false;
        }
    }

    IEnumerator completedMission()
    {
        yield return new WaitForSeconds(1.5f);

        if (workerAnimator)
        {
            workerAnimator.Play("Head Nod Yes");
        }

        ins.finishInstruction();
        ins.setNextInstructions();

        this.enabled = false;
    }

    private void SetSelectableState(Selectable selectable, Color color, bool showCancel)
    {
        PlayerPrefs.SetString("actual_HighLight", "None");
        selectable.objSelectable.layer = LayerMask.NameToLayer(showCancel ? "Selectable" : "Default");
        selectable.ImageOBJ.GetComponent<Image>().color = color;
        selectable.ImageOBJ.transform.Find("Cancel").gameObject.SetActive(!showCancel);
        selectable.objSelectable.SetActive(showCancel);
    }
}