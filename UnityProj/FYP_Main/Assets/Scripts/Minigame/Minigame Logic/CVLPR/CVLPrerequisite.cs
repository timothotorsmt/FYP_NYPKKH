using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Input;
using UnityEngine.EventSystems;

public class CVLPrerequisite : MonoBehaviour, IInputActions
{
    public GameObject Destination;
    private BoxCollider2D _boxCollider2D;
    private bool _isMoving;
    private bool _isFinished;
    public string itemname;

    private Vector2 _startPosition;
    private Vector3 _resetPosition;
    public bool correct;
    public int amt;
    bool Okay = true;

    // Start is called before the first frame update
    void Start()
    {
        _resetPosition = this.transform.localPosition;
        _boxCollider2D = GetComponent<BoxCollider2D>();
        Reset();
        itemname = gameObject.name;
    }

    void Reset()
    {
        // Make sure all the shadows r active
        Destination.SetActive(true);
        UIInputManager.Instance.AddSubscriber(this);

        _isFinished = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDisable()
    {
        if (UIInputManager.Instance != null)
        {
            UIInputManager.Instance.RemoveSubscriber(this);
        }
    }

    public void OnStartTap()
    {
        // dc if its finished
        if (_isFinished) { return; }

        Vector2 mousePos = InputUtils.GetInputPosition();

        //Debug.Log(Vector2.Distance((Vector2)this.transform.position, mousePos));
        if (Vector2.Distance((Vector2)this.transform.position, mousePos) < 1.0f)
        {
            _startPosition = (Vector2)this.transform.position;
            _isMoving = true;
        }
    }

    public void OnTap()
    {   
        if (_isMoving)
        {
            Vector2 mousePos = InputUtils.GetInputPosition();

            this.transform.position = mousePos;
        }
    }

    public void OnEndTap()
    {
        if (_isMoving == true)
        {
            _isMoving = false;
            // Debug.Log(gameObject.name);

            if (Vector2.Distance((Vector2)this.transform.position, (Vector2)Destination.transform.position) < 1.0f)
            {
                this.transform.position = new Vector3(Destination.transform.position.x, Destination.transform.position.y, Destination.transform.position.z);
               // Destination.SetActive(false);
                //_isFinished = true;
            
                if(!correct)
                {
                    CheckList.Instance.wrong();
                    CVLPRTaskController.Instance.MarkWrongTask();
                    Okay = false;
                 //   return;
                }
              
                CheckList.Instance.checking(itemname);

                if (CheckList.Instance.amtInsided(itemname)>amt)
                {
                    Okay = false;
                    CheckList.Instance.wrong2();
                    CVLPRTaskController.Instance.MarkWrongTask();

                    CheckList.Instance.removeInside(itemname);
                  //  return;
                }

                if (Okay)
                {
                    GameObject b = Instantiate(gameObject, transform.position, Quaternion.identity) as GameObject;
                    CVLPRTaskController.Instance.MarkCorrectTask(false);
                    b.transform.SetParent(transform.parent);
                    b.transform.position = transform.position;
                    b.transform.localScale = transform.localScale*0.7f;
                    b.GetComponent<CVLPrerequisite>().enabled = false;

                }

            }
                this.transform.localPosition = new Vector3(_resetPosition.x, _resetPosition.y, _resetPosition.z);
        }
    }
}
