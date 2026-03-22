using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/AttackSO")]
public class AttackSO : ScriptableObject
{
    public string attackName;
    public PokemonTypes attackType;

    public int pp;
    public int maxPp;
    public int damage;
    public int accuracy;
    public int speed;
    public string desc;

    public Effects effect;


}