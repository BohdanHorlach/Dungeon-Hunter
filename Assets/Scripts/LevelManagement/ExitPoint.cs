using UnityEngine;
using UnityEngine.Events;

public class ExitPoint : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private ExpHandler playerExpHandler;
    [SerializeField] private PlayerAnimatorHandler playerAnimator;
    [SerializeField] private GameplayStatistics gameplayStatistics;
    [SerializeField] private GameModeSwitch mainMenu;
    [SerializeField] private KeyCode backToMenu;


    private bool playerIsNear = false;


    public UnityEvent PlayerReturnedToTheMenu;


    private void OnEnable()
    {
        playerAnimator.GameOver += GoBackToMenu;
    }


    private void OnDisable()
    {
        playerAnimator.GameOver -= GoBackToMenu;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            animator.SetBool("isShowButton", true);
            playerIsNear = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            animator.SetBool("isShowButton", false);
            playerIsNear = false;
        }
    }


    private void Update()
    {
        if (playerIsNear == false)
            return;

        if (Input.GetKeyDown(backToMenu))
            GoBackToMenu();
    }


    private void GoBackToMenu()
    {
        gameplayStatistics.SetBestTry(playerExpHandler.Exp);
        gameplayStatistics.AddToTotal(playerExpHandler.Exp);
        playerExpHandler.ResetExp();
        animator.SetBool("isShowButton", false);

        PlayerReturnedToTheMenu.Invoke();
        mainMenu.BackToMainMenu();
    }
}