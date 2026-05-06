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
        //else
        //    singleFight.Attack();
    }


    //public IEnumerator BattleFinished(string name) 
    //{
    //    yield return StartCoroutine("You Have Killed "+ name + "!");
    //    List<Pokemon> pokemonsInFight = new List <Pokemon>();
    //    for (int i = 0; i < _PokemonEQ.Instance.pokemonUsedInFight.Count; i++) 
    //        pokemonsInFight.Add(_PokemonEQ.Instance.EqPokemons[_PokemonEQ.Instance.pokemonUsedInFight[i]]);
        
    //    foreach (Pokemon p in pokemonsInFight) 
    //    {
    //        if (_NPCManager.Instance.isItTrainer)
    //            GiveXPToPokemonsFromTrainer(p);
    //        while (p.CheckForLevelUp())
    //        {
    //            yield return StartCoroutine(p.PokemonNameOut() + " Leveled UP!");
    //            yield return new WaitForSeconds(0.5f);
    //            if (p.CheckForEvolution())
    //            {
    //                yield return StartCoroutine(p.PokemonNameOut() + " EVOLVED!");
    //                yield return new WaitForSeconds(0.5f);
    //                p.Evolution();
    //                setUpMyPokemon();
    //            }
    //            string newAttackName = p.CheckForAttacksAdded();
    //            if (newAttackName != null) 
    //            {
    //                if (newAttackName[0] == '-')
    //                {
    //                    newAttackName.Replace("-", "");
    //                    yield return StartCoroutine(p.PokemonNameOut() + " LEARNED NEW ATTACK! (" + newAttackName + ")");
    //                    yield return new WaitForSeconds(0.5f);

    //                }
    //                else 
    //                {
    //                    yield return StartCoroutine(p.PokemonNameOut() + " LEARNED NEW ATTACK! (" + newAttackName + ")");
    //                    yield return new WaitForSeconds(0.5f);
    //                }
    //            }
    //        }
    //    }
    //    yield return new WaitForSeconds(1f);

    //}

    private void GiveXPToPokemonsFromTrainer(Pokemon pokemon) 
    {
        pokemon.giveXP(_NPCManager.Instance.TrainerPokemons[0].level/_PokemonEQ.Instance.pokemonUsedInFight.Count);
    }

}
