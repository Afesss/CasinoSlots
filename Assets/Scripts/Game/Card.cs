using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using Zenject;

public class Card : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private CardType cardType;

    public event Action<Card> OnOpen;

    [Inject] private GameManager _gameManager;
    public CardType CardType => cardType;
    public int Id => id;

    public bool Opened { get; private set; }

    private void OpenCard()
    {
        if (_gameManager.BlockInput || Opened)
            return;
        transform.DORotate(Vector3.up * 180, 1f);
        OnOpen?.Invoke(this);
        Opened = true;
    }
    public void RotateToOpen()
    {
        transform.DORotate(Vector3.up * 180, 1f);
        Opened = true;
    }
    public void CloseCard()
    {
        transform.DORotate(Vector3.zero, 1f).OnComplete(() =>
        {
            Opened = false;
        });
    }

    private void OnMouseDown()
    {
        OpenCard();
    }
}

public enum CardType
{
    Card,
    Prompt,
    Bomb,
    Time,
    GameOver
}
