using System;
using UnityEngine;


public class AttackerEnemy : MonoBehaviour
{
    [SerializeField] private EnemyAnimatorHandler animator;
    [SerializeField] private string[] triggersOfAttackForAnimator;
    [SerializeField] private TargetFinder finder;
    [SerializeField] private AgentMovement agent;
    [SerializeField] private float minTimeOfInactivy;
    [SerializeField] private float maxTimeOfInactivy;
    [SerializeField] private float meleeDistance;


    protected Action[] attackActions;
    protected Vector3 target;
    protected bool isActive = true;
    protected bool isLostTarget = true;
    protected bool IsLeaving { get { return finder.IsLeaving; } }


    private void OnEnable()
    {
        finder.TargetFound += SetTarget;
        finder.TargetLost += LostTarget;
    }


    private void OnDisable()
    {
        finder.TargetFound -= SetTarget;
        finder.TargetLost -= LostTarget;
    }


    private void InitializeAttackActionsArray()
    {
        int length = triggersOfAttackForAnimator.Length;
        attackActions = new Action[length];

        for(int i = 0; i < length; i++)
        {
            int index = i;
            attackActions[i] = () => SetTrigger(triggersOfAttackForAnimator[index]);
        }
    }


    private void Start()
    {
        InitializeAttackActionsArray();
    }


    private void Update()
    {
        if (isActive == false || isLostTarget == true)
            return;

        if(Vector3.Distance(agent.transform.position, target) <= meleeDistance && IsLeaving == false)
            Attack();
    }


    private void Attack()
    {
        float timeOfInactivy = UnityEngine.Random.Range(minTimeOfInactivy, maxTimeOfInactivy);
        int indexAction = UnityEngine.Random.Range(0, attackActions.Length);

        attackActions[indexAction]();

        Timer.StartTimer(timeOfInactivy, ResetActive);
    }


    private void ResetActive()
    {
        isActive = true;
        KeepMoving();
    }


    private void SetTarget(Vector3 position)
    {
        target = position;

        isLostTarget = false;
    }


    private void LostTarget()
    {
        isLostTarget = true;
    }


    protected void SetTrigger(string name)
    {
        StopMoving();
        animator.SetTrigger(name);
    }


    private void StopMoving()
    {
        isActive = false;
        agent.StopMove();
    }


    private void KeepMoving()
    {
        agent.GoMove();
    }

}