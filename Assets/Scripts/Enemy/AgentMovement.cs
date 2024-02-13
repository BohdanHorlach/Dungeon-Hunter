using UnityEngine;
using UnityEngine.AI;


public class AgentMovement : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private TargetFinder finder;
    [SerializeField] private NavMeshAgent agent;

    private float agentDrift = 0.001f;


    private void Start()
    {
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