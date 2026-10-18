using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitRebound : MonoBehaviour
{
    public float bounceForce; 
    public Material limitMaterial;

    private Material oldMaterial;

    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();

        if(rend != null)
        {
            oldMaterial = rend.material;
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag == "Player")
        {
            Rigidbody rb = col.rigidbody;

            rb.AddExplosionForce(bounceForce, col.contacts[0].point, 0.01f);

            /*if (Physics.Raycast(rb.position, rb.velocity, out RaycastHit hit, rb.velocity.magnitude))//cast the ray 100 units at the specified direction
            {
                if (hit.collider.name == "Wall")
                {
                    print("Raycast hits wall");
                    //find new ray direction
                    Vector3 newVelocity = Vector3.Reflect(rb.velocity, hit.normal);
                    Debug.DrawRay(hit.point, newVelocity.normalized * 8, Color.magenta);

                    // Set new rotation and velocity.
                    rb.MoveRotation(Quaternion.LookRotation(newVelocity, transform.up));
                    rb.velocity = newVelocity;
                    //hitpoint and indirection now face the correct way
                }
            }*/
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {

            if (rend != null && limitMaterial != null)
            {
                rend.material = limitMaterial;
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {

            if (rend != null && oldMaterial != null)
            {
                rend.material = oldMaterial;
            }
        }
    }
}
