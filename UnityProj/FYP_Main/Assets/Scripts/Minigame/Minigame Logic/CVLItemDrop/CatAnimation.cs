using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static UnityEditor.Recorder.OutputPath;

public class CatAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CatPaw();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CatPaw()
    {
       // Vector3 targetPosition = new Vector3(5f, 6f, -5f); // Set the target position for the wipe animation
        //float duration = 4f; // Set the duration of the animation
        Quaternion a = Quaternion.Euler(0,0, -519.041f);
        Vector3 Rotation = new Vector3(a.eulerAngles.x,a.eulerAngles.y,a.eulerAngles.z);

        //transform.DOMove(targetPosition, duration).SetLoops(-1, LoopType.Yoyo);
        transform.DORotate(Rotation, 2f, RotateMode.Fast).SetLoops(-1, LoopType.Yoyo);
    }
}
