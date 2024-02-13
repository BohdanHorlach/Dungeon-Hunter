using System;
using UnityEngine;


[CreateAssetMenu(fileName = "GameplayStatistics", menuName = "Dungeon Hunter/GameplayStatistics")]
public class GameplayStatistics : ScriptableObject
{
    private int bestTry;
    private int totalExp;


    public int BestTry { get => bestTry; }
    public int TotalExp { get => totalExp; }

    public event Action StatisticsChange;


    public void SetBestTry(int value)
    {
        if (bestTry < value)
        {
            bestTry = value;
            StatisticsChange?.Invoke();
        }
    }


    public void AddToTotal(int value)
    {
        if (value > 0)
        {
            totalExp += value;
            StatisticsChange?.Invoke();
        }
    }
}