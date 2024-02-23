using System.Collections;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerInputDetector inputDetector;
    [SerializeField] private Rigidbody2D playerBody;
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

        Vector3 resultPosition = directional * speedMove * Time.deltaTime;

        playerBody.MovePosition(playerBody.transform.position + resultPosition);
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
            playerBody.MovePosition(playerBody.transform.position + direction * Time.deltaTime);
            yield return null;
        }
    }


    public void StopMove()
    {
        isMoved = false;
    }


    public void KeepMoveing()
    {
        isMoved = true;
    }
}