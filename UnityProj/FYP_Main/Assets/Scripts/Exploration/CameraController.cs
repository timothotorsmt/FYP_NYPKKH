using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    [SerializeField] Transform _focus = default;
    [SerializeField] PlayerRoomInfo _playerRoom;
    [SerializeField, Min(0f)] float _focusRadius = 1f;
    [SerializeField, Range(0f, 1f)] float _focusCentering = 0.5f;

    Vector3 focusPoint;
    float halfViewport;


    // Start is called before the first frame update
    void Start()
    {
        focusPoint = _focus.position;
        halfViewport = (Camera.main.orthographicSize * Camera.main.aspect);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        UpdateFocusPoint();

        if (focusPoint.x - halfViewport >= _playerRoom._currentRoom.GetRoomMinEdge() && focusPoint.x + halfViewport <= _playerRoom._currentRoom.GetRoomMaxEdge())
        {
            transform.localPosition = new Vector3(focusPoint.x, _playerRoom._currentRoom.gameObject.transform.position.y, transform.localPosition.z);
        }
    }

    void UpdateFocusPoint()
    {
        Vector3 targetPoint = _focus.position;
        targetPoint.z = gameObject.transform.position.z;
        if (_focusRadius > 0f)
        {
            float distance = Vector3.Distance(targetPoint, focusPoint);
            float t = 1f;
            if (distance > 0.01f && _focusCentering > 0f)
            {
                t = Mathf.Pow(1f - _focusCentering, Time.deltaTime);
            }
            if (distance > _focusRadius)
            {
                t = Mathf.Min(t, _focusRadius / distance);
            }

            focusPoint = Vector3.Lerp(targetPoint, focusPoint, t);
        }
        else
        {
            focusPoint = targetPoint;
        }
    }

    public void SetToNewRoom()
    {
        transform.localPosition = new Vector3(_playerRoom._currentRoom.gameObject.transform.position.x, _playerRoom._currentRoom.gameObject.transform.position.y, transform.localPosition.z);
    }
}
