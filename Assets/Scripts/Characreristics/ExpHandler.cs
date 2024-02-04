using System;
using UnityEngine;


public class ExpHandler : MonoBehaviour
{
    [SerializeField] private CharacterCharacteristics characteristics;
    private int exp;

    public int Exp { get => exp; }
    public event Action<int> ExpChanged;


    private void Awake()
    {
        exp = characteristics.Exp;
    }


    public void AddExp(int value)
    {
        if (value < 0)
            return;

        exp += value;
        ExpChanged?.Invoke(exp);
    }


    public void ResetExp()
    {
        exp = 0;
        ExpChanged?.Invoke(exp);
    }
}