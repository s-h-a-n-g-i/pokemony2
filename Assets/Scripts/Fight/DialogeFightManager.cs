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
    [SerializeField] private TrainerManager trainerManager;


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
            yield return new WaitForSeconds(0.05f);

        }


        dialogeFinished = true;
    }


    public IEnumerator PokemonFightCutscene(Pokemon dealingPokemon, Attack dealingAttack, Pokemon targetPokemon, Attack targetAttack)
    {
        string output, action;
        dialogeWindow.SetActive(true);

        var atk1 = dealingAttack.getDamage(dealingPokemon, targetPokemon);
        var atk2 = targetAttack.getDamage(targetPokemon, dealingPokemon);

        targetPokemon.hp -= atk1.Item1;
        if (targetPokemon.hp < 0)
        { 
            targetPokemon.hp = 0;
            action = " killed ";
        }
        else
            action = " attacked ";

        output = dealingPokemon.PokemonNameOut() + action + targetPokemon.PokemonNameOut() + " with " + atk1.Item2 + dealingAttack.attackName;
        yield return StartCoroutine(DialogeShow(output));


        yield return new WaitForSeconds(1);
        dealingPokemon.hp -= atk2.Item1;
        if (dealingPokemon.hp < 0)
        {
            dealingPokemon.hp = 0;
            action = " killed ";
        }
        else
            action = " attacked ";

        output = targetPokemon.PokemonNameOut() + action + dealingPokemon.PokemonNameOut() + " with " + atk2.Item2 + targetAttack.attackName;
        yield return StartCoroutine(DialogeShow(output));

        yield return new WaitForSeconds(1);

        dialogeWindow.SetActive(false);

        if (_GlobalPokemon.isItTrainer)
            trainerManager.CheckAndSwapTrainer();
    }




    /////////////////////////////////////////////////////////////////////////////////
    private bool isPokemonDead(int pokemonHP, int pokemonDamage) 
    {
        if(pokemonHP-pokemonDamage<=0)
            return true;
        else
            return false;
        
    }






}
