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
    public Question[] questions;
    private List<Question> unanswered;
    private Question currentQuestion;
    private List<string> correctResponse;
    private List<string> selected;

    public Text questFact;
    public Button[] optionBtns;
    public Text[] responsesText;
    public Image imgComp;

    public GameObject questPanel;
    public GameObject wrongPanel;
    public GameObject nextPanel;

    public bool reInitButtons = true;
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

        unanswered = questions.ToList();

        foreach (var button in optionBtns)
        {
            button.interactable = true;
        }

        SetNextQuestion();
    }

    void SetNextQuestion()
    {
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

            SetAnswerOptions();
        }
        else
        {
            finishQuiz();
        }
    }

    void SetAnswerOptions()
    {
        for (int i = 0; i < currentQuestion.options.Length; i++)
        {
            var button = optionBtns[i];
            var option = currentQuestion.options[i];

            button.gameObject.name = option.name;

            if (reInitButtons)
            {
                button.interactable = true;
            }

            if (button.transform.Find("Text"))
            {
                button.transform.Find("Text").GetComponent<Text>().text = MainTranslate.Fields[option.fact];
            }

            if (option.image)
            {
                button.GetComponent<Image>().sprite = option.image;
                SpriteState st = new SpriteState
                {
                    highlightedSprite = option.highLightImage,
                    pressedSprite = option.highLightImage
                };
                button.spriteState = st;
                button.targetGraphic = button.GetComponent<Image>();
            }

            if (responsesText.Length > 0)
            {
                responsesText[i].text = MainTranslate.Fields[option.response];
                responsesText[i].gameObject.name = option.name;
            }

            if (option.isCorrect)
            {
                correctResponse.Add(option.name);
            }
        }
    }

    public void setExternalResponse(string text)
    {
        InitQuiz();

        foreach (var option in currentQuestion.options)
        {
            if (option.name == text)
            {
                option.isCorrect = true;
            }
        }
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
        if (selected.Count == correctResponse.Count && selected.All(resp => correctResponse.Contains(resp)))
        {
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

    public void setPanelCompleted()
    {
        panelCompleted = true;
    }

    void finishQuiz()
    {
        questPanel.SetActive(false);
        wrongPanel.SetActive(false);

        if (nextPanel)
        {
            nextPanel.SetActive(true);
        }

        var targetWeight = GetComponentInParent<TargetWeight>();
        if (targetWeight != null)
        {
            targetWeight.panelCompleted = true;
        }
    }
}