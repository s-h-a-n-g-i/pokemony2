using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using FMOD.Studio;

public class FightSystemManager : MonoBehaviour
{
  
    [Header("My Pokemon")]
    [SerializeField] private Image myPokemonImage;
    [SerializeField] private TMP_Text myPokemonName;



    [Header("Managers")]
    [SerializeField] private SingleFightManager singleFight;
    [SerializeField] private TrainerManager trainerFight;
    private DialogeFightManager dialogeFightManager;



    public int chosenPokemonPlayer = 0;

    private bool checkDeadOnce = true;

    private void Start()
    {
        dialogeFightManager = GetComponent<DialogeFightManager>();
        if (_NPCManager.Instance.isItTrainer)
            singleFight.enabled = false;
        else
            trainerFight.enabled = false;
        _PokemonEQ.Instance.pokemonUsedInFight.Clear();
        _PokemonEQ.Instance.pokemonUsedInFight.Add(0);
        for (int i = 0; i < _PokemonEQ.Instance.EqPokemons.Length; i++) 
        {
            if (_PokemonEQ.Instance.EqPokemons[i].hp > 0) 
            {
                chosenPokemonPlayer = i;
                break;
            }
        }
        setUpMyPokemon();
    }

    void Update()
    {
        _PokemonEQ.Instance.ActivePokemon.CheckPokemonHasAttacks();

        myPokemonCheckForDead();

        EnemyPokemonSetup();
        //Debug.Log(trainerFight.FinishedBattle);
    }



    private void EnemyPokemonSetup() 
    {
        if (_NPCManager.Instance.isItTrainer)
            trainerFight.SetupPokemonAnimation();
        else
            singleFight.SetupPokemonAnimation();
    }


    private void myPokemonCheckForDead()
    {
        if (_NPCManager.Instance.isItTrainer)
            if(trainerFight.FinishedBattle)
                checkDeadPkmn();
        
        else if(singleFight.FinishedBattle)
                checkDeadPkmn();

    }

    private void checkDeadPkmn() 
    {
        if (!_PokemonEQ.Instance.IsAllPokemonAlive && checkDeadOnce) 
        {
            //Debug.Log("kurwanigger");
            checkDeadOnce = false;
            dialogeFightManager.StopAllCoroutines();
            StartCoroutine(dialogeFightManager.AllPokemonPlayerDead());
        }
    }


    public void setUpMyPokemon()
    {
        if (_PokemonEQ.Instance.ActivePokemon != _PokemonEQ.Instance.EqPokemons[chosenPokemonPlayer])
        {
            _PokemonEQ.Instance.ActivePokemon = _PokemonEQ.Instance.EqPokemons[chosenPokemonPlayer];
        }

        myPokemonImage.sprite = _PokemonEQ.Instance.ActivePokemon.image;
        myPokemonName.text = _PokemonEQ.Instance.ActivePokemon.PokemonNameOut(true);
    }

    public void Attacking(int playerAttackCounter) 
    {
        if (_NPCManager.Instance.isItTrainer)
            trainerFight.Attack(playerAttackCounter);
        else
            singleFight.Attack(playerAttackCounter);
    }

}
