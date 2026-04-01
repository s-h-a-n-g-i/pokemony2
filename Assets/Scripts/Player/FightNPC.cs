using UnityEngine;
using UnityEngine.SceneManagement;

public class FightNPC : MonoBehaviour
{

    [SerializeField] private PokemonSO[] pokemonToFight;
    [SerializeField] private int level = 3;
    //[SerializeField] private PokemonInFightSO pokemonToFighting;
    private Pokemon[] pokemon;

    void Start()
    {
        pokemon = new Pokemon[pokemonToFight.Length];


        //Debug.Log(pokemonToFight.name);
        for(int i = 0; i < pokemon.Length;i++)
        {
            pokemon[i] = new Pokemon(pokemonToFight[i], level);
        }

    }

    void Update()
    {
                
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _GlobalPokemon.ResetBeforeFight();

        _GlobalPokemon.isItTrainer = true;
        _GlobalPokemon.TrainerPokemons = pokemon;

        SceneManager.LoadScene("Fight");
    }
}
