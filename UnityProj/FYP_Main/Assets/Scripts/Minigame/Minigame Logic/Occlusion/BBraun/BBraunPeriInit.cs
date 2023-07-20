using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BBraunInfusomat;

public class BBraunPeriInit : MonoBehaviour
{
    private BBraunIPLogic _bBraunIPLogic;

    // Start is called before the first frame update
    void Start()
    {
        _bBraunIPLogic = GetComponent<BBraunIPLogic>();
        if (_bBraunIPLogic != null)
        {
            _bBraunIPLogic.InitialiseNormalBehaviour();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
