using System.Collections.Generic;
using UnityEngine;

public class _PokemonEQ : MonoBehaviour
{
    public static _PokemonEQ Instance;


    public static Pokemon ActivePokemon;
    public static Pokemon[] EqPokemons = new Pokemon[5];
    public static List<Pokemon> AllHavePokemons;

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

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
