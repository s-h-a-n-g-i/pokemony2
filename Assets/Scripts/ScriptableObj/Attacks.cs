using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Attacks")]
public class Attacks : ScriptableObject
{
    public string attackName;
    public PokemonTypes attackType;

    public int pp;
    public int damage;
    public int accuracy;
    public int speed;
    public string desc;

    public Effects effect;
}
