using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour
{
    [SerializeField] private string _nodeTag = "Node";

    private BuildManager _buildManager;
    private NodeUI _nodeUI;
    private Ray _rayFromCamera;
    private RaycastHit _hitFromCameraRay;


    private void Start()
    {
        _buildManager = BuildManager.instance;
    }

    private void Update()
    {
        if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                return;
            }
            CheckRaycast(Input.touches[0].position);
            return;
        }

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            CheckRaycast(Input.mousePosition);
            return;
        }
#endif
    }

    private void CheckRaycast(Vector2 touchPosition)
    {
        _rayFromCamera = Camera.main.ScreenPointToRay(touchPosition);

        if (!Physics.Raycast(_rayFromCamera, out _hitFromCameraRay))
        {
            return;
        }

        //if(hitFromCameraRay.collider == null)
        //{
        //    return;
        //}

        if(_hitFromCameraRay.collider.CompareTag(_nodeTag))
        {
            Node node = _hitFromCameraRay.collider.GetComponent<Node>();

            _buildManager.SelectNode(node);

            return;
        }

        _buildManager.DeselectNode();
    }
}
