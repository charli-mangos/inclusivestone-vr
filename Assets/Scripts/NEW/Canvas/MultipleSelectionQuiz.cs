using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MultipleSelectionQuiz : MonoBehaviour
{
    //Unanswered questions
    public Question[] questions;
    private List<Question> unanswered;

    //Current question
    private Question currentQuestion;

    //Text fact component
    public Text questFact;

    //Button elements and response text placement
    public Button[] optionBtns;
    public Text[] responsesText;
    public Image imgComp;

    //Correct answer
    private List<string> correctResponse;

    //Selected answer
    private List<string> selected;

    //Question panel gameobject
    public GameObject questPanel;
    //Wrong result panel gameobject
    public GameObject wrongPanel;
    //Next panel gameobject
    public GameObject nextPanel;

    //Reinit button option
    public bool reInitButtons = true;

    //Game Controller element
    public GameController gameController;

    public bool panelCompleted = false;


    private void Start()
    {
        InitQuiz();
    }

    public void InitQuiz()
    {
        questPanel.SetActive(true);
        wrongPanel.SetActive(false);

        unanswered = questions.ToList<Question>();

        foreach (var but in optionBtns)
        {
            but.interactable = true;
        }

        SetNextQuestion();
    }

    void SetNextQuestion()
    {
        //Randomizer
        //currentQuestion = questions[Random.Range(0, questions.Count)];

        correctResponse = new List<string>();
        selected = new List<string>();

        if (unanswered.Count > 0)
        {
            currentQuestion = unanswered[0];

            questFact.text = MainTranslate.Fields[currentQuestion.fact];


            if (imgComp)
            {
                imgComp.sprite = currentQuestion.imageFact;
            }

            //If there is extra data update it
            /*if (extraText)
            {
                extraText.text = "";

                foreach (string dataID in currentQuest.extraData)
                {
                    extraText.text += "- " + MainTranslate.Fields[dataID] + Environment.NewLine;
                }
            }*/

            SetAnswerOptions();
        }
        else
        {
            finishQuiz();
        }
    }

    void SetAnswerOptions()
    {
        int i = 0;

        //Check every option in question and update the button
        foreach (var but in currentQuestion.options)
        {
            //Change button name
            optionBtns[i].gameObject.name = but.name;

            if (reInitButtons)
            {
                optionBtns[i].interactable = true;
            }


            //Change option button content
            if (optionBtns[i].transform.Find("Text"))
            {
                optionBtns[i].transform.Find("Text").GetComponent<Text>().text = MainTranslate.Fields[but.fact];
            }

            //If there are images change the values
            if (but.image)
            {
                optionBtns[i].GetComponent<Image>().sprite = but.image;
                SpriteState st = new SpriteState();
                st.highlightedSprite = but.highLightImage;
                st.pressedSprite = but.highLightImage;
                optionBtns[i].spriteState = st;
                optionBtns[i].targetGraphic = optionBtns[i].GetComponent<Image>();
            }

            //Change response text
            if (responsesText.Length > 0)
            {
                responsesText[i].text = MainTranslate.Fields[but.response];
                responsesText[i].gameObject.name = but.name;
            }

            //Get correct responses
            if (currentQuestion.options[i].isCorrect)
            {
                correctResponse.Add(but.name);
            }

            i++;
        }
    }

    public void setExternalResponse(string text)
    {
        InitQuiz();

        foreach (var but in currentQuestion.options)
        {
            if (but.name == text)
            {
                but.isCorrect = true;
            }
        }
        //currentQuestion.options.Find(element => element.name == name).isCorrect = true;
        //SetAnswerOptions();
    }

    public void selectButton(GameObject button)
    {
        selected.Add(button.name);
        button.GetComponent<Button>().interactable = false;

        if (correctResponse.Count == 1)
        {
            checkResponse();
        }
    }

    public void checkResponse()
    {
        if (selected.Count == correctResponse.Count)
        {
            //Quiz completado
            foreach (var resp in selected)
            {
                if (!correctResponse.Contains(resp))
                {
                    failQuestion();
                    return;
                }
            }

            completedQuestion();
        }
        else
        {
            failQuestion();
        }
    }


    void completedQuestion()
    {
        unanswered.Remove(currentQuestion);
        SetNextQuestion();
    }

    void failQuestion()
    {
        questPanel.SetActive(false);
        wrongPanel.SetActive(true);

        if (gameController != null)
        {
            gameController.addMistake();

        }
    }

    void finishQuiz()
    {
        questPanel.SetActive(false);
        wrongPanel.SetActive(false);

        if (nextPanel)
        {
            panelCompleted = true;
            nextPanel.SetActive(true); 
        }

        if (this.GetComponentInParent<TargetWeight>() != null)
        {
            this.GetComponentInParent<TargetWeight>().panelCompleted = true;
        }
    }
}

