using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTranslator : MonoBehaviour
{
    public GameObject selectLanguagePanel;
    public GameObject chooseScene;

    private Text[] allText;

    void Start()
    {
        string generalLang = PlayerPrefs.GetString("lang");

        if(generalLang != null)
        {
            SetDefault(generalLang);
        }
    }

    void SetDefault(string ISOCode)
    {

        MainTranslate.SetLanguage(ISOCode);

    }

    public void SetLanguage(string ISOCode)
    {
        MainTranslate.SetLanguage(ISOCode);

        Text[] yourLabels = FindObjectsOfType<Text>();


        foreach (Text label in yourLabels)
        {
            if (label.GetComponent<UnitTranslation>())
            {
                label.GetComponent<UnitTranslation>().ResetUnit();
            }
        }
    }
}
