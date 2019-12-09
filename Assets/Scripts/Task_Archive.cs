using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Task_Archive : MonoBehaviour
{
    private int LowPriorityTaskCount
    {
        get { return PlayerPrefs.GetInt("LowPriorityTaskCount", 0); }
        set { PlayerPrefs.SetInt("LowPriorityTaskCount", value); }
    }
    private int NormalPriorityTaskCount
    {
        get { return PlayerPrefs.GetInt("NormalPriorityTaskCount", 0); }
        set { PlayerPrefs.SetInt("NormalPriorityTaskCount", value); }
    }
    private int HighPriorityTaskCount
    {
        get { return PlayerPrefs.GetInt("HighPriorityTaskCount", 0); }
        set { PlayerPrefs.SetInt("HighPriorityTaskCount", value); }
    }
    private string[] ArchiveTask;
    private List<string> CombinedArchive = new List<string>();
    private string TaskFormatting;
    [SerializeField] private Text ArchiveText;
    [SerializeField] private Text DescriptionColumn;
    [SerializeField] private Text PriorityColumn;
    [SerializeField] private Text CreationDateColumn;
    [SerializeField] private Text StarCountColumn;
    [SerializeField] private GameObject[] PieCharts;
    private int TaskArchiveSize;
    private void Start()
    {
        OnArchiveUpdate();
        OnSetPieChartData();
    }
    private void OnEnabled()
    {
        OnArchiveUpdate();
        OnSetPieChartData();
    }
    public void OnSetPieChartData()
    {
        /*LowPriorityTaskCount = PlayerPrefs.GetInt("LowPriorityTaskCount", 0);
        NormalPriorityTaskCount = PlayerPrefs.GetInt("NormalPriorityTaskCount", 0);
        HighPriorityTaskCount = PlayerPrefs.GetInt("HighPriorityTaskCount", 0);*/
        for (int i = 0; i < PieCharts.Length; i++)
        {
            PieCharts[i].GetComponent<PieChart>().OnPieChartSet(LowPriorityTaskCount, "Low Priority Tasks", NormalPriorityTaskCount, "Normal Priority Tasks", HighPriorityTaskCount, "High Priority Tasks");
        }
    }
    private void OnArchiveUpdate()
    {
        DescriptionColumn.text = "Task description: \n";
        PriorityColumn.text = "Priority: \n";
        StarCountColumn.text = "Star count: \n";
        CreationDateColumn.text = "Created: \n";
        for (int i = 0; i < TaskArchiveSize; i++)
        {
            ArchiveTask = PlayerPrefs.GetString("ArchivedTask_" + i).Split(',');
            DescriptionColumn.text += ArchiveTask[0] + "\n";
            PriorityColumn.text += ArchiveTask[2] + "\n";
            StarCountColumn.text += ArchiveTask[3] + " Stars" + "\n";
            CreationDateColumn.text += ArchiveTask[1] + "\n";

        }
    }
}
