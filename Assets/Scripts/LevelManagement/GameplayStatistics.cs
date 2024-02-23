using UnityEngine;


[CreateAssetMenu(fileName = "GameplayStatistics", menuName = "Dungeon Hunter/GameplayStatistics")]
public class GameplayStatistics : ScriptableObject
{
    private int bestTry;
    private int totalExp;


    public int BestTry { get => bestTry; }
    public int TotalExp { get => totalExp; }


    public void SetBestTry(int value)
    {
        if (bestTry < value)
        {
            bestTry = value;
        }
    }


    public void AddToTotal(int value)
    {
        if (value > 0)
        {
            totalExp += value;
        }
    }
}