using UnityEngine;


public class MainMenu : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private PlayerAnimatorHandler playerAnimator;
    [SerializeField] private Animator animator;
    [SerializeField] private float timeTransition;

    private void OnEnable()
    {
        playerAnimator.GameOver += BackToMainMenu;
    }


    private void OnDisable()
    {
        playerAnimator.GameOver -= BackToMainMenu;
    }


    private void Transition()
    {
        player.position = new Vector3(0, 0, 0);
        animator.SetTrigger("HideLoaded");
    }


    public void BackToMainMenu()
    {
        animator.SetTrigger("ShowLoaded");
        Timer.StartTimer(timeTransition, Transition);
    }
}