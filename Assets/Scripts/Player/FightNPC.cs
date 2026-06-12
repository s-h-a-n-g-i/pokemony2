using System.Collections;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FightNPC : MonoBehaviour
{
    //[SerializeField] private int trainerID = 0;

    [Header("Sprites")]
    [SerializeField] private Sprite top;
    [SerializeField] private Sprite right;
    [SerializeField] private Sprite down;
    [SerializeField] private Sprite left;

    [Header("Dialoge before fight")]
    [SerializeField] DialogeLine[] dialogeLines;
    [Header("what Pokemon has trainer")]
    [SerializeField] private bool canEscape = true;
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
        if (_NPCManager.Instance.IsDefeated(gameObject.name) || _PlayerSave.Instance.playerName == "Death")
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        Transform s = collision.gameObject.transform;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        float diffx = 0;
        float diffy = 0;

        diffx = s.position.x - transform.position.x;
        if (s.position.x > transform.position.x)
        { 
            spriteRenderer.sprite = right; 
        }

        if (s.position.x < transform.position.x)
        {
            diffx = transform.position.x - s.position.x;
            spriteRenderer.sprite = left; 
        }

        diffy = s.position.y - transform.position.y;
        if (s.position.y > transform.position.y && diffy>diffx)
        { 
            spriteRenderer.sprite = top;
        }

        diffy = transform.position.y - s.position.y;
        if (s.position.y < transform.position.y && diffy > diffx)
        { 
            spriteRenderer.sprite = down;
        }


        //_GlobalPokemon.ResetBeforeFight();

        //_GlobalPokemon.TrainerID = trainerID;
        _NPCManager.Instance.canescape = canEscape;
        _NPCManager.Instance.TrainerName = gameObject.name;
        _NPCManager.Instance.isItTrainer = true;
        _NPCManager.Instance.TrainerPokemons = pokemon;

        _PlayerSave.Instance.placed = false;
        StartCoroutine(StartFight());
    }

    IEnumerator StartFight() 
    {
        playerMovement.StopPlayer();
        yield return StartCoroutine(playerBobles.questBobel());
        foreach (DialogeLine dialogeLine in dialogeLines)
            yield return StartCoroutine(dialoge.DialogeShow(dialogeLine.whoSayes+": "+dialogeLine.whatSayes));
        SceneManager.LoadScene("FightNew");
    }
}
