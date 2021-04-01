using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeSelecter : MonoBehaviour
{
    [SerializeField] TMP_Text hourText;
    [SerializeField] TMP_Text minuteText;

    public void AddHour()
    {
        int newHour = int.Parse(hourText.text) + 1;
        if (newHour > 23) newHour = 0;
        hourText.text = newHour <= 9 ? ("0" + newHour.ToString()) : newHour.ToString();
    }

    public void DecreaseHour()
    {
        int newHour = int.Parse(hourText.text) - 1;
        if (newHour < 0) newHour = 23;
        hourText.text = newHour <= 9 ? ("0" + newHour.ToString()) : newHour.ToString();
    }

    public void AddMinute()
    {
        int newMinute = int.Parse(minuteText.text) + 1;
        if (newMinute > 59) newMinute = 0;
        minuteText.text = newMinute <= 9 ? ("0" + newMinute.ToString()) : newMinute.ToString();
    }

    public void DecreaseMinute()
    {
        int newMinute = int.Parse(minuteText.text) - 1;
        if (newMinute < 0) newMinute = 59;
        minuteText.text = newMinute <= 9 ? ("0" + newMinute.ToString()) : newMinute.ToString();
    }
}
