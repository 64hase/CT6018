using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskListElement : MonoBehaviour
{

    [SerializeField] private Text dateText;
    [SerializeField] private Text descriptionText;
    [SerializeField] private Text priorityText;

    public void Init(string a_dateText, string a_descriptionText, string a_priorityText, int starCount)
    {
        //Sets the text objects in the tasklist
        dateText.text = a_dateText;
        descriptionText.text = a_descriptionText;
        priorityText.text = a_priorityText;
    }
}
