using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LocomotionController : MonoBehaviour
{
    public XRController leftRay;
    public XRController rightRay;
    public InputHelpers.Button teleportActivation;
    public float activationThreshold = 0.1f;
    public XRRayInteractor leftInteractor;
    public XRRayInteractor rightInteractor;

    private bool movement;
    private GameObject[] moveArea;

    public bool EnableRightTeleport { get; set; } = true;
    public bool EnableLeftTeleport { get; set; } = true;

    private void Update()
    {
        CheckIfMovement();

        if (leftRay)
        {
            bool isLeftHovering = leftInteractor.TryGetHitInfo(out Vector3 position, out Vector3 normal, out int index, out bool validTarget);
            leftRay.gameObject.SetActive(EnableLeftTeleport && CheckIfActivated(leftRay) && !isLeftHovering);
        }

        if (rightRay)
        {
            bool isRightHovering = rightInteractor.TryGetHitInfo(out Vector3 position, out Vector3 normal, out int index, out bool validTarget);
            rightRay.gameObject.SetActive(EnableRightTeleport && CheckIfActivated(rightRay) && !isRightHovering);
        }
    }

    private bool CheckIfActivated(XRController controller)
    {
        if (movement)
        {
            InputHelpers.IsPressed(controller.inputDevice, teleportActivation, out bool isActive, activationThreshold);
            return isActive;
        }
        else
        {
            return false;
        }
    }

    private void CheckIfMovement()
    {
        moveArea = GameObject.FindGameObjectsWithTag("MoveArea");
        movement = moveArea.Length > 0;
    }
}