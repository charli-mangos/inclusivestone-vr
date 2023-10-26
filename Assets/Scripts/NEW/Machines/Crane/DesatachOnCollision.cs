using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesatachOnCollision : MonoBehaviour
{
    public CraneController controller;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("MoveArea"))
        {
            HingeJoint joint = GetComponent<HingeJoint>();

            if (joint && controller.power < 0)
            {
                Destroy(joint);
                joint = null;

                this.transform.Find("Amarrado").gameObject.SetActive(false);
            }
        }
    }
}
