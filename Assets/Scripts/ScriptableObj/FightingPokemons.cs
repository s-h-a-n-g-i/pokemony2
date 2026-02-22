using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Fight")]
public class FightingPokemons : ScriptableObject
{
    public bool isThisTrainer = false;
    public Pokemon pokemonToBattle;
    public Pokemon[] pokemonsToBattleTrainer;

}
