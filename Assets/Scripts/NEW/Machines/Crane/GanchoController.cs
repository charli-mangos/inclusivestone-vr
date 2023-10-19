using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GanchoController : MonoBehaviour
{
    [SerializeField]
    private CraneController crane;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Gancho" && crane.state == "up_down")
        {
            if (crane.power > 0 && !crane.isUp)
            {
                crane.isUp = true;
            }

            if (crane.power < 0 && !crane.isDown)
            {
                crane.isDown = true;
            }
        }
    }

}
