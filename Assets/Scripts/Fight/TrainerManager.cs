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
        if (_NPCManager.Instance.TrainerPokemons[chosenPokemon].hp <= 0)
            if (_NPCManager.Instance.TrainerPokemons.Length > chosenPokemon + 1)
            {
                Debug.Log(chosenPokemon);
                _PokemonEQ.Instance.ActivePokemon.giveXP(_NPCManager.Instance.TrainerPokemons[chosenPokemon].level);
                PokemonTrainerCounter[chosenPokemon].SetActive(false);
                chosenPokemon++;
            }
            else
            {
                _NPCManager.Instance.MarkDefeated(_NPCManager.Instance.TrainerName);
                _PokemonEQ.Instance.ActivePokemon.giveXP(_NPCManager.Instance.TrainerPokemons[chosenPokemon].level);
                //Debug.Log("DIED");
                dialogeManager.StopAllCoroutines();
                dialogeManager.StartCoroutine(dialogeManager.EndedBattle(_NPCManager.Instance.TrainerName));
                //StartCoroutine();
            }
    }



    private void SetTrainer()
    {
        foreach (var item in PokemonTrainerCounter)
        {
            item.SetActive(false);
        }

        for (int i = 0; i < _NPCManager.Instance.TrainerPokemons.Length; i++)
        {
            PokemonTrainerCounter[i].SetActive(true);
        }

    }


    private void setUpEnemyTrainerPokemon()
    {
        enemyPokemonImage.sprite = _NPCManager.Instance.TrainerPokemons[chosenPokemon].image;
        enemyPokemonName.text = _NPCManager.Instance.TrainerPokemons[chosenPokemon].PokemonNameOut();
    }

    public void Attack(int playerAttackCounter) 
    {
        Pokemon enemyPokemon = _NPCManager.Instance.TrainerPokemons[chosenPokemon];
        Attack enemyAttack = enemyPokemon.GetRandomAttack();

        Pokemon playerPokemon = _PokemonEQ.Instance.ActivePokemon;
        Attack playerAttack = playerPokemon.AttacksActive[playerAttackCounter];

        if (playerAttack.howFastAttackIs(playerPokemon)>= enemyAttack.howFastAttackIs(enemyPokemon))
            StartCoroutine(dialogeManager.PokemonFightCutscene(_PokemonEQ.Instance.ActivePokemon, playerPokemon.AttacksActive[playerAttackCounter], _NPCManager.Instance.TrainerPokemons[chosenPokemon], enemyAttack));
        else
            StartCoroutine(dialogeManager.PokemonFightCutscene(_NPCManager.Instance.TrainerPokemons[chosenPokemon], enemyAttack, _PokemonEQ.Instance.ActivePokemon, playerPokemon.AttacksActive[playerAttackCounter]));
    }

    public void ChangePokemon()
    {
        Pokemon playerPokemon = _PokemonEQ.Instance.ActivePokemon;

        Pokemon enemyPokemon = _NPCManager.Instance.TrainerPokemons[chosenPokemon];
        Attack enemyAttack = enemyPokemon.GetRandomAttack();

        StartCoroutine(dialogeManager.PokemonFightCutscene(_PokemonEQ.Instance.ActivePokemon, null, _NPCManager.Instance.TrainerPokemons[chosenPokemon], enemyAttack,true));
    }




}
