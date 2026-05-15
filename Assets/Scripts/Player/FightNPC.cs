using System.Collections;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FightNPC : MonoBehaviour
{
    //[SerializeField] private int trainerID = 0;

    [Header("Dialoge before fight")]
    [SerializeField] DialogeLine[] dialogeLines;
    [Header("what Pokemon has trainer")]
    [HideInInspector] public bool defeated = false;
    [SerializeField] private PokemonSO[] pokemonToFight;
    [SerializeField] private int level = 3;

    //[SerializeField] private PokemonInFightSO pokemonToFighting;
    private Pokemon[] pokemon;

    private DialogeManager dialoge;

    private PlayerMovement playerMovement;
    private Bobles playerBobles;

    void Start()
    {
        playerBobles = GameObject.Find("Player").GetComponent<Bobles>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();

        TrainerSetupNPC();
        dialoge = GameObject.Find("GameManager").GetComponent<DialogeManager>();
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

        PlayerSave.Instance.placed = false;
        StartCoroutine(StartFight());
    }

    IEnumerator StartFight() 
    {
        playerBobles.questBobel();
        playerMovement.StopPlayer();
        foreach (DialogeLine dialogeLine in dialogeLines)
            yield return StartCoroutine(dialoge.DialogeShow(dialogeLine.whoSayes+": "+dialogeLine.whatSayes));
        SceneManager.LoadScene("Fight");
    }
}
