using UnityEngine;

public class CheckQuizCompletion : MonoBehaviour
{
    public MultipleSelectionQuiz[] quizzes;  // Asigna los elementos MultipleSelectionQuiz en el Inspector
    public Instructions instructionObject;   // Asigna el objeto Instructions en el Inspector
    //public string nextInstruction;

    void Update()
    {
        // Verifica si todos los quizzes tienen panelCompleted en true
        bool allQuizzesCompleted = true;

        foreach (var quiz in quizzes)
        {
            if (!quiz.panelCompleted)
            {
                allQuizzesCompleted = false;
                break;
            }
        }


        // Si todos los quizzes están completos, finaliza la instrucción actual e inicializa la siguiente
        if (allQuizzesCompleted)
        {
            if (instructionObject != null && instructionObject.GetCountOfUnanswered() > 0)
            {
                instructionObject.finishInstruction();
                instructionObject.setNextInstructions();

                this.enabled = false;
            }
        }
    }
}