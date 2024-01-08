using System.Collections;
using UnityEngine;

public class ActivateQuiz : MonoBehaviour
{
    public MultipleSelectionQuiz quizScript;  // Asigna el script del quiz en el Inspector
    public GameObject quizPanel;  // Asigna el panel del quiz en el Inspector
    public Transform gameController;  // Asigna el objeto GameController en el Inspector
    public float activationDistance = 1.0f;

    private bool quizPanelActive = false;

    private void Start()
    {
        quizPanel.SetActive(false);
    }

    private void Update()
    {
        float distanceToGameController = Vector3.Distance(transform.position, gameController.position);

        if (distanceToGameController < activationDistance && !quizScript.panelCompleted)
        {
            // Verificar si hay algún otro panel de quiz activo
            MultipleSelectionQuiz[] allQuizzes = FindObjectsOfType<MultipleSelectionQuiz>();

            foreach (var otherQuiz in allQuizzes)
            {
                if (otherQuiz.questPanel.activeSelf)
                {
                    // Hay otro quiz activo, no activar este quiz
                    return;
                }
            }

            if (!quizPanelActive)
            {
                quizPanel.SetActive(true);
                quizPanelActive = true;
            }
        }
        else
        {
            if (quizPanelActive)
            {
                quizPanel.SetActive(false);
                quizPanelActive = false;
            }
        }
    }

    IEnumerator ActivateQuizWithDelay()
    {
        yield return new WaitForSeconds(1.0f);  // Espera un segundo antes de activar el quiz
        quizPanel.SetActive(true);
        quizPanelActive = true;
    }
}