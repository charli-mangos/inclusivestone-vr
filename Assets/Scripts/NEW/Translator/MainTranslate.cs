using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainTranslate : MonoBehaviour
{
    // Text Fields
    /// Useage: Fields[key]
    /// Example: MainTranslate.Fields["world"]
    public static Dictionary<String, String> Fields { get; private set; }

    // Language value
    public static string lang;

    // Init on first use
    static MainTranslate()
    {
        LoadLanguage();
    }

    // Load language files from ressources
    private static void LoadLanguage()
    {
        // Init Fields
        if (Fields == null)
        {
            Fields = new Dictionary<string, string>();

            lang = Get2LetterISOCodeFromSystemLanguage().ToLower();
        }

        Fields.Clear();

        var textAsset = Resources.Load(@"Lang/" + lang); //no .txt needed

        string allTexts = "";
        if (textAsset == null)
        {
            textAsset = Resources.Load(@"Lang/en") as TextAsset; //no .txt needed
        }

        if (textAsset == null)
        {
            Debug.LogError("File not found for I18n: Assets/Resources/Lang/" + lang + ".txt");
        }

        allTexts = (textAsset as TextAsset).text;

        string[] lines = allTexts.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);

        string key, value;
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].IndexOf("=") >= 0 && !lines[i].StartsWith("#"))
            {
                key = lines[i].Substring(0, lines[i].IndexOf("="));
                value = lines[i].Substring(lines[i].IndexOf("=") + 1, lines[i].Length - lines[i].IndexOf("=") - 1).Replace("\\n", Environment.NewLine);

                Fields.Add(key, value);
            }
        }
    }

    // get the current language
    public static string GetLanguage()
    {
        return Get2LetterISOCodeFromSystemLanguage().ToLower();
    }

    // get the current language
    public static void SetLanguage(string ISOCode)
    {
        lang = ISOCode;


        PlayerPrefs.SetString("lang", ISOCode);

        LoadLanguage();
    }

    // The 2-letter ISO code from system language. 
    /// Helps to convert Unity's Application.systemLanguage to a 
    /// 2 letter ISO country code. There is unfortunately not more
    /// countries available as Unity's enum does not enclose all
    /// countries.
    public static string Get2LetterISOCodeFromSystemLanguage()
    {
        SystemLanguage language = Application.systemLanguage;
        string res = "EN";
        switch (language)
        {
            case SystemLanguage.Afrikaans: res = "AF"; break;
            case SystemLanguage.Arabic: res = "AR"; break;
            case SystemLanguage.Basque: res = "EU"; break;
            case SystemLanguage.Belarusian: res = "BY"; break;
            case SystemLanguage.Bulgarian: res = "BG"; break;
            case SystemLanguage.Catalan: res = "CA"; break;
            case SystemLanguage.Chinese: res = "ZH"; break;
            case SystemLanguage.ChineseSimplified: res = "ZH"; break;
            case SystemLanguage.ChineseTraditional: res = "ZH"; break;
            case SystemLanguage.Czech: res = "CS"; break;
            case SystemLanguage.Danish: res = "DA"; break;
            case SystemLanguage.Dutch: res = "NL"; break;
            case SystemLanguage.English: res = "EN"; break;
            case SystemLanguage.Estonian: res = "ET"; break;
            case SystemLanguage.Faroese: res = "FO"; break;
            case SystemLanguage.Finnish: res = "FI"; break;
            case SystemLanguage.French: res = "FR"; break;
            case SystemLanguage.German: res = "DE"; break;
            case SystemLanguage.Greek: res = "EL"; break;
            case SystemLanguage.Hebrew: res = "IW"; break;
            case SystemLanguage.Hungarian: res = "HU"; break;
            case SystemLanguage.Icelandic: res = "IS"; break;
            case SystemLanguage.Indonesian: res = "IN"; break;
            case SystemLanguage.Italian: res = "IT"; break;
            case SystemLanguage.Japanese: res = "JA"; break;
            case SystemLanguage.Korean: res = "KO"; break;
            case SystemLanguage.Latvian: res = "LV"; break;
            case SystemLanguage.Lithuanian: res = "LT"; break;
            case SystemLanguage.Norwegian: res = "NO"; break;
            case SystemLanguage.Polish: res = "PL"; break;
            case SystemLanguage.Portuguese: res = "PT"; break;
            case SystemLanguage.Romanian: res = "RO"; break;
            case SystemLanguage.Russian: res = "RU"; break;
            case SystemLanguage.SerboCroatian: res = "SH"; break;
            case SystemLanguage.Slovak: res = "SK"; break;
            case SystemLanguage.Slovenian: res = "SL"; break;
            case SystemLanguage.Spanish: res = "ES"; break;
            case SystemLanguage.Swedish: res = "SV"; break;
            case SystemLanguage.Thai: res = "TH"; break;
            case SystemLanguage.Turkish: res = "TR"; break;
            case SystemLanguage.Ukrainian: res = "UK"; break;
            case SystemLanguage.Unknown: res = "EN"; break;
            case SystemLanguage.Vietnamese: res = "VI"; break;
        }

        return res;
    }

}
