using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text tmpText;
    public event Action OnTimerEnd;
    private int timer;

    public void StartTimer(int time)
    {
        timer = time;
        StartCoroutine(TimerLogic());
    }

    public void StopTimer()
    {
        StopAllCoroutines();
    }

    public string GetCurrentTime()
    {
        if (timer <= 0)
            return "00:00";
        return $"{timer / 60:00}:{timer % 60:00}";
    }

    public void RemoveTime()
    {
        timer -= 20;
    }

    private IEnumerator TimerLogic()
    {
        while (timer > 0)
        {
            timer--;
            tmpText.text = $"{timer / 60:00}:{timer % 60:00}";
            yield return new WaitForSeconds(1);
        }
        tmpText.text = "00:00";
        OnTimerEnd?.Invoke();
    }
}
