using System.Collections.Generic;
using UnityEngine;

public class _PokemonEQ : MonoBehaviour
{
    public static _PokemonEQ Instance;


    public Pokemon ActivePokemon;
    public Pokemon[] EqPokemons = {null,null,null,null,null};
    public List<Pokemon> AllHavePokemons;
    public int LevelingUpPokemon;
    public List<int> pokemonUsedInFight = new List<int>();

    public bool IsAllPokemonAlive = true;
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

    private void Update()
    {
        IsAllPokemonAlive = CheckAllPokemonLife();
    }

    private bool CheckAllPokemonLife() 
    {
        bool[] allChecked = { false, false, false, false, false };
        for (int i = 0; i < 5; i++)
            if (EqPokemons[i] != null &&( EqPokemons[i].basicName != string.Empty))
                if(EqPokemons[i].hp>0) allChecked[i] = true;

        return allChecked[0] || allChecked[1] || allChecked[2] || allChecked[3] || allChecked[4];
    }

}
