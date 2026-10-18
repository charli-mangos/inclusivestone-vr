using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObstacleCrash : MonoBehaviour
{
    //List of cones names
    private List<string> circuit;

    //Side of impact 
    private enum HitDirection { None, Top, Bottom, Forward, Back, Left, Right };

    //Scene name
    private string sceneName;

    void Start()
    {
        Init();

        sceneName = SceneManager.GetActiveScene().name;
    }

    //Start names string
    public void Init()
    {
        circuit =  new List<string>();
    }

    void OnTriggerEnter(Collider col)
    {
        int score = PlayerPrefs.GetInt(sceneName + "_score");//total score of scene

        //if collision is whith an obstacle
        if (col.gameObject.layer == LayerMask.NameToLayer("Obstacles") && !circuit.Contains(col.gameObject.name))
        {
            //if object is a cone
            if(col.gameObject.tag == "Cone" )
            {
                HitCone(col);
            }

            //Decrease score on crash
            StartCoroutine(DecreaseScore(score));
        }

        //If collision is with ZZ object
        if(col.gameObject.tag == "ZigZag")
        {
            HitZZ(col);
        }
    }

    private void HitCone(Collider col)
    {
        //Update cones hitted value 
        int cones = PlayerPrefs.GetInt(sceneName + "_cones");
        PlayerPrefs.SetInt(sceneName + "_cones", cones + 1);

        //Add cone to the hitted cones list
        circuit.Add(col.gameObject.name);
    }

    private void HitZZ(Collider col)
    {
        //Hit direction
        HitDirection dir = ReturnDirection(this.gameObject, col.gameObject);

        //Actual hitted side
        string actualSide = PlayerPrefs.GetString(sceneName + "_zigzag");
        //Num of hits
        int numHits = PlayerPrefs.GetInt(sceneName + "_hitsZZ");

        //If hit direction is right
        if (dir == HitDirection.Right)
        {
            //And previous was left 
            if (actualSide == "Left" || actualSide == "None")
            {
                PlayerPrefs.SetInt(sceneName + "_hitsZZ", numHits + 1);//Increase hits num
            }

            //Update actualSide
            PlayerPrefs.SetString(sceneName + "_zigzag", "Right");
        }

        //If hit direction is left
        if (dir == HitDirection.Left)
        {
            //And previous was right
            if (actualSide == "Right" || actualSide == "None")
            {
                PlayerPrefs.SetInt(sceneName + "_hitsZZ", numHits + 1);//Increase hits num
            }

            //Update actual side
            PlayerPrefs.SetString(sceneName + "_zigzag", "Left");
        }
    }

    private IEnumerator DecreaseScore(int score)
    {
        PlayerPrefs.SetInt(sceneName + "_score", score - 5);

        yield return new WaitForSeconds(5);
    }

    //Get hit direction
    private HitDirection ReturnDirection(GameObject Object, GameObject ObjectHit)
    {

        HitDirection hitDirection = HitDirection.None;
        RaycastHit MyRayHit;
        Vector3 direction = (Object.transform.position - ObjectHit.transform.position).normalized;
        Ray MyRay = new Ray(ObjectHit.transform.position, direction);

        if (Physics.Raycast(MyRay, out MyRayHit))
        {
            if (MyRayHit.collider != null)
            {
                Vector3 MyNormal = MyRayHit.normal;
                MyNormal = MyRayHit.transform.TransformDirection(MyNormal);

                if (MyNormal.x > 0) { hitDirection = HitDirection.Right; }
                if (MyNormal.x < 0) { hitDirection = HitDirection.Left; }
            }
        }
        return hitDirection;
    }
}
