using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CheckCol : MonoBehaviour
{
    //Possible objs to select
    public GameObject[] targetObjects;
    private List<GameObject> targetList;

    //List of selected objs
    private List<GameObject> selected;

    // Start is called before the first frame update
    void Start()
    {
        InitList();
    }

    void InitList()
    {
        if (targetList == null || targetList.Count == 0)
        {
            targetList = targetObjects.ToList<GameObject>();
        }

        selected = new List<GameObject>();
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Grab")
        {
            selected.Add(col.gameObject);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Grab")
        {
            selected.Remove(col.gameObject);
        }
    }

    public bool checkResult()
    {
        bool result = false;

        foreach (var element in selected)
        {

            if (targetList.Contains(element))
            {
                result = true;
            }
            else
            {
                result = false;
                break;
            }
        }


        return result;
    }
}
