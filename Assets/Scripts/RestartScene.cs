using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartScene : MonoBehaviour
{
    void Start()
    {
        // Obtén el botón al que se adjunta este script
        Button btn = GetComponent<Button>();
        // Asigna la función de reinicio a la acción del botón
        btn.onClick.AddListener(OnRestartButtonClick);
    }

    void OnRestartButtonClick()
    {
        // Obtiene el número de la escena actual
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        // Carga la escena actual
        SceneManager.LoadScene(currentSceneIndex);
    }
}