using UnityEngine;
using UnityEngine.SceneManagement;

public class FightNPC : MonoBehaviour
{
    //[SerializeField] private int trainerID = 0;
    public bool defeated = false;
    [SerializeField] private PokemonSO[] pokemonToFight;
    [SerializeField] private int level = 3;
    //[SerializeField] private PokemonInFightSO pokemonToFighting;
    private Pokemon[] pokemon;

    void Start()
    {
        TrainerSetupNPC();

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

    private void TrainerSetupNPC()
    {
        if (_NPCManager.Instance.IsDefeated(gameObject.name))
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //_GlobalPokemon.ResetBeforeFight();

        //_GlobalPokemon.TrainerID = trainerID;
        _NPCManager.Instance.TrainerName = gameObject.name;
        _NPCManager.Instance.isItTrainer = true;
        _NPCManager.Instance.TrainerPokemons = pokemon;

        PlayerSave.placed = false;

        SceneManager.LoadScene("Fight");
    }
}
