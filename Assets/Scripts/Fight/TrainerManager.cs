using JetBrains.Annotations;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrainerManager : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField] private GameObject[] PokemonTrainerCounter;
    [SerializeField] private DialogeFightManager dialogeManager;


    [Header("Enemy Pokemon")]
    [SerializeField] private Image enemyPokemonImage;
    [SerializeField] private TMP_Text enemyPokemonName;

    public int chosenPokemon = 0;



    void Start()
    {
        SetTrainer();
        //Debug.Log(_GlobalPokemon.TrainerPokemons.Length);
    }

    void Update()
    {

        setUpEnemyTrainerPokemon();
    }


    public void CheckAndSwapTrainer() 
    {
        if (_GlobalPokemon.TrainerPokemons[chosenPokemon].hp <= 0)
            if (_GlobalPokemon.TrainerPokemons.Length > chosenPokemon + 1)
            {
                Debug.Log(chosenPokemon);
                _GlobalPokemon.ActivePokemon.giveXP(_GlobalPokemon.TrainerPokemons[chosenPokemon].level);
                PokemonTrainerCounter[chosenPokemon].SetActive(false);
                chosenPokemon++;
            }
            else
            {
                _GlobalPokemon.ActivePokemon.giveXP(_GlobalPokemon.TrainerPokemons[chosenPokemon].level);
                Debug.Log("DIED");
            }
    }



    private void SetTrainer()
    {
        foreach (var item in PokemonTrainerCounter)
        {
            item.SetActive(false);
        }

        for (int i = 0; i < _GlobalPokemon.TrainerPokemons.Length; i++)
        {
            PokemonTrainerCounter[i].SetActive(true);
        }

    }


    private void setUpEnemyTrainerPokemon()
    {
        enemyPokemonImage.sprite = _GlobalPokemon.TrainerPokemons[chosenPokemon].image;
        enemyPokemonName.text = _GlobalPokemon.TrainerPokemons[chosenPokemon].PokemonNameOut();
    }

    public void Attack(int playerAttackCounter) 
    {
        Pokemon enemyPokemon = _GlobalPokemon.TrainerPokemons[chosenPokemon];
        Attack enemyAttack = enemyPokemon.GetRandomAttack();

        Pokemon playerPokemon = _GlobalPokemon.ActivePokemon;
        Attack playerAttack = playerPokemon.AttacksActive[playerAttackCounter];

        if (playerAttack.howFastAttackIs(playerPokemon)>= enemyAttack.howFastAttackIs(enemyPokemon))
            StartCoroutine(dialogeManager.PokemonFightCutscene(_GlobalPokemon.ActivePokemon, playerPokemon.AttacksActive[playerAttackCounter], _GlobalPokemon.TrainerPokemons[chosenPokemon], enemyAttack));
        else
            StartCoroutine(dialogeManager.PokemonFightCutscene(_GlobalPokemon.TrainerPokemons[chosenPokemon], enemyAttack,_GlobalPokemon.ActivePokemon, playerPokemon.AttacksActive[playerAttackCounter]));
    }

    public void ChangePokemon()
    {
        Pokemon playerPokemon = _GlobalPokemon.ActivePokemon;

        Pokemon enemyPokemon = _GlobalPokemon.TrainerPokemons[chosenPokemon];
        Attack enemyAttack = enemyPokemon.GetRandomAttack();

        StartCoroutine(dialogeManager.PokemonFightCutscene(_GlobalPokemon.ActivePokemon, null, _GlobalPokemon.TrainerPokemons[chosenPokemon], enemyAttack,true));
    }




}
