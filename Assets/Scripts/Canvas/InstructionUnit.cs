using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class InstructionUnit
{
    //Panel object
    public GameObject panel;

    //Description text
    public string[] description;

    //Text object
    public Text canvasText;

    //Check if instruction is finished
    public bool isCompleted = false;

    //Check if instruction is temporary
    public bool isTemporary = false;
}
