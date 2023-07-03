using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Checklist : MonoBehaviour
{
    [SerializeField] string[] nameOfItem = { "IV bag", "spike", "IV tube", "Tube 2" };
    [SerializeField] bool[] inside = { true, true, true, true };
    public GameObject list;
    private GameObject[] ListOfItems;
    public TextMeshProUGUI itemName;
    [SerializeField] private UnityEvent _ChecklistEvent;

    private static Checklist _instance;

    public static Checklist Instance
    {
        get
        {
            if (_instance == null)
            
                Debug.LogError("Checklist is NULL!");
            return _instance;  
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        ListOfItems = GameObject.FindGameObjectsWithTag("items");
        string text;
        text = "";
        for (int i = 0; i < nameOfItem.Length; i++)
        {
            text += nameOfItem[i] + "\n";
        }
        itemName.text = text;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.C))
        {
            Debug.Log("asd");
        }
    }

    public void items()
    {
        Checklist.Instance.items();
    }
   
    public void checking (string a)
    {
        for (int i = 0; i < nameOfItem.Length; i++)
        {
            if (nameOfItem[i] == a)
            {
                inside[i] = true;
               // Debug.Log($"{a}:{inside[i]}s");
            }
        }
        check();
    }

    public void button()
    {
        for (int i = 0; i < ListOfItems.Length; i++)
        {
            ListOfItems[i].GetComponent<SpriteRenderer>().enabled = list.activeSelf;
        }
        //to activate the checklist
        list.SetActive(!list.activeSelf);
    }

    public void check()
    {
        string text;
        text = "";
        int correct = 0 ;
        for (int i = 0; i < nameOfItem.Length; i++)
        {
            if(inside[i] == true)
            {
                text += $"<s>{nameOfItem[i]}</s> \n";
                correct++;

            }
            else
            {
                text += nameOfItem[i] + "\n";
            }
        }
     

        if(correct==nameOfItem.Length && (CVLPRTaskController.Instance.GetCurrentTask() == CVLPRTasks.PRERQUISITES))
        {
            CVLPRTaskController.Instance.MarkCurrentTaskAsDone();
            Debug.Log(CVLPRTaskController.Instance.GetCurrentTask());
        }

        itemName.text = text;
    }

}
