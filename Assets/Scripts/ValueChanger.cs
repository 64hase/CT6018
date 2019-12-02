using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueChanger : MonoBehaviour
{
    [SerializeField] private Button UpButton;
    [SerializeField] private Button DownButton;
    [SerializeField] private Text Value;
    [SerializeField] private int ValueLimit;
    private int CurrentValue;
    [SerializeField] private bool isDayValue;
    [SerializeField] private Text CurrentMonthValue;
    [SerializeField] private Text DayValue;
    private int MonthValue;
    // Start is called before the first frame update
    void Start()
    {
        UpButton.onClick.AddListener(OnValueChangeUp);
        DownButton.onClick.AddListener(OnValueChangeDown);
        CurrentValue = int.Parse(Value.text);
        MonthValue = int.Parse(CurrentMonthValue.text);
    }

    private void OnValueChangeUp()
    {
        MonthValue = int.Parse(CurrentMonthValue.text);
        if (MonthValue == 2)
        {
            ValueLimit = 28;
            if (int.Parse(DayValue.text) + 1 > 28)
            {
                DayValue.text = "28";
            }
        }
        if (MonthValue == 4 || MonthValue == 6 || MonthValue == 9 || MonthValue == 11)
        {
            ValueLimit = 30;
                if (int.Parse(DayValue.text) + 1 > 30)
                {
                    DayValue.text = "30";
                }
        }
        if (MonthValue == 1 || MonthValue == 3 || MonthValue == 5 || MonthValue == 7 || MonthValue == 8 || MonthValue == 10 || MonthValue == 12)
        {
            ValueLimit = 31;
        }
        if (CurrentValue + 1 <= ValueLimit)
        {
            CurrentValue = CurrentValue + 1;
            Value.text = CurrentValue.ToString();
        }
    }
    private void OnValueChangeDown()
    {
        MonthValue = int.Parse(CurrentMonthValue.text);
        if (MonthValue == 2)
        {
            ValueLimit = 28;
            if (int.Parse(DayValue.text) - 1 > 28)
            {
                DayValue.text = "28";
            }
        }
        if (MonthValue == 4 || MonthValue == 6 || MonthValue == 9 || MonthValue == 11)
        {
            ValueLimit = 30;
            if (int.Parse(DayValue.text) - 1 > 30)
            {
                DayValue.text = "30";
            }
        }
        if (MonthValue == 1 || MonthValue == 3 || MonthValue == 5 || MonthValue == 7 || MonthValue == 8 || MonthValue == 10 || MonthValue == 12)
        {
            ValueLimit = 31;
        }
        if (CurrentValue - 1 >= 1)
            {
                CurrentValue = CurrentValue - 1;
                Value.text = CurrentValue.ToString();
            }
    }
}
