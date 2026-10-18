using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple = true)]
public class ConditionalShowAttribute : PropertyAttribute
{
    public string conditionalVariableName;

    public ConditionalShowAttribute(string boolVariableName)
    {
        conditionalVariableName = boolVariableName;
    }
}