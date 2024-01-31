using System;
using UnityEngine;


[RequireComponent(typeof(CircleCollider2D))]
public class TargetFinder : MonoBehaviour
{
    [SerializeField, Min(1)] private float blindSpotDistance;

    private Transform target;
    private bool isLeaving = false;

    public event Action<Vector3> TargetFound;
    public event Action<Vector3> TargetLeaving;
    public event Action TargetLost;

    public bool IsLeaving { get { return isLeaving; } }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
            return;
        
        target = collision.transform;
        isLeaving = false;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            TargetFound?.Invoke(target.position);
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            isLeaving = true;
    }


    private void Update()
    {
        if (target == null)
            return;

        if (isLeaving == true)
            TargetLeaving?.Invoke(target.position);

        if (Vector3.Distance(transform.position, target.position) > blindSpotDistance)
            ResetTarget();
    }


    private void ResetTarget()
    {
        isLeaving = false;
        target = null;
        TargetLost?.Invoke();
    }
}