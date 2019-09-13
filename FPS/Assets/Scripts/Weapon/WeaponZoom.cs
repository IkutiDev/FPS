using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private RigidbodyFirstPersonController FpsController;

    [Header("FOV Values")]
    [SerializeField] private float zoomedInFOV=30f;
    [SerializeField] private float zoomedOutFOV=60f;

    [Header("Sensitivity values")] 
    [SerializeField] private float sensitivityZoomedIn = 1f;
    [SerializeField] private float sensitivityZoomedOut = 2f;
    private bool zoomedInToggle;
    private void OnDisable()
    {
        ZoomOut();
    }
    public void Zoom()
    {
        if (zoomedInToggle)
        {
            ZoomOut();
        }
        else
        {
            ZoomIn();
        }
    }

    private void ZoomOut()
    {
        zoomedInToggle = false;
        FpsController.mouseLook.XSensitivity = sensitivityZoomedOut;
        FpsController.mouseLook.YSensitivity = sensitivityZoomedOut;
        playerCamera.fieldOfView = zoomedOutFOV;
    }

    private void ZoomIn()
    {
        zoomedInToggle = true;
        FpsController.mouseLook.XSensitivity = sensitivityZoomedIn;
        FpsController.mouseLook.YSensitivity = sensitivityZoomedIn;
        playerCamera.fieldOfView = zoomedInFOV;
    }
}
