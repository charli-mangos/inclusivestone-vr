using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Camera Camera2Follow;
    public float distance = 3.0F;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;
    private Transform target;
    public bool hasMargin;
    private float constXRight = 0f;
    public float distanceMargin = 0f;

    void Awake()
    {
        target = Camera2Follow.transform;

        if (hasMargin)
        {
            constXRight = distanceMargin;
        }
    }


    void Update()
    {
        // Define my target position in front of the camera ->
        Vector3 targetPosition = target.TransformPoint(new Vector3(constXRight, 0, distance));
        var dir = Camera2Follow.transform.position - transform.position;

        Quaternion LookAtRotation = Quaternion.LookRotation(-dir);

        // version 1: my object's rotation is always facing to camera with no dampening  ->
        //transform.LookAt(transform.position + Camera2Follow.transform.rotation * Vector3.forward, Camera2Follow.transform.rotation * Vector3.up);

        // version 2 : my object's rotation isn't finished synchronously with the position smooth.damp ->
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, LookAtRotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        // Smoothly move my object towards that position ->
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(targetPosition.x, transform.position.y, targetPosition.z), ref velocity, smoothTime);
    }
}
