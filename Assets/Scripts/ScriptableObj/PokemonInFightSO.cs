using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/PokemonInFight")]
public class PokemonInFightSO : ScriptableObject
{
    public bool isThisTrainer = false;
    public Pokemon pokemonToBattle;
    public Pokemon[] pokemonsToBattleTrainer;

}
