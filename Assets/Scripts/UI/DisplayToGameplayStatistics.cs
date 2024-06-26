using TMPro;
using UnityEngine;


public class DisplayToGameplayStatistics : MonoBehaviour
{
    [SerializeField] private GameplayStatistics statistics;
    [SerializeField] private TextMeshProUGUI textTotalScore;
    [SerializeField] private TextMeshProUGUI textBestTry;


    private void Start()
    {
        SetValue();
    }


    private void Update()
    {
        SetValue();
    }


    private void SetValue()
    {
        textTotalScore.text = statistics.TotalExp.ToString();
        textBestTry.text = statistics.BestTry.ToString();
    }
}