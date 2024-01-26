using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Mission : MonoBehaviour
{
    //Mission tittle
    public string tittle;

    //Turns true if mission is completed
    public bool isCompleted = false;

    //Turns true when mission begin
    //private bool isStarted = false;

    //Y value of signal
    public float highVal;

    //Signal gameObject
    public GameObject prefabSignal;
    private GameObject signal;

    //Canvas element
    public GameObject missionCanvas;
    public GameObject missionTutorial;

    //Next mission UI
    [SerializeField] private GameObject finMissionPanel;
    [SerializeField] private SlideController slider;

    public string nextMissionTittle;

    //List of objects to block after finiching the mission
    [SerializeField] private GameObject[] toBlock;

    //Machine Controller
    public bool isCar;
    [ConditionalShow("isCar")][SerializeField] private CarController carController;
    [ConditionalShow("isCar")] [SerializeField] private MoveUp moveUp;
    [ConditionalShow("isCar")] [SerializeField] private RotateElement rotate;
    [ConditionalShow("isCar")] [SerializeField] private MoveRetractil moveRe;

    //Ray Controllers
    [SerializeField] private GameObject leftRayCotroller;
    [SerializeField] private GameObject rightRayCotroller;

    void Start()
    {
        if (missionTutorial != null)
        {
            missionTutorial.SetActive(true);
        }
        else
        {
            initMission();
        }
    }

    //Is called when mission started
    public void initMission()
    {
        //isStarted = true;
        if(missionCanvas != null)
        {
            missionCanvas.SetActive(true);
            //canvas.GetComponent<Instructions>().setNextInstructions();
        }

        //Instantiate warning signal
        if (signal == null && prefabSignal != null)
        {
            signal = Instantiate(prefabSignal, new Vector3(missionCanvas.transform.position.x, highVal, missionCanvas.transform.position.z), Quaternion.identity) as GameObject;
            signal.transform.parent = this.transform;
        }

        /*if (canvas.GetComponent<Instructions>())
        {
            canvas.GetComponent<Instructions>().startInstructions = true;
            canvas.GetComponent<Instructions>().setNextInstructions();
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if (isCompleted)
        {
            //Destroy warning signal
            StartCoroutine(FinalizeMission());

        }
        /*else if (isStarted)
        {
            //Check mission targets 
        }*/
    }

    IEnumerator FinalizeMission()
    {
        Debug.Log("Fin " + this.tittle);

        if(missionCanvas != null)
        {
            missionCanvas.SetActive(false);

        }

        Destroy(signal, 1f);

        yield return new WaitForSeconds(0.5f);

        //Is a car machine
        if (isCar)
        {
            moveRe.enabled = false;
            moveUp.enabled = false;
            rotate.enabled = false;

            carController.enabled = false;
        }
       
        //Active false every object in list toBlock
        foreach(GameObject var in toBlock)
        {
            var.SetActive(false);
        }

        //Finish panel
        if(finMissionPanel != null)
        {
            finMissionPanel.SetActive(true);

            Debug.Log("Tenemos panel");

            //Se destruye el botón de la mission completada
            if (FindDescendantByNameRecursive(finMissionPanel.transform, tittle))
            {
                Destroy(FindDescendantByNameRecursive(finMissionPanel.transform, tittle).gameObject);
            }
        }


        if(slider != null)
        {
            //Activate slider
            slider.initSlider();

            //slider.removeSlide(slider.getSlideByName(tittle));
            slider.setSlideByName(nextMissionTittle);
        }
        

        //Activate Ray interactors
        leftRayCotroller.SetActive(true);
        rightRayCotroller.SetActive(true);

        //goalzone.SetActive(false);
        this.enabled = false;
    }

    // Función recursiva para buscar un objeto descendiente por nombre
    private Transform FindDescendantByNameRecursive(Transform parent, string name)
    {
        foreach (Transform child in parent)
        {
            if (child.name == name)
            {
                return child; // Se encontró el objeto descendiente
            }

            Transform result = FindDescendantByNameRecursive(child, name);

            if (result != null)
            {
                return result; // Se encontró el objeto descendiente en un hijo
            }
        }

        return null; // No se encontró el objeto descendiente
    }
}
