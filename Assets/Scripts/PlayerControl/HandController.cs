using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;


public class HandController : MonoBehaviour
{
    private InputDevice targetDevice;

    public bool showController = false;
    public List<GameObject> controllers;
    private GameObject spawnedController;

    public InputDeviceCharacteristics controllerChar;

    public GameObject handModel;
    private GameObject spawnedHandModel;

    private Animator handAnimator;

    public string isRight;

    public float trigVal;
    public bool primaryButton;
    public float gripValue;
    public Vector2 axisVal;

    void Start()
    {
        TryInitialize();
    }

    void TryInitialize()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerChar, devices);

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
            GameObject prefab = controllers.Find(con => con.name == targetDevice.name);

            if (prefab)
            {
                spawnedController = Instantiate(prefab, transform);
            }
            else
            {
                Debug.LogError("Did not find corresponding controller model");
                spawnedController = Instantiate(controllers[0], transform);
            }

            spawnedHandModel = Instantiate(handModel, transform);
            handAnimator = spawnedHandModel.GetComponent<Animator>();
        }
    }

    void UpdateEvents()
    {
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerVal))
        {
            trigVal = triggerVal;
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primary))
        {
            primaryButton = primary;
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripVal))
        {
            gripValue = gripVal;
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 axis))
        {
            axisVal = axis;
        }
    }

    void UpdateAnimations()
    {
        handAnimator.SetFloat("Trigger", trigVal > 0.01f ? trigVal : 0f);
        handAnimator.SetFloat("Grip", gripValue > 0.01f ? gripValue : 0f);
    }

    void Update()
    {
        if (!targetDevice.isValid)
        {
            TryInitialize();
        }
        else
        {
            UpdateEvents();

            if (showController)
            {
                spawnedHandModel.SetActive(false);
                spawnedController.SetActive(true);
            }
            else
            {
                spawnedHandModel.SetActive(true);
                spawnedController.SetActive(false);
                UpdateAnimations();
            }
        }
    }
}