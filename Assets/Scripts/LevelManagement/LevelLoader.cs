using System;
using UnityEngine;


public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator animator;


    public event Action ShowLoaded;


    public void HideScreen()
    {
        animator.SetTrigger("HideLoaded");
    }


    public void ShowScreen()
    {
        ShowLoaded?.Invoke();
        animator.SetTrigger("ShowLoaded");
    }
}