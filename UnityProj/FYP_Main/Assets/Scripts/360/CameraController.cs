using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField, Range(0, 50)]
    public float cameraSensitivity = 50;
    public float yaw;
    public float pitch;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                // Movement of the player
                Vector2 touch = Input.GetTouch(0).deltaPosition;
                yaw += -touch.x * cameraSensitivity * Time.deltaTime;
                pitch += touch.y * cameraSensitivity * Time.deltaTime;
                pitch = Mathf.Clamp(pitch, -90f, 90f);
                Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
                transform.rotation = rotation;
            }
        }
    }

}
