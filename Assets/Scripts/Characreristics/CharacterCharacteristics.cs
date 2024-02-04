using UnityEngine;


[CreateAssetMenu(fileName = "CharacterCharacteristics", menuName = "Dungeon Hunter/CharacterCharacteristics")]
public class CharacterCharacteristics : ScriptableObject
{
    [SerializeField] private float health;
    [SerializeField] private float stamina;
    [SerializeField] private float damage;
    [SerializeField] private int exp;

    public float Health { get { return health; } }
    public float Stamina { get { return stamina; } }
    public float Damage { get { return damage; } }
    public int Exp { get { return exp; } }
}