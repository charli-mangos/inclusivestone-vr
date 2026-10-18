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
        if (uncompleted.Count == 0 && finished)
        {
            gameController.finishMission();
            this.enabled = false;
        }
    }

    // Drops the collectors that are already completed and records whether any remain.
    public void checkIfCompleted()
    {
        uncompleted.RemoveAll(col => col.isCompleted);
        finished = uncompleted.Count == 0;
    }
}
