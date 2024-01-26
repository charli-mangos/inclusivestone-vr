using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CollectionManager : MonoBehaviour
{
    //GameObjects to collect
    public CollectorObjects[] collector;
    private List<CollectorObjects> uncompleted;

    private bool finished = true;

    public GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        if (uncompleted == null || uncompleted.Count == 0)
        {
            uncompleted = collector.ToList<CollectorObjects>();
        }
    }

    void Update()
    {
        
        if (uncompleted.Count == 0 && finished == true)
        {
            Debug.Log("Se limpio");
            gameController.finishMission();
            this.enabled = false;
        }
    }

    // Update is called once per frame
    public void checkIfCompleted()
    {

        if (uncompleted.Count > 0)
        {
            foreach (var col in uncompleted)
            {
                if (!col.isCompleted)
                {
                    finished = false;
                }
                else
                {
                    Debug.Log("Completatis");
                    uncompleted.Remove(col);
                }

            }
        }
    }
}
