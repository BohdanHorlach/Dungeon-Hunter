using System;
using UnityEngine;


public class PlayerInputDetector : MonoBehaviour
{
    [SerializeField] private KeyCode rollButton;
    [SerializeField] private KeyCode attackButton;

    public event Action<Vector3> MoveDetected;
    public event Action RollDetected;
    public event Action AttackDetected;


    private void Update()
    {
        Move();
        Roll();
        Attack();
    }


    private void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector3 directional = new Vector3(moveX, moveY);

        MoveDetected?.Invoke(directional);
    }



    private void Roll()
    {
        if (Input.GetKeyDown(rollButton))
            RollDetected?.Invoke();
    }


    private void Attack()
    {
        if (Input.GetKeyDown(attackButton))
            AttackDetected?.Invoke();
    }
}