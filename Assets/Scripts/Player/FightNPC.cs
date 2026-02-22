using UnityEngine;
using UnityEngine.SceneManagement;

public class FightNPC : MonoBehaviour
{

    [SerializeField] private Creatures pokemonToFight;
    [SerializeField] private int level;
    [SerializeField] private FightingPokemons pokemonToFighting;
    private Pokemon pokemon;

    void Start()
    {
        Debug.Log(pokemonToFight.name);
        pokemon.CreatePokemon(pokemonToFight, 0);
    }

    void Update()
    {
                
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        pokemonToFighting.isThisTrainer = true;
        pokemonToFighting.pokemonToBattle = pokemon;

        SceneManager.LoadScene("Fight");
    }
}
