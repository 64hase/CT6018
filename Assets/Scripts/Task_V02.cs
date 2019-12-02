using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Task_V02 : MonoBehaviour
{
    [SerializeField] private string[] TaskPlayerPrefExport;
    [SerializeField] private Text TaskDescription;
    //[SerializeField] private Text DateTimeCreation;
    [SerializeField] private Text DueDate;
    [SerializeField] private Text Priority;
    [SerializeField] private Image PriorityPanel; 
    [SerializeField] private Text StarCount;
    [SerializeField] private Color PriorityHighColour;
    [SerializeField] private Color PriorityMediumColour;
    [SerializeField] private Color PriorityLowColour;

    private void start()
    {
        OnUpdateFromObjectName();
    }
    public void OnUpdateFromObjectName()
    {
        string[] TaskInfoSplit = PlayerPrefs.GetString(this.gameObject.name).Split(',');
        TaskDescription.text = TaskInfoSplit[0];
        Priority.text = TaskInfoSplit[2];
        StarCount.text = TaskInfoSplit[3];
        DueDate.text = TaskInfoSplit[4];
    }
    public void OnTaskUpdate(string TaskDescriptionUpdate, string DateTimeUpdate, string PriorityUpdate, int StarCountUpdate, string DueDateUpdate)
    {
        // TaskPlayerPrefExport = TaskDescriptionText.text + "," + DateTime.Now + "," + Priority + "," + StarCount.ToString() + "," + DueDate;
        TaskDescription.text = TaskDescriptionUpdate;
        //DateTimeCreation.text = DateTimeUpdate;
        Priority.text = PriorityUpdate;
        StarCount.text = StarCountUpdate.ToString();
        DueDate.text = DueDateUpdate;

        if (Priority.text == "High")
        {
            PriorityPanel.color = PriorityHighColour;
        }
        if (Priority.text == "Medium")
        {
            PriorityPanel.color = PriorityMediumColour;
        }
        if (Priority.text == "Low")
        {
            PriorityPanel.color = PriorityLowColour;
        }
    }
}
