using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSeeker : MonoBehaviour
{
    public static CameraSeeker instance;

    private void Awake()
    {
        instance = this;
    }

    public Vector3 GetCameraPosition { get { return transform.position; } }
}
