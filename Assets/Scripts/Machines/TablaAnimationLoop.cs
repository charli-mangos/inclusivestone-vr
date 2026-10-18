using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TablaAnimationLoop : MonoBehaviour
{
    private GameObject currentTabla, nextTabla, tabla1, tabla5, tabla3, tabla4;

    private Animator anim;

    public Animator cortadora1;

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Tabla"))
        {
            currentTabla = col.gameObject;
            nextTabla = this.transform.Find("TablaAnimada2").gameObject;
            tabla1 = currentTabla.transform.Find("Tabla1").gameObject;
            tabla3 = currentTabla.transform.Find("Tabla3").gameObject;
            tabla4 = currentTabla.transform.Find("Tabla4").gameObject;
            tabla5 = currentTabla.transform.Find("Tabla5").gameObject;

            anim = currentTabla.GetComponent<Animator>();

            if (tabla1.activeSelf)
            {
                tabla1.SetActive(false);
                tabla3.SetActive(true);
            }
            else if (tabla3.activeSelf)
            {
                if (!nextTabla.activeSelf)
                {
                    nextTabla.SetActive(true);
                    cortadora1.Play("MovilCortadora");
                }

                tabla3.SetActive(false);
                tabla4.SetActive(true);
            }
            else if (tabla4.activeSelf)
            {
                tabla4.SetActive(false);
                tabla5.SetActive(true);
            }
            else if (tabla5.activeSelf)
            {

                Invoke("RestartLoop", 2f);
            }
        }
    }

    private void RestartLoop()
    {

        tabla5.SetActive(false);
        tabla1.SetActive(true);

        anim.Play("Tabla1");
        cortadora1.Play("MovilCortadora");

    }
}
