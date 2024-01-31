using System.Collections;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerInputDetector inputDetector;
    [SerializeField] private Transform playerTransform;
    [SerializeField, Min(0)] private float speedMove;
    [SerializeField, Min(0)] private float rollForce;


    private bool isMoved = true;


    private void OnEnable()
    {
        inputDetector.MoveDetected += Move;
    }


    private void OnDisable()
    {
        inputDetector.MoveDetected -= Move;
    }


    private void Move(Vector3 directional)
    {
        if (isMoved == false)
            return;

        playerTransform.position += directional * speedMove * Time.deltaTime;
    }


    private void Roll()
    {
        if (isMoved == false)
            return;

        Vector3 direction = transform.right * rollForce;
        StartCoroutine("MoveFromRoll", direction);
    }


    private void StopRoll()
    {
        StopCoroutine("MoveFromRoll");
    }


    private IEnumerator MoveFromRoll(Vector3 direction)
    {
        while (true)
        {
            playerTransform.position += direction * Time.deltaTime;
            yield return null;
        }
    }


    private void StopMove()
    {
        isMoved = false;
    }


    private void KeepMoveing()
    {
        isMoved = true;
    }
}