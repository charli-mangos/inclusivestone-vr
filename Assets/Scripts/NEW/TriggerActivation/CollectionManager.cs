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

    // Update is called once per frame
    void Update()
    {
        foreach(var col in uncompleted)
        {
            if (col.isCompleted)
            {
                uncompleted.Remove(col);
            }
            else
            {
                finished = false;
            }
        }

        if(finished == true)
        {
            gameController.finishMission();
        }
    }
}
