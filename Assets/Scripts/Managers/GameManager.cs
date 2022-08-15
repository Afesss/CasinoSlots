using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Zenject;
using System;

public class GameManager : MonoBehaviour
{
    [Header("Levels")]
    [SerializeField] private GameObject[] lvls;

    [Header("References")]
    [SerializeField] private Prompt prompt;
    [SerializeField] private Timer timer;
    [SerializeField] private WinWindow winWindow;
    [SerializeField] private FailWindow failWindow;

    [Header("Sounds")]
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip loseSound;
    [SerializeField] private AudioClip rightSound;
    [SerializeField] private AudioClip wrongSound;
    [SerializeField] private AudioClip hintSound;
    [SerializeField] private AudioClip bombSound;
    [SerializeField] private AudioClip timerSound;

    [Inject] private DiContainer _diContainer;
    [Inject] private Sound _sound;

    public bool BlockInput { get; private set; }

    private int countOpenCard;

    private int CurrentLvl
    {
        get => PlayerPrefs.GetInt("CurrentLvl", 0);
        set => PlayerPrefs.SetInt("CurrentLvl", value);
    }

    private Card firstOpenedCard;
    public LvlGrid LvlGrid { get; private set; }

    private void Start()
    {
        InitializeLvl();
        InitializeTimer();
    }

    public void RestartLvl()
    {
        foreach(var card in LvlGrid.Grid)
        {
            card.CloseCard();
        }
        firstOpenedCard = null;
        BlockInput = false;
        countOpenCard = 0;
        timer.StartTimer(LvlGrid.TimeToFail);
        failWindow.DisableWindow();
    }
    private void InitializeLvl()
    {
        countOpenCard = 0;

        if (CurrentLvl >= lvls.Length)
            CurrentLvl = lvls.Length - 1;

        var obj = _diContainer.InstantiatePrefab(lvls[CurrentLvl]);
        LvlGrid = obj.GetComponent<LvlGrid>();
        LvlGrid.Initialize();

        foreach (var card in LvlGrid.Grid)
        {
            card.OnOpen += Card_OnOpen;
        }
    }
    private void InitializeTimer()
    {
        timer.StartTimer(LvlGrid.TimeToFail);
        timer.OnTimerEnd += Timer_OnTimerEnd;
    }
    
    private void Timer_OnTimerEnd()
    {
        _sound.PlaySound(loseSound);
        failWindow.EnableWindow(timer.GetCurrentTime());
    }

    private void Card_OnOpen(Card card)
    {
        switch (card.CardType)
        {
            case CardType.Card:
                OpenCard(card);
                break;
            case CardType.Prompt:
                prompt.AddPrompt();
                _sound.PlaySound(hintSound);
                break;
            case CardType.Bomb:
                _sound.PlaySound(bombSound);
                prompt.RemovePrompt();
                break;
            case CardType.Time:
                _sound.PlaySound(timerSound);
                timer.RemoveTime();
                break;
            case CardType.GameOver:
                BlockInput = true;
                timer.StopTimer();
                Observable.Timer(TimeSpan.FromSeconds(1)).Subscribe(t =>
                {
                    _sound.PlaySound(loseSound);
                    failWindow.EnableWindow(timer.GetCurrentTime());
                });
                break;
        }
    }

    private void OpenCard(Card card)
    {
        if (firstOpenedCard == null)
        {
            firstOpenedCard = card;
            return;
        }
        BlockInput = true;
        if (firstOpenedCard.Id == card.Id)
        {
            BlockInput = false;
            firstOpenedCard = null;
            _sound.PlaySound(rightSound);
            countOpenCard++;
            if (countOpenCard == LvlGrid.CountCardToEndGame)
            {
                timer.StopTimer();
                CurrentLvl++;
                Observable.Timer(TimeSpan.FromSeconds(1)).Subscribe(t =>
                {
                    _sound.PlaySound(winSound);
                    winWindow.EnableWindow(timer.GetCurrentTime());
                });
            }
                
            return;
        }

        Observable.Timer(TimeSpan.FromSeconds(0.5f)).Subscribe(t =>
        {
            _sound.PlaySound(wrongSound);
            BlockInput = false;
            firstOpenedCard.CloseCard();
            firstOpenedCard = null;
            card.CloseCard();
        });
    }
}
