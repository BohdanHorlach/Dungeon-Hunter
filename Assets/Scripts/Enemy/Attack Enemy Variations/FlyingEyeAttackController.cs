using System.Collections;
using UnityEngine;


public class FlyingEyeAttackController : AttackerEnemy
{
    [SerializeField] private Transform movedTransform;
    [SerializeField] private float speedMoving;

    private Coroutine coroutine;


    private void Spin()
    {
        Vector3 directionMove = (target - transform.position).normalized;
        coroutine = StartCoroutine(MoveOfSpin(directionMove));
    }


    private IEnumerator MoveOfSpin(Vector3 direction)
    {
        while (true)
        {
            movedTransform.position += direction * speedMoving * Time.deltaTime;
            yield return null;
        }
    }


    private void StopSpin()
    {
        if (coroutine == null)
            return;

        StopCoroutine(coroutine);
        coroutine = null;
    }
}