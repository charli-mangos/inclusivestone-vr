using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

[System.Serializable]
public class QuestionOption
{
    public string name;
    public string fact;
    public Sprite image;
    public Sprite highLightImage;
    public string response;
    public bool isCorrect;
}