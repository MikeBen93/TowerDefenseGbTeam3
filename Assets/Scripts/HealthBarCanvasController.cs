using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarCanvasController : MonoBehaviour
{
    public Transform healthBarCanvas;

    private CameraSeeker cameraSeeker;

    private void Start()
    {
        cameraSeeker = CameraSeeker.instance;
    }

    private void Update()
    {
        healthBarCanvas.transform.LookAt(cameraSeeker.GetCameraPosition);
    }
}
