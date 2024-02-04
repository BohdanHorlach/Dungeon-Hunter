using UnityEngine;


public class ExitPoint : MonoBehaviour
{
    [SerializeField] private ExpHandler playerExpHandler;
    [SerializeField] private GameplayStatistics gameplayStatistics;
    [SerializeField] private MainMenu mainMenu;


    public void GoBackToMenu()
    {
        gameplayStatistics.SetBestTry(playerExpHandler.Exp);
        gameplayStatistics.AddToTotal(playerExpHandler.Exp);
        playerExpHandler.ResetExp();

        mainMenu.BackToMainMenu();
    }
}