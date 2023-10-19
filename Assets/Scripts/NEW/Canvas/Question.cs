using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

[System.Serializable]
public class Question
{
    public string title;
    public string fact;

    public Sprite imageFact;

    public string[] extraData;

    public QuestionOption[] options;
}
