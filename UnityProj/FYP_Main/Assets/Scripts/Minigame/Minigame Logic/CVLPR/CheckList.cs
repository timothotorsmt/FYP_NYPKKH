using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Core.SceneManagement;
using TMPro;

public class CheckList : MonoBehaviour
{
    [SerializeField] List<string> nameOfItem=new List<string>();
    [SerializeField] List<bool> inside= new List<bool>();
    [SerializeField] List<int> amtNeededInside = new List<int>();
    [SerializeField] List<int> amtAlreadyInside = new List<int>();
    public GameObject list;
    [SerializeField]
    private GameObject[] ListOfItems;
    public TextMeshProUGUI itemName;
    [SerializeField] private UnityEvent _ChecklistEvent,_TooMuch;
    CVLPrerequisite[] allitem;
    public GameObject backButton;

    private static CheckList _instance;

    public static CheckList Instance
    {
        get
        {
            if (_instance == null)
            
                Debug.LogError("Checklist is NULL!");
            return _instance;  
        }
    }

    public void back()
    {
        SceneLoader.Instance.ChangeScene(SceneID.HUB);
    }

    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine("SetUp");
    }

    IEnumerator SetUp()
    {
        yield return new WaitForSeconds(0.1f);
        string text;
        text = "";


        allitem = FindObjectsOfType<CVLPrerequisite>();

        foreach (CVLPrerequisite a in allitem)
        {
            if (a.correct == true)
            {
                Debug.Log("asdasdasd");
                nameOfItem.Add(a.itemname);
                inside.Add(false);
                amtAlreadyInside.Add(0);
                amtNeededInside.Add(a.amt);

            }
        }
        for (int i = 0; i < nameOfItem.Count; i++)
        {
            Debug.Log(nameOfItem[i]);
        }


        for (int i = 0; i < nameOfItem.Count; i++)
        {
            text += nameOfItem[i] + $" x{amtNeededInside[i]}" + "\n";
        }



        itemName.text = text;
        for (int i = 0; i < inside.Count; i++)
        {
            inside[i] = false;
        }
    }

    // Update is called once per frame

    public void items()
    {
        CheckList.Instance.items();
    }
   
    public void checking (string a)
    {

        for (int i = 0; i < nameOfItem.Count; i++)
        {
            if (nameOfItem[i] == a)
            {
             
                inside[i] = true;
                amtAlreadyInside[i]++;
                // Debug.Log($"{a}:{inside[i]}s");
            }
        }


      

        check();
    }

    public void button()
    {
  
        backButton.SetActive(list.activeSelf);
        //to activate the checklist
        list.SetActive(!list.activeSelf);
    }

    public void check()
    {
        string text;
        text = "";
        int correct = 0 ;
      

        
        for (int i = 0; i < nameOfItem.Count; i++)
        {
                
            if (amtNeededInside[i]==amtAlreadyInside[i])
            {
                text += $"<s>{nameOfItem[i]} x{amtNeededInside[i]}</s> \n";
                correct++;
                       
            }
            else
            {
                text += nameOfItem[i] + $" x{amtNeededInside[i]}" + "\n";
            }
                
        }
        

     
       
        if(correct==nameOfItem.Count && (CVLPRTaskController.Instance.GetCurrentTask() == CVLPRTasks.PRERQUISITES))
        {
           
            CVLPRTaskController.Instance.MarkCurrentTaskAsDone();
            Debug.Log(CVLPRTaskController.Instance.GetCurrentTask());
            
        }

        itemName.text = text;
    }

    public void wrong()
    {
        _ChecklistEvent.Invoke();
    }
    public void wrong2()
    {
        _TooMuch.Invoke();
    }

    public int amtInsided(string a)
    {
        for (int i = 0; i < nameOfItem.Count; i++)
        {
            if (nameOfItem[i] == a)
            {

                return amtAlreadyInside[i];

                // Debug.Log($"{a}:{inside[i]}s");
            }
        }
        return 0;
    }

    public void removeInside(string a)
    {
        for (int i = 0; i < nameOfItem.Count; i++)
        {
            if (nameOfItem[i] == a)
            {

                amtAlreadyInside[i] -= 1;
                check();
            }
        }
    }
}
