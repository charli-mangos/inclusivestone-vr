using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameController : MonoBehaviour
{
    public Mission[] missions;
    public List<Mission> uncompleted;

    //Current mission active
    private Mission currentMission;

    //Check if scene has started
    public bool startScene = false;

    //Failed panel
    public GameObject mistakePanel;

    //Move control
    public LocomotionController moveControl;

    private int mistakes;

    [SerializeField] private bool multipleMission = false;

    // Start is called before the first frame update
    void Start()
    {
        if (uncompleted == null || uncompleted.Count == 0)
        {
            uncompleted = missions.ToList<Mission>();

            uncompleted.RemoveAll(mis => mis.isCompleted);
        }

        mistakes = 0;
    }

    void Update()
    {
        if(mistakes > 2)
        {
            GameObject[] objByLayer = FindInActiveObjectsByLayer(LayerMask.NameToLayer("UI"));

            foreach (var obj in objByLayer)
            {
                if (obj.tag  != "MistakePanel")
                {
                    obj.SetActive(false);
                }
            }

            if (!mistakePanel.activeSelf)
            {
                mistakePanel.SetActive(true);
            }
           
            moveControl.EnableRightTeleport = false;
            moveControl.EnableLeftTeleport = false;
        }
    }

    public void addMistake()
    {
        mistakes += 1;

    }

    public Mission getCurrentMission()
    {
        return currentMission;
    }

    public void setNextMission()
    {
        currentMission = uncompleted[0];

        currentMission.gameObject.SetActive(true);

    }

    public void setMissionByName(string name)
    {

        currentMission = uncompleted.Find(item => item.tittle == name);

        currentMission.gameObject.SetActive(true);

    }

    public void finishMission()
    {
        currentMission.isCompleted = true;

        if (currentMission != null)
        {
            uncompleted.Remove(currentMission);
        }

        if(uncompleted.Count > 0 && multipleMission)
        {
            setNextMission();
        }
    }

    private GameObject[] FindInActiveObjectsByLayer(int layer)
    {
        List<GameObject> validTransforms = new List<GameObject>();
        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].hideFlags == HideFlags.None)
            {
                if (objs[i].gameObject.layer == layer)
                {
                    validTransforms.Add(objs[i].gameObject);
                }
            }
        }
        return validTransforms.ToArray();
    }

}
