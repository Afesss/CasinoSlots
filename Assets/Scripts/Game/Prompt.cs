using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

public class Prompt : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text tmpText;
    private int promptCount;

    [Inject] private GameManager _gameManager;

    private void Start()
    {
        promptCount = 2;
        tmpText.text = promptCount.ToString();
    }

    public void AddPrompt()
    {
        promptCount++;
        tmpText.text = promptCount.ToString();
    }

    public void RemovePrompt()
    {
        if (promptCount <= 0) return;

        promptCount--;
        tmpText.text = promptCount.ToString();
    }

    public void UsePrompt()
    {
        if (promptCount <= 0)
            return;

        RemovePrompt();

        int id = 0;
        foreach (var lvlCard in _gameManager.LvlGrid.Grid)
        {
            if (!lvlCard.Opened && lvlCard.Id > 0)
                id = lvlCard.Id;
        }
        foreach (var lvlCard in _gameManager.LvlGrid.Grid)
        {
            if (lvlCard.Id == id)
            {
                lvlCard.RotateToOpen();
                Observable.Timer(System.TimeSpan.FromSeconds(1)).Subscribe(t =>
                {
                    lvlCard.CloseCard();
                });
            }
        }
    }
}
