using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Input;

namespace SkyboxCamera
{
    public class CameraController : MonoBehaviour, IInputActions
    {
        [SerializeField, Range(0, 50)] private float _cameraSensitivity = 50;
        [SerializeField] private float _minClamp = -90f;
        [SerializeField] private float _maxClamp = 90f;
        [SerializeField] private bool _disableYAxis = false;

        private float _yaw;
        private float _pitch;
        private Vector2 _touch;

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
                _touch = Input.GetTouch(0).deltaPosition;
                _yaw += -_touch.x * _cameraSensitivity * Time.deltaTime; // Yaw calculates the left-right movement 
                if (!_disableYAxis)
                {
                    _pitch += _touch.y * _cameraSensitivity * Time.deltaTime; // Pitch calculates the up-down movement
                    _pitch = Mathf.Clamp(_pitch, _minClamp, _maxClamp);
                }

                // Input the values as rotation values
                Quaternion rotation = Quaternion.Euler(_pitch, _yaw, 0);
                // Lineearly interpolate between these 2 quaternions
                transform.rotation = Quaternion.Lerp(rotation, transform.rotation, 0.5f);
            }

            // Add in mouse support
        }

        public void OnEndTap()
        {

        }

    }
}
