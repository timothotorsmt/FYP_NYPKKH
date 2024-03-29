using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Input;

namespace SkyboxCamera
{
    // The script to change skybox 
    public class SkyboxSprite : MonoBehaviour, IInputActions
    {

    #region input manager supporter functions
        void OnEnable()
        {
            if (InputManager.Instance != null)
            {
                InputManager.Instance.AddSubscriber(this);
            }
        }

        void OnDisable()
        {
            if (InputManager.Instance != null)
            {
                InputManager.Instance.RemoveSubscriber(this);
            }
        }
    #endregion

    #region Input manager functions
        public void OnStartTap()
        {

        }

        public void OnTap()
        {
            // Gets camera position and transforms the object to follow the UI
            Vector3 cameraPos = Camera.main.transform.position;

            // for UI elements
            if (this.gameObject.GetComponent<RectTransform>() != null)
            {

                // This barbie is using recttransform
                GetComponent<RectTransform>().LookAt(cameraPos);
            }
            else
            {
                transform.LookAt(cameraPos);

            }

        }

        public void OnEndTap()
        {

        }
    #endregion
    }
}
