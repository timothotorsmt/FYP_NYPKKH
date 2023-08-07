using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PatientManagement;
using UniRx.Extention;
using UniRx;
using System.Linq;

public class AgeGroupChangeableObject : MonoBehaviour
{
    [SerializeField] private List<changableObject<AgeGroup>> _changableObjects;
    private ReactiveProp<AgeGroup> ageGroup = new ReactiveProp<AgeGroup>();

    // Start is called before the first frame update
    void Start()
    {
        // Add a listener to the subscription 
        ageGroup.Value.Subscribe(State => {
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

    public void ChangeAgeGroup(AgeGroup newAgeGroup)
    {
        ageGroup.SetValue(newAgeGroup);
    }
}
