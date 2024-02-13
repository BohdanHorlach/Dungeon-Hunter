using System;
using UnityEngine;


public class PlayerAnimatorHandler : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private StaminaHandler staminaHandler;
    [SerializeField] private PlayerStaminaManagment playerStaminaManagment;
    [SerializeField] private PlayerInputDetector inputDetector;
    [SerializeField] private HealthHandler helthHandler;
    [SerializeField] private ExpHandler expHandler;
    [SerializeField] private DetectorHitFromAttack[] detectorsHitFromAttacks;

    private string[] triggersName = new string[] {
        "Roll",
        "Attack",
        "Hit",
        "Kickback",
        "Death"
    };
    private Action[] handlers;
    private bool isFacingRight = true;
    private bool isRoll = false;

    public event Action GameOver;

    private void Awake()
    {
        handlers = new Action[] {
            () => 
            { 
                if (isRoll == false && staminaHandler.CurrentStamina > playerStaminaManagment.RollPower)
                    animator.SetTrigger("Roll"); 
            },
            () => 
            {
                if(staminaHandler.CurrentStamina > playerStaminaManagment.AttackPower)
                    animator.SetTrigger("Attack"); 
            },
            () => animator.SetTrigger("Hit"),
            () => animator.SetTrigger("Kickback"),
            () => animator.SetTrigger("Death"),
    };
    }


    private void OnEnable()
    {
        inputDetector.MoveDetected += SetWalk;
        inputDetector.RollDetected += handlers[0];
        inputDetector.AttackDetected += handlers[1];
        helthHandler.DamageReceived += handlers[2];
        helthHandler.Death += handlers[4];
        helthHandler.Death += expHandler.ResetExp;

        foreach (DetectorHitFromAttack detector in detectorsHitFromAttacks)
            detector.OnKickback += handlers[3];
    }


    private void OnDisable()
    {
        inputDetector.RollDetected -= handlers[0];
        inputDetector.AttackDetected -= handlers[1];
        helthHandler.DamageReceived -= handlers[2];
        helthHandler.Death -= handlers[4];
        helthHandler.Death -= expHandler.ResetExp;

        foreach (DetectorHitFromAttack detector in detectorsHitFromAttacks)
            detector.OnKickback -= handlers[3];
    }


    private void SetWalk(Vector3 directionMove)
    {
        bool isWalk = Mathf.Abs(directionMove.x) > 0.1;

        isWalk = isWalk == false ? Mathf.Abs(directionMove.y) > 0.1 : isWalk;

        SetFlip(directionMove.x);

        animator.SetBool("isWalk", isWalk);
    }


    private void SetFlip(float moveFromX)
    {
        if (moveFromX > 0.0f && isFacingRight == false || moveFromX < 0.0f && isFacingRight == true)
            Flip();
    }


    private void Flip()
    {
        isFacingRight = !isFacingRight;

        transform.Rotate(0f, 180f, 0f);
    }


    private void SetRoll()
    {
        isRoll = true;
    }


    private void ResetRoll()
    {
        isRoll = false;
    }


    private void StopPlayback()
    {
        animator.StopPlayback();
    }

    
    private void PlayerDeath()
    {
        GameOver?.Invoke();
    }


    public void ResetAnimator()
    {
        foreach (string trigger in triggersName)
            animator.ResetTrigger(trigger);

        animator.SetBool("isWalk", false);
    }

}