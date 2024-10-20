using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private enum Mode
    {
        LookAt,
        LookAtInverted,
        CameraForward,
        CameraForwardInverted
    }

    [SerializeField] private Mode mode;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject parent;

    private Vector3 initalPosition;
    private Vector3 inialDir;

    private void Awake()
    {
        initalPosition = transform.position;
        inialDir = parent.transform.position - initalPosition;

        if (mainCamera == null )
        {
            mainCamera = Camera.main.gameObject;
        }
    }

    private void LateUpdate()
    {
        /*
        switch (mode)
        {
            case Mode.LookAt:
                transform.LookAt(mainCamera.transform);
                break;
            case Mode.LookAtInverted:
                Vector3 dirFromCamera = transform.position - mainCamera.transform.position;
                transform.LookAt(mainCamera.transform.position + dirFromCamera);
                break;
            case Mode.CameraForward:
                transform.forward = mainCamera.transform.forward;
                break;
            case Mode.CameraForwardInverted:
                transform.forward = -mainCamera.transform.forward;
                break;
        }
        */

        transform.position = parent.transform.position - inialDir;

        transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
    }
}
