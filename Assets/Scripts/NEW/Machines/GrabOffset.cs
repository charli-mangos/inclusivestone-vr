using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabOffset : XRGrabInteractable
{
    //Init pos and rot
    private Vector3 initialLocalPos;
    private Quaternion initialLocalRot;

    //Is grabbing boolean
    public bool isGrabbing = false;

    // Start is called before the first frame update
    void Start()
    {
        if (!attachTransform)
        {
            GameObject grab = new GameObject("Grab Pivot");
            grab.transform.SetParent(transform, false);
            attachTransform = grab.transform;
        }

        initialLocalPos = attachTransform.localPosition;
        initialLocalRot = attachTransform.localRotation;

    }

    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {

        if (interactor is XRDirectInteractor)
        {
            attachTransform.position = interactor.transform.position;
            attachTransform.rotation = interactor.transform.rotation;

            isGrabbing = true;
        }

        base.OnSelectEntered(interactor);
    }

    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        attachTransform.localPosition = initialLocalPos;
        attachTransform.localRotation = initialLocalRot;

        isGrabbing = false;
    }
     
}