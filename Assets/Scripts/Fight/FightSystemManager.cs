using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FightSystemManager : MonoBehaviour
{
  
    [Header("My Pokemon")]
    [SerializeField] private Image myPokemonImage;
    [SerializeField] private TMP_Text myPokemonName;


    [Header("Managers")]
    [SerializeField] private SingleFightManager singleFight;
    [SerializeField] private TrainerManager trainerFight;


    public int chosenPokemonPlayer = 0;

    private void Start()
    {
        if(_NPCManager.Instance.isItTrainer)
            singleFight.enabled = false;
        else
            trainerFight.enabled = false;
        _PokemonEQ.Instance.pokemonUsedInFight.Clear();
        _PokemonEQ.Instance.pokemonUsedInFight.Add(0);
        setUpMyPokemon();
    }

    void Update()
    {

    }

    public void setUpMyPokemon()
    {
        if (_PokemonEQ.Instance.ActivePokemon != _PokemonEQ.Instance.EqPokemons[chosenPokemonPlayer]) 
        {
            _PokemonEQ.Instance.ActivePokemon = _PokemonEQ.Instance.EqPokemons[chosenPokemonPlayer];
        }

        myPokemonImage.sprite = _PokemonEQ.Instance.ActivePokemon.image;
        myPokemonName.text = _PokemonEQ.Instance.ActivePokemon.PokemonNameOut();
    }

    public void Attacking(int playerAttackCounter) 
    {
        if (_NPCManager.Instance.isItTrainer)
            trainerFight.Attack(playerAttackCounter);
        else
            singleFight.Attack(playerAttackCounter);
    }


}
