using UnityEngine;
using UnityEngine.SceneManagement;

public class FightNPC : MonoBehaviour
{

    [SerializeField] private Creatures[] pokemonToFight;
    [SerializeField] private int level = 3;
    [SerializeField] private FightingPokemons pokemonToFighting;
    private Pokemon[] pokemon;

    void Start()
    {
        pokemon = new Pokemon[pokemonToFight.Length];


        //Debug.Log(pokemonToFight.name);
        for(int i = 0; i < pokemon.Length;i++)
        {
            pokemon[i] = new Pokemon();
            //Debug.Log(i);
            pokemon[i].CreatePokemon(pokemonToFight[i], level);
        }

    }

    void Update()
    {
                
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        pokemonToFighting.isThisTrainer = true;
        pokemonToFighting.pokemonsToBattleTrainer = pokemon;

        SceneManager.LoadScene("Fight");
    }
}
