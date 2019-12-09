using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieChart : MonoBehaviour
{
    [SerializeField] private Image PieChartLayer01;
    [SerializeField] private Image PieChartSeg01KeyColour;
    [SerializeField] private Text PieChartseg01KeyText;
    [SerializeField] private Color PieChart01Colour;
    [SerializeField] private Image PieChartLayer02;
    [SerializeField] private Image PieChartSeg02KeyColour;
    [SerializeField] private Text PieChartseg02KeyText;
    [SerializeField] private Color PieChart02Colour;
    [SerializeField] private Image PieChartLayer03;
    [SerializeField] private Image PieChartSeg03KeyColour;
    [SerializeField] private Text PieChartseg03KeyText;
    [SerializeField] private Color PieChart03Colour;
    [SerializeField] private float PieChartLayer01Fill;
    [SerializeField] private float PieChartLayer02Fill;
    [SerializeField] private float PieChartLayer03Fill;
    [SerializeField] private float PieChartTotalValue;

    public void OnPieChartSet(float value1, string Value1Name, float value2, string Value2Name, float value3, string Value3Name)
    {
        //PieChartLayer01Fill = value1;
        //PieChartLayer02Fill = value2;
        //PieChartLayer03Fill = value3;
        Debug.Log("Value1: " + value1);
        Debug.Log("Value2: " + value2);
        Debug.Log("Value3: " + value3);

        PieChartTotalValue = value1 + value2 + value3;

        PieChartLayer01.fillAmount = value1 / PieChartTotalValue;
        PieChartLayer01.color = PieChart01Colour;
        PieChartSeg01KeyColour.color = PieChart01Colour;
        PieChartseg01KeyText.text = Value1Name;

        PieChartLayer02.fillAmount = (value1 + value2) / PieChartTotalValue;
        PieChartLayer02.color = PieChart02Colour;
        PieChartSeg02KeyColour.color = PieChart02Colour;
        PieChartseg02KeyText.text = Value2Name;

        PieChartLayer03.fillAmount = (value1 + value2 + value3) / PieChartTotalValue;
        PieChartLayer03.color = PieChart03Colour;
        PieChartSeg03KeyColour.color = PieChart03Colour;
        PieChartseg03KeyText.text = Value3Name;
    }
}
