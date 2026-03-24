using JetBrains.Annotations;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrainerManager : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField] private PokemonInFightSO FightSO;
    [SerializeField] private GameObject[] PokemonTrainerCounter;
    [SerializeField] private DialogeFightManager dialogeManager;


    [Header("Enemy Pokemon")]
    [SerializeField] private Image enemyPokemonImage;
    [SerializeField] private TMP_Text enemyPokemonName;

    public int chosenPokemon = 0;



    void Start()
    {
        SetTrainer();
    }

    void Update()
    {
        //Debug.Log(CreatureEqSO.ActivePokemon.basicName);
        //CreatureEqSO.ActivePokemon = CreatureEqSO.Equipped[chosenPokemon];
        
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


    private void setUpEnemyTrainerPokemon()
    {
        enemyPokemonImage.sprite = FightSO.pokemonsToBattleTrainer[chosenPokemon].image;
        enemyPokemonName.text = FightSO.pokemonsToBattleTrainer[chosenPokemon].PokemonNameOut();
    }

    public void Attack(int playerAttackCounter) 
    {

        StartCoroutine(dialogeManager.FightPokemons(1));
    }


    //private string SetAttack(Pokemon pokemonAttacking, Attack atk, Pokemon pokemonDamaged)
    //{
    //    if (pokemonDamaged.checkHit(atk))
    //    {
    //        DealDamage(atk.getDamage(pokemonAttacking), enemyPokemonActive);
    //        return atk.attackName + " hitler" + enemyPokemonActive.PokemonNameOut();
    //    }
    //    else
    //    {
    //        return atk.attackName + " not hitler" + enemyPokemonActive.PokemonNameOut();
    //    }
    //}



    //private void DealDamage(int damage, Pokemon pokemon)
    //{
    //    if (pokemon.def >= damage)
    //        damage = pokemon.def + 1;
    //    pokemon.hp = damage - pokemon.def;
    //}


}
