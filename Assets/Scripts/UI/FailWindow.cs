using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FailWindow : MonoBehaviour
{
    [SerializeField] private TMP_Text timeText;

    public void EnableWindow(string time)
    {
        gameObject.SetActive(true);
        timeText.text = time;
    }

    public void DisableWindow()
    {
        gameObject.SetActive(false);
    }
}
