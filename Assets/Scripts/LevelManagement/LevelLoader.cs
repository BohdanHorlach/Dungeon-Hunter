using UnityEngine;


public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void HideScreen()
    {
        animator.SetTrigger("HideLoaded");
    }


    public void ShowScreen()
    {
        animator.SetTrigger("ShowLoaded");
    }
}