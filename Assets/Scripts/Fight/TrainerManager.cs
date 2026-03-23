using JetBrains.Annotations;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrainerManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private PokemonInFightSO FightSO;
    [SerializeField] private CreatureEq CreatureEqSO;
    [SerializeField] private Eq EqSO;
    [SerializeField] private GameObject[] PokemonTrainerCounter;


    [Header("My Pokemon")]
    [SerializeField] private Image myPokemonImage;
    [SerializeField] private TMP_Text myPokemonName;


    [Header("Enemy Pokemon")]
    [SerializeField] private Image enemyPokemonImage;
    [SerializeField] private TMP_Text enemyPokemonName;

    [Header("DialogeWindow")]
    [SerializeField] private GameObject dialogeWindow;
    [SerializeField] private TMP_Text dialogeText;

    [SerializeField] private Pokemon enemyPokemonActive;

    public int chosenPokemon = 0;

    private bool isTrainer;


    void Start()
    {
        dialogeWindow.SetActive(false);
        CreatureEqSO.ActivePokemon = CreatureEqSO.Equipped[chosenPokemon];
        isTrainer = FightSO.isThisTrainer;
        if (isTrainer) SetTrainer();
    }

    void Update()
    {
        Debug.Log(CreatureEqSO.ActivePokemon.basicName);
        //CreatureEqSO.ActivePokemon = CreatureEqSO.Equipped[chosenPokemon];
        setUpMyPokemon();
        setUpEnemyTrainerPokemon();
    }



    private void SetTrainer()
    {
        foreach (var item in PokemonTrainerCounter)
        {
            item.SetActive(false);
        }

        for (int i = 0; i < FightSO.pokemonsToBattleTrainer.Length; i++)
        {
            PokemonTrainerCounter[i].SetActive(true);
        }

    }

    private void setUpMyPokemon(int chosen = 0)
    {
        myPokemonImage.sprite = CreatureEqSO.ActivePokemon.image;
        myPokemonName.text = CreatureEqSO.ActivePokemon.PokemonNameOut();
    }

    private void setUpEnemyTrainerPokemon(int chosen = 0)
    {
        enemyPokemonImage.sprite = FightSO.pokemonsToBattleTrainer[chosen].image;
        enemyPokemonName.text = FightSO.pokemonsToBattleTrainer[chosen].PokemonNameOut();
    }


    private void setUpEnemyPokemon()
    {
        enemyPokemonActive = FightSO.pokemonToBattle;
        enemyPokemonImage.sprite = FightSO.pokemonToBattle.image;
        enemyPokemonName.text = FightSO.pokemonToBattle.PokemonNameOut();
    }


    public IEnumerator Attack(int attackSpeed, Attack atk, Pokemon pokemon)
    {
        dialogeText.text = "";
        dialogeWindow.SetActive(true);
        yield return new WaitForSeconds(0f);
    }




    private string SetAttack(Pokemon pokemonAttacking, Attack atk, Pokemon pokemonDamaged)
    {
        if (pokemonDamaged.checkHit(atk))
        {
            DealDamage(atk.getDamage(pokemonAttacking), enemyPokemonActive);
            return atk.attackName + " hitler" + enemyPokemonActive.PokemonNameOut();
        }
        else
        {
            return atk.attackName + " not hitler" + enemyPokemonActive.PokemonNameOut();
        }
    }



    private void DealDamage(int damage, Pokemon pokemon)
    {
        if (pokemon.def >= damage)
            damage = pokemon.def + 1;
        pokemon.hp = damage - pokemon.def;
    }








}
