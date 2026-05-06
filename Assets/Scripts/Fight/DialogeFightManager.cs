using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogeFightManager : MonoBehaviour
{

    [Header("dialoge window")]
    [SerializeField] private GameObject dialogeWindow;
    [SerializeField] private TMP_Text dialogeText;

    [Header("Settings")]
    [SerializeField] private TrainerManager trainerManager;
    private FightSystemManager fightSystemManager;

    [HideInInspector] public bool playerFirstAttack = true;

    [HideInInspector] public int enemyDamage;
    [HideInInspector] public int playerDamage;

    [HideInInspector] public bool dialogeFinished = true;
    [HideInInspector] public bool canPlayNextDialoge = true;
    void Start()
    {
        fightSystemManager = GetComponent<FightSystemManager>();
        dialogeText.text = string.Empty;
        dialogeWindow.SetActive(false);
    }

    void Update()
    {
        //_NPCManager.Instance.GetComponent<_NPCManager>().NPCInBattle = "sperma";
        //_NPCManager.Instance.NPCInBattle = "";
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

    ///////////////MARTWIAK
    public IEnumerator EndedBattle(string nameDefeated)
    {
        dialogeWindow.SetActive(true);
        yield return StartCoroutine(DialogeShow(nameDefeated + " has been defeated!"));

        List<Pokemon> pokemonsInFight = new List<Pokemon>();
        for (int i = 0; i < _PokemonEQ.Instance.pokemonUsedInFight.Count; i++)
            pokemonsInFight.Add(_PokemonEQ.Instance.EqPokemons[_PokemonEQ.Instance.pokemonUsedInFight[i]]);

        foreach (Pokemon p in pokemonsInFight)
        {
            if (_NPCManager.Instance.isItTrainer)
                GiveXPToPokemonsFromTrainer(p);
            while (p.CheckForLevelUp())
            {
                Debug.Log(p.evoLevel1);
                p.LvLUp();
                Debug.Log(p.evoLevel1);

                yield return StartCoroutine(DialogeShow(p.PokemonNameOut() + " Leveled UP!"));
                yield return new WaitForSeconds(0.5f);

                if (p.CheckForEvolution())
                {
                    yield return StartCoroutine(DialogeShow(p.PokemonNameOut() + " EVOLVED!"));
                    yield return new WaitForSeconds(0.5f);
                    p.Evolution();
                    fightSystemManager.setUpMyPokemon();
                }
                string newAttackName = p.CheckForAttacksAdded();
                if (newAttackName != null)
                {
                    if (newAttackName[0] == '-')
                    {
                        newAttackName.Replace("-", "");
                        yield return StartCoroutine(DialogeShow(p.PokemonNameOut() + " LEARNED NEW ATTACK! (" + newAttackName + ")"));
                        yield return new WaitForSeconds(0.5f);

                    }
                    else
                    {
                        yield return StartCoroutine(DialogeShow(p.PokemonNameOut() + " LEARNED NEW ATTACK! (" + newAttackName + ")"));
                        yield return new WaitForSeconds(0.5f);
                    }
                }
            }
        }
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(PlayerSave.Instance._sceneName);
    }

    private void GiveXPToPokemonsFromTrainer(Pokemon pokemon)
    {
        pokemon.giveXP(_NPCManager.Instance.TrainerPokemons[0].level / _PokemonEQ.Instance.pokemonUsedInFight.Count);
    }


    ////////////// CALA SEKWENCJA ATAKU OBU POKEMONOW
    public IEnumerator PokemonFightCutscene(Pokemon firstPokemon, Attack firstAttack, Pokemon secondPokemon, Attack secondAttack, bool changedPokemon = false)
    {

        dialogeWindow.SetActive(true);


        if (!changedPokemon)
        {
            var atk1 = firstAttack.getDamage(firstPokemon, secondPokemon);

            yield return StartCoroutine(DialogeShow(
                ProvideAttack(firstPokemon, firstAttack, secondPokemon, atk1.Item1, atk1.Item2)));
        }
        else
            yield return StartCoroutine(DialogeShow("Pokemon changed to " + firstPokemon.PokemonNameOut()));



        //////TEST CZY POKEMON MOZE ZAATAKOWAC (ZE NIE JEST MARTWY PO ATAKU)
        if (secondPokemon.hp != 0)
        {
            yield return new WaitForSeconds(1);

            var atk2 = secondAttack.getDamage(secondPokemon, firstPokemon);

            yield return StartCoroutine(DialogeShow(
                ProvideAttack(secondPokemon, secondAttack, firstPokemon, atk2.Item1, atk2.Item2)));

        }

        yield return new WaitForSeconds(1);
        if (secondPokemon.hp != 0)
            dialogeWindow.SetActive(false);
        if (_NPCManager.Instance.isItTrainer)
            trainerManager.FinishedBattle = true;
    }


    private string ProvideAttack(Pokemon dealingPokemon, Attack dealingAttack, Pokemon targetPokemon , int atkDamage, string atkPower) 
    {
        targetPokemon.hp -= atkDamage;
        string action = CheckKill(targetPokemon);
        return dealingPokemon.PokemonNameOut() + action + targetPokemon.PokemonNameOut() + " with " + atkPower + dealingAttack.attackName;
        
    }

    private string CheckKill(Pokemon pokemonToCheck) 
    {
        if (pokemonToCheck.hp < 0)
        {
            pokemonToCheck.hp = 0;
            return  " killed ";
        }
        else
            return  " attacked ";
    }
}
