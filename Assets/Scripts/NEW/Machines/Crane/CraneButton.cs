using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CraneButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private string state = "None";

    [SerializeField]
    private int power = 0;

    [SerializeField]
    private CraneController control;

    //If button is clicked down
    public void OnPointerDown(PointerEventData eventData)
    {
        if(control.state == "None" || this.state == "None" || this.state == "Start")
        {
            control.power = this.power;
            control.state = this.state;
        }
    }

    //If button is clicked up
    public void OnPointerUp(PointerEventData eventData)
    {
        if(control.state != "Block")
        {
            control.power = 0;
            control.state = "None";
        }
    }

}
