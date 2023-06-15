using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Input
{
    public static class InputUtils
    {
        public static Vector3 GetInputPosition()
        {
            Vector3 mousePosition;

            if (Application.isEditor)
            {
                mousePosition = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
                mousePosition.z = 0f;
            }
            else
            {
                mousePosition = Camera.main.ScreenToWorldPoint(UnityEngine.Input.GetTouch(0).position);
                mousePosition.z = 0f;
            }

            return mousePosition;
        }

        public static Vector3 GetInputPositionScreen()
        {
            Vector3 mousePosition;

            if (Application.isEditor)
            {
                mousePosition = UnityEngine.Input.mousePosition;
                mousePosition.z = 0f;
            }
            else
            {
                mousePosition = UnityEngine.Input.GetTouch(0).position;
                mousePosition.z = 0f;
            }

            return mousePosition;
        }
    }
}
