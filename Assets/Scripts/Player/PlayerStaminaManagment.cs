using UnityEngine;


public class PlayerStaminaManagment : MonoBehaviour
{
    [SerializeField] private StaminaHandler staminaHandler;
    [SerializeField] private float attackPower;
    [SerializeField] private float rollPower;
    

    public float AttackPower { get { return attackPower; } }
    public float RollPower { get { return rollPower; } }

    //Calls from animator

    private void AttackAction()
    {
        staminaHandler.Action(attackPower);
    }


    private void RollAction()
    {
        staminaHandler.Action(rollPower);
    }
}