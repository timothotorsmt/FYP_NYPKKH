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
        // Gets camera position and transforms the object to follow the UI
        Vector3 cameraPos = Camera.main.transform.position;
        transform.LookAt(cameraPos);
    }

    public void OnEndTap()
    {

    }
}
