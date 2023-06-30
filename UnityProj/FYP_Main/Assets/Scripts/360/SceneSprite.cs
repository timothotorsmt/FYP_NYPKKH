using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Input;

public class SceneSprite : MonoBehaviour, IInputActions
{
    public float yaw;
    public float pitch;

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
        // Get the yaw and pitch of Camera
        float cameraYaw = Camera.main.GetComponent<CameraController>().yaw;
        float cameraPitch = Camera.main.GetComponent<CameraController>().pitch;
        // Set Yaw and Pitch of sprite to be negative of the camera's
        yaw = -cameraYaw;
        pitch = -cameraPitch;
        // Input the values as rotation values
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        transform.rotation = rotation;
    }

    public void OnEndTap()
    {

    }
}
