using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Rendering.MaterialUpgrader;

public class DialogeFightManager : MonoBehaviour
{

    [Header("dialoge window")]
    [SerializeField] private GameObject dialogeWindow;
    [SerializeField] private TMP_Text dialogeText;

    [Header("Settings")]
    [SerializeField] private PokemonInFightSO FightSO;
    [SerializeField] private CreatureEq CreatureEqSO;
    [SerializeField] private TrainerManager trainerManager;


    //[HideInInspector] public List<string> queueText = new List<string>();
    [HideInInspector] public bool playerFirstAttack = true;

    [HideInInspector] public int enemyDamage;
    [HideInInspector] public int playerDamage;

    [HideInInspector] public bool dialogeFinished = true;
    [HideInInspector] public bool canPlayNextDialoge = true;
    void Start()
    {
        dialogeText.text = string.Empty;
        dialogeWindow.SetActive(false);
    }

    void Update()
    {
        
    }

    public IEnumerator DialogeShow(string textToEnter)
    {
        dialogeText.text = "";
        dialogeFinished = false;


        for (int i = 0; i < textToEnter.Length; i++) 
        {
            dialogeText.text += textToEnter[i];
            yield return new WaitForSeconds(0.1f);

        }


        dialogeFinished = true;
    }

    public IEnumerator FightPokemons(int playerAttackCounter)
    {


        dialogeWindow.SetActive(true);
        
        yield return StartCoroutine(DialogeShow(DamageApplyTrainer(playerAttackCounter)));

        yield return new WaitForSeconds(1);

        yield return StartCoroutine(DialogeShow(DamageApplyTrainer(playerAttackCounter,false)));

        dialogeWindow.SetActive(false);
    }

    private string DamageApplyTrainer(int playerAttackCounter, bool firstRound = true) 
    {
        string output;

        Pokemon enemyPokemon = FightSO.pokemonsToBattleTrainer[trainerManager.chosenPokemon];
        Attack enemyAttack = enemyPokemon.GetRandomAttack();
        
        Pokemon playerPokemon = CreatureEqSO.ActivePokemon;
        Attack playerAttack = playerPokemon.AttacksActive[playerAttackCounter];

        if (playerAttack.howFastAttackIs(playerPokemon) >= enemyAttack.howFastAttackIs(enemyPokemon))
        {
            if (firstRound)
            {
                FightSO.pokemonsToBattleTrainer[trainerManager.chosenPokemon].hp -= playerAttack.getDamage(playerPokemon, enemyPokemon);
                output = playerPokemon.PokemonNameOut() + " attacked " + enemyPokemon.PokemonNameOut() + " with " + playerAttack.attackName;
            }
            else
            {
                CreatureEqSO.ActivePokemon.hp -= enemyAttack.getDamage(enemyPokemon, playerPokemon);
                output = enemyPokemon.PokemonNameOut() + " attacked " + playerPokemon.PokemonNameOut() + " with " + enemyAttack.attackName;
            }

        }
        else 
        {
            if (firstRound)
            {
                CreatureEqSO.ActivePokemon.hp -= enemyAttack.getDamage(enemyPokemon, playerPokemon);
                output = enemyPokemon.PokemonNameOut() + " attacked " + playerPokemon.PokemonNameOut() + " with " + enemyAttack.attackName;
            }
            else
            {
                FightSO.pokemonsToBattleTrainer[trainerManager.chosenPokemon].hp -= playerAttack.getDamage(playerPokemon, enemyPokemon);
                output = playerPokemon.PokemonNameOut() + " attacked " + enemyPokemon.PokemonNameOut() + " with " + playerAttack.attackName;
            }
        }


        //output = playerPokemon.PokemonNameOut() + " attacked " + enemyPokemon.PokemonNameOut();
        return output;
    }



}
