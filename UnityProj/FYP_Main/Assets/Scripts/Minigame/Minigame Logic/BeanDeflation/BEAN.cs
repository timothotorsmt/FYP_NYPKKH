using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BEAN : MonoBehaviour
{
    // Start is called before the first frame update
    Bag selected;
    public DeflationGameLogic d;
    bool onStandBy=false;
    float timer;
    void Start()
    {
        timer = Random.Range(3, 5);
        StartCoroutine("ChooseBag");
        Quaternion a = Quaternion.Euler(0, 0, -13.46f);

        Vector3 Rotation = new Vector3(a.eulerAngles.x, a.eulerAngles.y, a.eulerAngles.z);

        transform.DORotate(Rotation, 1f, RotateMode.Fast).SetLoops(-1, LoopType.Yoyo);
        timer = Random.Range(2, 5);
        d = FindObjectOfType<DeflationGameLogic>();
  
    }

    void ChoosesABag()
    {
        if (d.getGamerunning())
        {
            Bag[] b = FindObjectsOfType<Bag>();
            float BreakOutOfLoop = 0;
            if (b.Length > 0)
            {
                bool ok = false;
                while (!ok && BreakOutOfLoop <4)
                {
                    selected = b[Random.Range(0, b.Length)];


                    Debug.Log(selected.idle);
                    if (selected.idle)
                    {
                        if (selected.bean == null)
                        {
                            ok = true;
                            selected.idle = false;
                        }
                    }

                        BreakOutOfLoop++;
                }
                if(ok)
                {
                    Invoke("GoToThere", 3);
                }
                else
                {
                    MoveAway();
                }
                //set timer for it to go to the bag location
           
            }
        }
        else
        {
            if (onStandBy == false)
            {
                Debug.Log(d.getGamerunning());
                onStandBy = true;
                StartCoroutine("redo");
            }
        }
    }

    IEnumerator redo()
    {
        yield return new WaitForSeconds(5);
        ChoosesABag();
        onStandBy = false;
    }

    void GoToThere()
    {
        if (selected == null)
        {
            MoveAway();
            return;
        }
 
        transform.position = selected.transform.position + new Vector3(1,-1,0) ;

  

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
       // Debug.Log("move");
        StopCoroutine("KILL");
        StartCoroutine("ChooseBag");
    }
    IEnumerator ChooseBag()
    {
        yield return new WaitForSeconds(timer);
        timer = Random.Range(3, 5);
        ChoosesABag();

    }


}
