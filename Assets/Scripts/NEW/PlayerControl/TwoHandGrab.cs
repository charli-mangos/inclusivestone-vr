using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TwoHandGrab : XRGrabInteractable
{
    public List<XRSimpleInteractable> secondGrab = new List<XRSimpleInteractable>();

    //public Transform[] snapPos;

    private XRBaseInteractor secondInteractor;

    private Quaternion initRotation;
    private Vector3 initPosition;

    public enum RotationType { None, First, Second };
    public RotationType twoHandRotationType;

    private bool snapToSecond = true;
    private Quaternion initRotOffset;

    public Quaternion rotEvent;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var items in secondGrab)
        {
            items.onSelectEntered.AddListener(OnSecondHandGrab);
            items.onSelectExited.AddListener(OnSecondHandRelease);
        }

        initPosition = this.transform.localPosition;
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        if (secondInteractor && selectingInteractor)
        {
            //Compute rotation
            if (snapToSecond == true)
            {
                rotEvent = getTwoHandRotation();
            }
            else
            {
                rotEvent = getTwoHandRotation() * initRotOffset;
            }

            selectingInteractor.attachTransform.rotation = rotEvent;
        }


        base.ProcessInteractable(updatePhase);
    }

    public Quaternion getTwoHandRotation()
    {
        Quaternion targetRot = Quaternion.LookRotation(secondInteractor.attachTransform.position - selectingInteractor.attachTransform.position);

        if (twoHandRotationType == RotationType.First)
        {
            targetRot = Quaternion.LookRotation(secondInteractor.attachTransform.position - selectingInteractor.attachTransform.position, selectingInteractor.attachTransform.up);
        }
        else if (twoHandRotationType == RotationType.Second)
        {
            targetRot = Quaternion.LookRotation(secondInteractor.attachTransform.position - selectingInteractor.attachTransform.position, secondInteractor.attachTransform.up);
        }

        return targetRot;
    }


    public void OnSecondHandGrab(XRBaseInteractor interactor)
    {
        secondInteractor = interactor;
        if (snapToSecond == false)
        {
            initRotOffset = Quaternion.Inverse(getTwoHandRotation()) * selectingInteractor.attachTransform.rotation;
        }
    }

    public void OnSecondHandRelease(XRBaseInteractor interactor)
    {
        secondInteractor = null;
    }

    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        base.OnSelectEntered(interactor);
        initRotation = interactor.attachTransform.localRotation;
    }

    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        base.OnSelectExited(interactor);
        secondInteractor = null;
        interactor.attachTransform.localRotation = initRotation;

    }

    public override bool IsSelectableBy(XRBaseInteractor interactor)
    {
        bool isalreadygrabbed = selectingInteractor && !interactor.Equals(selectingInteractor);
        return base.IsSelectableBy(interactor) && !isalreadygrabbed;
    }
}
