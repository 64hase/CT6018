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

    //Action for increasing the value.
    private void OnValueChangeUp()
    {
        MonthValue = int.Parse(CurrentMonthValue.text);
            //Sets day limit to 28 and lowers day value to 28 if month is set to 2 (February) and current value is above 28.
            if (MonthValue == 2)
            {
            if (isDayValue == true)
            {
                ValueLimit = 28;
                if (int.Parse(DayValue.text) >= 28)
                {
                    DayValue.text = "28";
                }
            }
            }
            //Sets day limit to 30 and lowers value to 30 if month is set to 4,6,9 or 11 (April, June, September or November) and current value is above 30.
            if (MonthValue == 4 || MonthValue == 6 || MonthValue == 9 || MonthValue == 11)
            {
            if (isDayValue == true)
            {
                ValueLimit = 30;
                if (int.Parse(DayValue.text) + 1 > 30)
                {
                    DayValue.text = "30";
                }
            }
            }
            //Sets day limit to 31 if month is set to 1,3,5,7,8,10 or 12 (January, March, May, July, August or October).
            if (MonthValue == 1 || MonthValue == 3 || MonthValue == 5 || MonthValue == 7 || MonthValue == 8 || MonthValue == 10 || MonthValue == 12)
            {
            if (isDayValue == true)
            {
                ValueLimit = 31;
            }
            }
            CurrentValue = Mathf.Clamp(CurrentValue + 1, 1, ValueLimit);
            Value.text = CurrentValue.ToString();
    }

    //Action for decreasing the value
    private void OnValueChangeDown()
    {
        //Sets day limit to 28 and lowers value to 28 if month is set to 2 (February) and current value is above 28.
        MonthValue = int.Parse(CurrentMonthValue.text);
        if (MonthValue == 2)
        {
            if (isDayValue == true)
            {
                ValueLimit = 28;
                if (int.Parse(DayValue.text) >= 28)
                {
                    DayValue.text = "28";
                }
            }
        }
        //Sets day limit to 30 and lowers value to 30 if month is set to 4,6,9,11 (April, June, September or November) and current value is above 30.
        if (MonthValue == 4 || MonthValue == 6 || MonthValue == 9 || MonthValue == 11)
        {
            if (isDayValue == true)
            {
                ValueLimit = 30;
                if (int.Parse(DayValue.text) - 1 > 30)
                {
                    DayValue.text = "30";
                }
            }
        }
        //Sets day limit to 31 if month is set to 1,3,5,7,8,10 or 12 (January, March, May, July, August or October). 
        if (MonthValue == 1 || MonthValue == 3 || MonthValue == 5 || MonthValue == 7 || MonthValue == 8 || MonthValue == 10 || MonthValue == 12)
        {
            if (isDayValue == true)
            {
                ValueLimit = 31;
            }
        }
                CurrentValue = Mathf.Clamp(CurrentValue - 1, 1, ValueLimit);
                Value.text = CurrentValue.ToString();
    }

}
