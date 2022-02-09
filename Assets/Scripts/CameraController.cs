using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private string _nodeTag = "Node";
    private void Update()
    {
        if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            RaycastToNode(Input.touches[0].position);
            return;
        }

#if UNITY_EDITOR
        if(Input.GetMouseButtonDown(0))
        {
            RaycastToNode(Input.mousePosition);
            return;
        }
#endif
    }


    private void RaycastToNode(Vector2 touchPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null && hit.collider.CompareTag(_nodeTag))
            {
               
                hit.collider.GetComponent<Node>().ChangeColorOnTouch();
            }
        }
    }
}
