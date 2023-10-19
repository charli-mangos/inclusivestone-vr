using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGround : MonoBehaviour
{
    public bool ground;

    private ObstacleCrash newOBS;

    public bool isObstacle = true;


    void Start() { }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Floor")
        {
            ground = true;

            if(isObstacle) Destroy(newOBS);

            FixedJoint joint = transform.GetComponent<FixedJoint>();
            if (joint) Destroy(joint);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.gameObject.tag == "Floor")
        {
            ground = false;

            if(isObstacle) newOBS = this.gameObject.AddComponent<ObstacleCrash>();
        }

    }
}
