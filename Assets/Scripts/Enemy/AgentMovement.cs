using UnityEngine;
using UnityEngine.AI;


public class AgentMovement : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private TargetFinder finder;
    [SerializeField] private NavMeshAgent agent;

    private float agentDrift = 0.001f;
    private Vector2 previousPosition;
    private bool isFacingRight = true;


    private void Start()
    {
        previousPosition = finder.transform.position;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }


    private void OnEnable()
    {
        finder.TargetFound += SetDestination;
        finder.TargetLeaving += SetDestination;
    }


    private void OnDisable()
    {
        finder.TargetFound -= SetDestination;
        finder.TargetLeaving -= SetDestination;
    }


    private void Update()
    {
        SetWalkAnimation();
    }


    private void SetWalkAnimation()
    {
        Vector2 actualPosition = agent.transform.position;
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

        agent.transform.Rotate(0f, 180f, 0f);
    }



    private void SetDestination(Vector3 target)
    {
        Vector3 driftPos = target;

        if (Mathf.Abs(transform.position.x - target.x) < agentDrift)
            driftPos = target + new Vector3(agentDrift, 0f, 0f);

        agent.SetDestination(driftPos);
    }


    public void StopMove()
    {
        agent.isStopped = true;
    }


    public void GoMove()
    {
        if(agent.gameObject.activeInHierarchy == true)
            agent.isStopped = false;
    }
}