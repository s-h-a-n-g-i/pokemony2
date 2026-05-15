using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bushes : MonoBehaviour
{
    [SerializeField] private Vector2 levelPokemon;
    [SerializeField] BushChances[] ChancesForBushes;
    private float chanceToDrop = 0;
    
    private GameObject player;

    private PlayerMovement movement;
    
    private void Start()
    {
        player = GameObject.Find("Player");
        movement = player.GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (movement.isMoving)
            chanceToDrop = 0.1f;
        else
            chanceToDrop = 0;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject == player && _PokemonEQ.Instance.IsAllPokemonAlive)
            if (Random.Range(0, 100) < chanceToDrop)
            { 
                Pokemon s = GetRandomPokemon();
                _NPCManager.Instance.isItTrainer = false;
                _FightManager.Instance.EnemyPokemon = s;

                PlayerSave.Instance.placed = false;
                StartCoroutine(StartBushFight());
            }
        
    }

    private Pokemon GetRandomPokemon() 
    {
        float weight = 0;

        foreach (var p in ChancesForBushes)
            weight += p.chances;

        float randomWeight = Random.value * weight;

        foreach (var p in ChancesForBushes)
        {
            if (randomWeight < p.chances)
                return new Pokemon(p.pokemon,(int)Random.Range(levelPokemon.x, levelPokemon.y));
            randomWeight -= p.chances;
        }
        return null;
    }

    private IEnumerator StartBushFight() 
    {
        player.GetComponent<PlayerMovement>().StopPlayer();
        yield return StartCoroutine(player.GetComponent<Bobles>().exclBobel());
        SceneManager.LoadScene("Fight");

    }


}
