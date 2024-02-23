using UnityEngine;


public class EnemyAnimatorHandler : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform mainTransform;
    [SerializeField] private HealthHandler helthHandler;

    private Vector2 previousPosition;
    private bool isFacingRight = true;


    private void Start()
    {
        previousPosition = mainTransform.position;
    }



    private void OnEnable()
    {
        helthHandler.HealthChanged += TakeDamage;
        helthHandler.Death += Death;
    }


    private void OnDisable()
    {
        helthHandler.HealthChanged -= TakeDamage;
        helthHandler.Death -= Death;
    }


    private void Update()
    {
        SetWalkAnimation();
    }


    private void SetWalkAnimation()
    {
        Vector2 actualPosition = mainTransform.position;
        bool isWalk = actualPosition.Equals(previousPosition) == false;

        SetFlip(actualPosition.x - previousPosition.x);

        animator.SetBool("isWalk", isWalk);
        previousPosition = actualPosition;
    }


    private void SetFlip(float diferenceFromX)
    {
        if (diferenceFromX > 0.0f && isFacingRight == false || diferenceFromX < 0.0f && isFacingRight == true)
            Flip();
    }


    private void Flip()
    {
        isFacingRight = !isFacingRight;

        mainTransform.Rotate(0f, 180f, 0f);
    }



    private void TakeDamage()
    {
        SetTrigger("TakeHit");
    }


    private void Death()
    {
        SetTrigger("Death");
    }


    public void SetTrigger(string triggerName)
    {
        animator.SetTrigger(triggerName);
    }
}