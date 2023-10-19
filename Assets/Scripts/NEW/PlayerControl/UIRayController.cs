using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class UIRayController : MonoBehaviour
{
    public XRController leftRay;
    public XRController rightRay;
    //public InputHelpers.Button teleportActivation;
    //public float activationThreshold = 0.1f;
    public XRRayInteractor leftInteractor;
    public XRRayInteractor rightInteractor;

    public bool isMachine = false;
    //private GameObject[] moveArea;

    public bool EnableRightRay { get; set; } = true;
    public bool EnableLeftRay { get; set; } = true;

    private void Update()
    {
        //isMachineUse();

        if (leftRay)
        {
            bool isLeftHovering = leftInteractor.TryGetHitInfo(out Vector3 position, out Vector3 normal, out int index, out bool validTarget);
            leftRay.gameObject.SetActive(EnableLeftRay && CheckIfActivated(leftRay) /*&& !isLeftHovering*/);
        }

        if (rightRay)
        {
            bool isRightHovering = rightInteractor.TryGetHitInfo(out Vector3 position, out Vector3 normal, out int index, out bool validTarget);
            rightRay.gameObject.SetActive(EnableRightRay && CheckIfActivated(rightRay) /*&& !isRightHovering*/);
        }
    }

    private bool CheckIfActivated(XRController controller)
    {
        if (!isMachine)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /*public void isMachineUse(bool control)
    {
        isMachine = control;
    }*/
}
