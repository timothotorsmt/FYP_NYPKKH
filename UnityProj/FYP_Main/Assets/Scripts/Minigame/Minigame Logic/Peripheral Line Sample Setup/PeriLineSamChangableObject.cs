using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;

public class PeriLineSamChangableObject : MonoBehaviour
{
    // List out all needed sprites
    [SerializeField] private List<changableObject<PeriLineSamTasks>> _changableObjects;    

    // Start()
    public void Start() 
    {
        // Subscribe to the current task state
        PeriLineSamTaskController.Instance.CurrentTask.Value.Subscribe(State => {
            if (_changableObjects.Where(s => s.TaskOnChange == State).Select(s => s.GameObjectToChange).Count() > 0) 
            {
                foreach (var item in _changableObjects)
                {
                    item.GameObjectToChange.SetActive(false);
                }

                _changableObjects.Where(s => s.TaskOnChange == State).Select(s => s.GameObjectToChange).First().SetActive(true);
            }
        });
    }
}



