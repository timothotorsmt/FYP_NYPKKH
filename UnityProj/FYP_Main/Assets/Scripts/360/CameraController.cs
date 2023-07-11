using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Input;

public class CameraController : MonoBehaviour, IInputActions
{
    [SerializeField, Range(0, 50)]
    public float cameraSensitivity = 50;
    public float yaw;
    public float pitch;
    private float minClamp = -90f;
    private float maxClamp = 90f;
    private Vector2 touch;

    void OnEnable()
    {
        InputManager.Instance.AddSubscriber(this);
    }

    void OnDisable()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.RemoveSubscriber(this);
        }
    }

    public void OnStartTap()
    {

    }

    public void OnTap()
    {
        if (Input.touchCount > 0)
        {
            // This gets the player touch
            touch = Input.GetTouch(0).deltaPosition;
            yaw += -touch.x * cameraSensitivity * Time.deltaTime; // Yaw calculates the left-right movement 
            pitch += touch.y * cameraSensitivity * Time.deltaTime; // Pitch calculates the up-down movement
            // This limits the player's up down movement
            pitch = Mathf.Clamp(pitch, minClamp, maxClamp);
            // Input the values as rotation values
            Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
            transform.rotation = rotation;
        }
    }

    public void OnEndTap()
    {

    }

}
