using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSprite : MonoBehaviour
{
    public float yaw;
    public float pitch;

    // Update is called once per frame
    void Update()
    {
        // Get the yaw and pitch of Camera
        float cameraYaw = Camera.main.GetComponent<CameraController>().yaw;
        float cameraPitch = Camera.main.GetComponent<CameraController>().pitch;
        // set yaw and pitch of sprite to be negative of the camera's
        yaw = -cameraYaw;
        pitch = -cameraPitch;

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        transform.rotation = rotation;
    }
}
