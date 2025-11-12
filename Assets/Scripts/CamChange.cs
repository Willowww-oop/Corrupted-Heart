using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class CamChange : MonoBehaviour
{

    public string triggerTag;

    public CinemachineCamera primaryCam;

    public CinemachineCamera[] cameras;

    void Start()
    {
        SwitchToCamera(primaryCam);
    }

    // Check if the trigger is theere; switch to targeted camera

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == triggerTag)
        {
            CinemachineCamera targetCamera = other.GetComponentInChildren<CinemachineCamera>();

            SwitchToCamera(targetCamera);
        }
    }

    // Switch back to original cam

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(triggerTag))
        {
            SwitchToCamera(primaryCam);
        }
    }

    // Checking all cameras, excluding original

    void SwitchToCamera(CinemachineCamera targetCamera)
    {
        foreach (CinemachineCamera cam in cameras)
        {
            cam.enabled = cam == targetCamera;
        }
    }

    [ContextMenu("Get All Cameras")]
    private void GetAllCameras()
    {
        cameras = GameObject.FindObjectsByType<CinemachineCamera>(FindObjectsSortMode.InstanceID);
    }
}
