using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetObject : MonoBehaviour
{
    [SerializeField]
    public string elementTag;

    private FixedJoint joint;

    private IsGround ground;

    void OnTriggerEnter(Collider col)
    {
        // Set a tag to make the conection selective.
        if (joint == null && col.gameObject.tag == elementTag)
        {

            ground = col.GetComponent<IsGround>();

            if (ground.ground == false)
            {
                joint = this.gameObject.AddComponent<FixedJoint>();
                joint.connectedBody = col.GetComponent<Rigidbody>();
                col.transform.SetParent(this.transform);
                col.GetComponent<Rigidbody>().isKinematic = true;
            }
        }

    }


    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == elementTag)
        {

            ground = col.GetComponent<IsGround>();

            if (ground.ground == true)
            {
                Destroy(joint);
                joint = null;
                col.transform.SetParent(null);
                col.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }

}
