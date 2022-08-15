using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WinWindow : MonoBehaviour
{
    [SerializeField] private TMP_Text timeText;

    public void EnableWindow(string time)
    {
        timeText.text = time;
        gameObject.SetActive(true);
    }
    public void DisableWindow()
    {
        gameObject.SetActive(false);
    }

    public void LoadNextLvl()
    {
        SceneLoader.LoadScene(SceneLoader.Game);
    }
}
