using UnityEngine;


public class ObjectSwitch : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject mainGameObject;


    public void TurnOff()
    {
        animator.StopPlayback();
        mainGameObject.SetActive(false);
    }
}