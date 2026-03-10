using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FightManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private FightingPokemons FightSO;
    [SerializeField] private CreatureEq CreatureEqSO;
    [SerializeField] private Eq EqSO;
    [SerializeField] private GameObject[] PokemonTrainerCounter;


    [Header("My Pokemon")]
    [SerializeField] private Image myPokemonImage;
    [SerializeField] private TMP_Text myPokemonName;


    [Header("Enemy Pokemon")]
    [SerializeField] private Image enemyPokemonImage;
    [SerializeField] private TMP_Text enemyPokemonName;

    public int chosenPokemon = 0;

    private bool isTrainer;


    void Start()
    {
        CreatureEqSO.ActivePokemon = CreatureEqSO.Equipped[chosenPokemon];
        isTrainer = FightSO.isThisTrainer;
        if (isTrainer) SetTrainer();
    }

    void Update()
    {

        CreatureEqSO.ActivePokemon = CreatureEqSO.Equipped[chosenPokemon];
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
        enemyPokemonImage.sprite = FightSO.pokemonToBattle.image;
        enemyPokemonName.text = FightSO.pokemonToBattle.PokemonNameOut();
    }

    private void SetAttack()
    {

    }

    public void Attack()
    {

    }

}
