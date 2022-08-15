using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LvlGrid : MonoBehaviour
{
    [SerializeField] private Vector2Int size;
    [SerializeField] private Vector2 offset;
    [SerializeField] private float scale;
    [SerializeField] private int countCardToEndGame;
    [SerializeField] private int secondsToFail;
    [SerializeField] private List<GameObject> lvlCards;

    [Inject] private DiContainer _diContainer;
    public int TimeToFail => secondsToFail;
    public int CountCardToEndGame => countCardToEndGame;
    public Card[,] Grid { get; private set; }
    public void Initialize()
    {
        Grid = new Card[size.x, size.y];
        var list = lvlCards;
        for (var i = 0; i < size.x; i++)
        {
            for (var y = 0; y < size.y; y++)
            {
                var num = Random.Range(0, list.Count);
                Grid[i, y] = _diContainer.InstantiatePrefab(list[num], transform).GetComponent<Card>();
                Grid[i, y].transform.position += new Vector3(i * offset.x, y * offset.y, 0);
                list.RemoveAt(num);
            }
        }
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
