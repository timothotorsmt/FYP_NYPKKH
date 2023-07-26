using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BEAN : MonoBehaviour
{
    // Start is called before the first frame update
    Bag selected;
    void Start()
    {
        ChoosesABag();
    }

    void ChoosesABag()
    {
        Bag[] b = FindObjectsOfType<Bag>();
        if (b.Length > 0)
        {
            selected = b[Random.Range(0, b.Length)];

            //set timer for it to go to the bag location
            Invoke("GoToThere", 3);
        }
    }

    void GoToThere()
    {
        transform.position = selected.transform.position + new Vector3(1,-1,0) ;

        Quaternion a = Quaternion.Euler(0, 0, -13.46f);

        Vector3 Rotation = new Vector3(a.eulerAngles.x, a.eulerAngles.y, a.eulerAngles.z);
        transform.DORotate(Rotation, 1f, RotateMode.Fast).SetLoops(-1, LoopType.Yoyo);

        StartCoroutine("PumpAir");
    }

    IEnumerator PumpAir()
    {
        //pump air in one frame 
        yield return new WaitForEndOfFrame();
        selected.SetAirBagValue(1,gameObject);
        StartCoroutine("KILL");
    }

    IEnumerator KILL()
    {
        //set timer for it to destroy the bag and dissapperar
        yield return new WaitForSeconds(5);
        selected.beGone();
        MoveAway();

    }

    public void MoveAway()
    {
        // move far away and call chooes a new bag
        transform.position = new Vector3(-1000, -54, 0);
        ChoosesABag();
        StopCoroutine("KILL");
    }


}
