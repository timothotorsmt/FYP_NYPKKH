using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Extention;

public class StomaPatient
{
    public ReactiveProp<float> StomaBagAirValue;
    public float StomaBagAirFillDelay;
    

    public StomaPatient()
    {
        StomaBagAirValue = new ReactiveProp<float>();

        // Generate a randopm number between 0 and 0.5f;
        // 0.5 is the minimum to deflate the bag
        StomaBagAirValue.SetValue(Random.Range(0.0f, 0.6f));
    }

    public void InteractWithPatient()
    {
        // Fail cases if the player clicks on the 
        if (StomaBagAirValue.GetValue() > 0.5f)
        {
            if (StomaBagAirValue.GetValue() > 0.8f)
            {
                // Patient will complain a lot
            }
            else
            {
                // Alice will make a judgement on how the bag is full enough to deflate
            }
        }
        else 
        {
            // Alice will make a comment on how the bag is not ready to deflate
        }
    } 

    public void AddStomaBagValue()
    {
        // Only fill up during deflate bag time otherwise is a bit unfair
        if (DeflationTaskController.Instance.GetCurrentTask() == DeflationTasks.DEFLATE_BAGS)
        {
            // Fill up 
            StomaBagAirValue.SetValue(StomaBagAirValue.GetValue() + Random.Range(0.00f, 0.05f));
        }
        // Get new stoma bag delat
        StomaBagAirFillDelay = Random.Range(2.5f, 5.0f);
    }
}
