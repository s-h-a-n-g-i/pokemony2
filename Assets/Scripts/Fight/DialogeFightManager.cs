using System.Collections;
using TMPro;
using UnityEngine;

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

        if (_GlobalPokemon.isItTrainer)
            trainerManager.CheckAndSwapTrainer();

        dialogeWindow.SetActive(false);

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
