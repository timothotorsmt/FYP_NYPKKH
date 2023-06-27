using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CheckList : MonoBehaviour
{
    public GameObject tickIcon;

    private bool isCompleted;

    public void OnDrop(PointerEventData eventData)
    {
        // Check if the dropped item is the correct one
        if (eventData.pointerDrag == gameObject)
        {
            CompleteItem();
        }
    }

    private void CompleteItem()
    {
        isCompleted = true;
        tickIcon.SetActive(true);

        // You can add additional actions here when the item is completed
    }
}
