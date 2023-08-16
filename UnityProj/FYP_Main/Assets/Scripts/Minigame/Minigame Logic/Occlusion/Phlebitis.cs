using PatientManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Phlebitis : MonoBehaviour
{
    [SerializeField] private Image _handImage;
    [SerializeField] private GameObject _assessSkinButton;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void SetPhlebitis()
    {
        _assessSkinButton.SetActive(true);
        _handImage.gameObject.GetComponent<PatientImage>()._bodyPart = BodyPart.ARM_PHLEBITIS;
    }


}
