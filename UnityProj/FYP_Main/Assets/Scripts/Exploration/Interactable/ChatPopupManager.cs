using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ChatPopupManager : MonoBehaviour
{
    // This class manages the popping up of the chat// interactable icons
    [SerializeField] private List<GameObject> _interactablePopups;
    [SerializeField, Range(0, 50)] private uint _minimumDistance;
    [SerializeField] private GameObject _player;

    // Change to UniRX
    private void Update()
    {
        foreach (GameObject item in _interactablePopups)
        {
            if (item.activeInHierarchy)
            {
                if (Mathf.Abs(item.transform.position.x - _player.transform.position.x) > _minimumDistance) 
                {
                    item.SetActive(false);
                }
            }
            else 
            {
                if (Mathf.Abs(item.transform.position.x - _player.transform.position.x) < _minimumDistance) 
                {
                    item.SetActive(true);
                }
            }
        }
    }
}
