using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitTranslation : MonoBehaviour
{
    public string TextId;

    public string extraTextID;

    public string extraText;

    public bool nextLine = false;

    private Text text;

    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
        InitUnit();
    }

    public void ResetUnit()
    {
        text.text = "";
        InitUnit();
    }

    public void InitUnit()
    {
        if (text != null)
        {
            if (TextId == "ISOCode")
            {
                text.text = MainTranslate.GetLanguage();
            }
            else
            {
                text.text = MainTranslate.Fields[TextId];
            }

            if (extraText != null && nextLine == false)
            {
                text.text = text.text + " " + extraText;
            }
            else if (nextLine == true)
            {
                text.text = text.text + "\n" + "\n" + MainTranslate.Fields[extraTextID];
            }
        }
    }

}
