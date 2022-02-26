using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarCanvasController : MonoBehaviour
{
    public Transform healthBarCanvas;
    public Transform cameraPosition;

    void Update()
    {
        healthBarCanvas.transform.LookAt(cameraPosition);
    }
}
