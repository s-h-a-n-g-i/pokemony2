using System.Collections.Generic;
using UnityEngine;

public static class _GlobalPokemon
{
    
    public static Pokemon ActivePokemon;
    public static Pokemon[] EqPokemons = new Pokemon[5];
    public static List<Pokemon> AllHavePokemons;


    public static Pokemon EnemyPokemon;

    public static int TrainerChosenPokemon = 0;
    public static Pokemon[] TrainerPokemons = new Pokemon[5];
    public static bool isItTrainer = false;


    public static Attack PlayerAttack;
    public static Attack EnemyAttack;


    public static void ResetBeforeFight() 
    {
        EnemyPokemon = null;

        TrainerPokemons = null;
        TrainerChosenPokemon = 0;

        isItTrainer = false;
        EnemyAttack = null;
        PlayerAttack = null;
    }



    public static Dictionary<PokemonTypes, float> CALC_AngelDamage = new()
    {
        {PokemonTypes.Angel, 1},
        {PokemonTypes.Demon, 2},
        {PokemonTypes.FallenAngel, 0.5f},
        {PokemonTypes.Seraph, 1.5f},
        {PokemonTypes.Ghost, 1},
        {PokemonTypes.Archangel, 0.75f},
        {PokemonTypes.Nefilim, 1}
    };


    public static Dictionary<PokemonTypes, float> CALC_DemonDamage = new()
    {
        {PokemonTypes.Angel, 0.5f},
        {PokemonTypes.Demon, 1f},
        {PokemonTypes.FallenAngel, 2f},
        {PokemonTypes.Seraph, 0.75f},
        {PokemonTypes.Ghost, 1.5f},
        {PokemonTypes.Archangel, 1f},
        {PokemonTypes.Nefilim, 1f}
    };

    public static Dictionary<PokemonTypes, float> CALC_FallenAngelDamage = new()
    {
        {PokemonTypes.Angel, 2f},
        {PokemonTypes.Demon, 0.5f},
        {PokemonTypes.FallenAngel, 1f},
        {PokemonTypes.Seraph, 1f},
        {PokemonTypes.Ghost, 0.75f},
        {PokemonTypes.Archangel, 1.5f},
        {PokemonTypes.Nefilim, 1f}
    };

    public static Dictionary<PokemonTypes, float> CALC_ArchangelDamage = new()
    {
        {PokemonTypes.Angel, 1.5f},
        {PokemonTypes.Demon, 1f},
        {PokemonTypes.FallenAngel, 0.75f},
        {PokemonTypes.Seraph, 2f},
        {PokemonTypes.Ghost, 0.5f},
        {PokemonTypes.Archangel, 1f},
        {PokemonTypes.Nefilim, 1f}
    };

    public static Dictionary<PokemonTypes, float> CALC_SeraphDamage = new()
    {
        {PokemonTypes.Angel, 0.75f},
        {PokemonTypes.Demon, 1.5f},
        {PokemonTypes.FallenAngel, 1f},
        {PokemonTypes.Seraph, 1f},
        {PokemonTypes.Ghost, 2f},
        {PokemonTypes.Archangel, 0.5f},
        {PokemonTypes.Nefilim, 1f}
    };

    public static Dictionary<PokemonTypes, float> CALC_GhostDamage = new()
    {
        {PokemonTypes.Angel, 1f},
        {PokemonTypes.Demon, 0.75f},
        {PokemonTypes.FallenAngel, 1.5f},
        {PokemonTypes.Seraph, 0.5f},
        {PokemonTypes.Ghost, 1f},
        {PokemonTypes.Archangel, 2f},
        {PokemonTypes.Nefilim, 1f}
    };
}
