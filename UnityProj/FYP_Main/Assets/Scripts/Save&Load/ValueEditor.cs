using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ValueEditor : MonoBehaviour
{
    // Define the variables
    private int currentLivesValue;
    private int currentHighscoreValue;
    private int currentHP;
    private int currentATK;
    public TMP_Text TextComponent;

    // Define the Savers used
    [SerializeField]
    private SampleDataSaver sampleDataSaver;
    [SerializeField]
    private SampleData2Saver sampleData2Saver;

    private void Start()
    {
        // Initialise the variables
        currentLivesValue = 0;
        currentHighscoreValue = 0;

        currentHP = 0;
        currentATK = 0;
    }

    private void Update()
    {
        // Set current shown data as saved data
        currentLivesValue = SampleDataSaver.sampleData.Lives;
        currentHighscoreValue = SampleDataSaver.sampleData.HighScore;

        currentHP = SampleData2Saver.sampleData2.HP;
        currentATK = SampleData2Saver.sampleData2.ATK;
        // Display the data
        //TextComponent.text = "Lives: " + currentLivesValue + "    " + "Highscore: " + currentHighscoreValue;
        TextComponent.text = "HP: " + currentHP + "    " + "ATK: " + currentATK;
    }
    public void AddValue()
    {
        currentATK += 1;
        SampleData2Saver.sampleData2.ATK = currentATK;
    }

    public void SubtractValue()
    {
        currentATK -= 1;
        SampleData2Saver.sampleData2.ATK = currentATK;
    }
}
