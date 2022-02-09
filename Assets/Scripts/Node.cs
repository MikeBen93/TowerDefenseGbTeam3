using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Color touchColor;
    public Vector3 positionOffset; // offset of the tower relative to node position

    private Color defaultColor;
    private Renderer rend;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        defaultColor = rend.material.color;
        //buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    public void ChangeColorOnTouch()
    {
        if(rend.material.color == defaultColor)
        {
            rend.material.color = touchColor;
        } else
        {
            rend.material.color = defaultColor;
        }
    }
}
