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
    private SingleFightManager singleManager;
    private AttackSwapToNew attackSwap;
    [HideInInspector] public bool playerFirstAttack = true;

    [HideInInspector] public int enemyDamage;
    [HideInInspector] public int playerDamage;

    [HideInInspector] public bool dialogeFinished = true;
    [HideInInspector] public bool canPlayNextDialoge = true;

    [HideInInspector] public bool ChosenNewAttack = true;
    void Start()
    {
        attackSwap = GetComponent<AttackSwapToNew>();
        singleManager = GetComponent<SingleFightManager>();
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
        textToEnter.Replace("<b>", "|");
        textToEnter.Replace("</b>", ";");

        for (int i = 0; i < textToEnter.Length; i++) 
        {
            switch (textToEnter[i]) 
            {
                case '|':
                    dialogeText.text += "BOLD";
                    break;
                case ';':
                    dialogeText.text += "BOLDnT";
                    break;
                default:
                    dialogeText.text += textToEnter[i];
                    break;
            }

            yield return new WaitForSeconds(0.05f);

        }

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Z));
        dialogeFinished = true;
    }

    ///////////////MARTWIAK
    public IEnumerator EndedBattle(string nameDefeated)
    {
        dialogeWindow.SetActive(true);

        yield return StartCoroutine(DialogeShow("<b>" + nameDefeated + "</b> has been defeated!"));


        List<Pokemon> pokemonsInFight = new List<Pokemon>();
        for (int i = 0; i < _PokemonEQ.Instance.pokemonUsedInFight.Count; i++)
            pokemonsInFight.Add(_PokemonEQ.Instance.EqPokemons[_PokemonEQ.Instance.pokemonUsedInFight[i]]);

        foreach (Pokemon p in pokemonsInFight)
        {
            if (_NPCManager.Instance.isItTrainer)
                GiveXPToPokemonsFromTrainer(p);
            else
                p.giveXP(_FightManager.Instance.EnemyPokemon.level / _PokemonEQ.Instance.pokemonUsedInFight.Count);
            while (p.CheckForLevelUp())
            {
                ChosenNewAttack = true;
                p.LvLUp();
                

                yield return StartCoroutine(DialogeShow("<b>" + p.PokemonNameOut() + " Leveled UP!"));

                if (p.CheckForEvolution())
                {
                    yield return StartCoroutine(DialogeShow("<b>" + p.PokemonNameOut() + " EVOLVED!"));
                    p.Evolution();
                    fightSystemManager.setUpMyPokemon();
                    yield return StartCoroutine(DialogeShow("<b>"+p.PokemonNameOut() + " IS NEW EVOLUTION!"));
                }
                Attack newAttack = p.CheckForAttacksAdded();
                if (newAttack != null)
                {
                    if (newAttack.hasSlot)
                    {
                        yield return StartCoroutine(DialogeShow(p.PokemonNameOut() + " LEARNED NEW ATTACK: <b>" + newAttack.attackName + "</b>"));

                    }
                    else
                    {
                        ChosenNewAttack = false;
                        yield return StartCoroutine(DialogeShow(p.PokemonNameOut() + " LEARNED NEW ATTACK: <b>" + newAttack.attackName + "</b>"));
                        attackSwap.FullyAttackSwap(p,newAttack);
                        yield return new WaitUntil(() => ChosenNewAttack);
                    }
                }
            }
        }

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
            yield return StartCoroutine(DialogeShow("Pokemon changed to <b>" + firstPokemon.PokemonNameOut() + "</b>"));



        //////TEST CZY POKEMON MOZE ZAATAKOWAC (ZE NIE JEST MARTWY PO ATAKU)
        if (secondPokemon.hp != 0)
        {

            var atk2 = secondAttack.getDamage(secondPokemon, firstPokemon);

            yield return StartCoroutine(DialogeShow(
                ProvideAttack(secondPokemon, secondAttack, firstPokemon, atk2.Item1, atk2.Item2)));

        }

        if (secondPokemon.hp != 0)
            dialogeWindow.SetActive(false);
        if (_NPCManager.Instance.isItTrainer)
            trainerManager.FinishedBattle = true;
        else
            singleManager.FinishedBattle = true;
    }


    private string ProvideAttack(Pokemon dealingPokemon, Attack dealingAttack, Pokemon targetPokemon , int atkDamage, string atkPower) 
    {
        targetPokemon.hp -= atkDamage;
        string action = CheckKill(targetPokemon);
        return dealingPokemon.PokemonNameOut() + action + targetPokemon.PokemonNameOut() + " with <b>" + atkPower + "</b> " + dealingAttack.attackName;
        
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
