using System.Collections.Generic;
using UnityEngine;

public class _PokemonEQ : MonoBehaviour
{
    public static _PokemonEQ Instance;


    public Pokemon ActivePokemon;
    public Pokemon[] EqPokemons = new Pokemon[5];
    public List<Pokemon> AllHavePokemons;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    public void CatchedPokemon(Pokemon catched) 
    {
        AllHavePokemons.Add(catched);
    }

}
