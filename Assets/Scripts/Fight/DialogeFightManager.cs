using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

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

    [Header("Animators")]
    public Animator playerAnimator;
    public Animator enemyAnimator;

    private float speedwagon = 0.05f;

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
        if (Input.GetKeyDown(KeyCode.Z)) 
        {
            speedwagon = 0;
        }
        PlayerPokemonAnimationSetup();
        //_NPCManager.Instance.GetComponent<_NPCManager>().NPCInBattle = "sperma";
        //_NPCManager.Instance.NPCInBattle = "";
    }

    private void PlayerPokemonAnimationSetup()
    {
        playerAnimator.SetBool("fly", _PokemonEQ.Instance.ActivePokemon.flying);
        playerAnimator.SetBool("dead", _PokemonEQ.Instance.ActivePokemon.hp<=0);
    }


    public void EnemyPokemonDead(bool live) 
    {
        enemyAnimator.SetBool("dead", live);
    }
    public void EnemyPokemonFly(bool live)
    {
        enemyAnimator.SetBool("fly", live);
    }

    //////POKAZYWANIE DIALOGOW W WALCE ?????????
    public IEnumerator DialogeShow(string textToEnter, bool lastText = true)
    {
        if (!lastText)
            dialogeWindow.SetActive(true);
        speedwagon = 0.05f;
        dialogeText.text = "";
        dialogeFinished = false;
        string oryginal = textToEnter;
        textToEnter = textToEnter.Replace("<b>", "|");
        textToEnter = textToEnter.Replace("</b>", ";");
        //Debug.Log(textToEnter);

        for (int i = 0; i < textToEnter.Length; i++) 
        {
            switch (textToEnter[i]) 
            {
                case '|':
                    dialogeText.text += "<b>";
                    break;
                case ';':
                    dialogeText.text += "</b>";
                    break;
                default:
                    dialogeText.text += textToEnter[i];
                    break;
            }

            AudioManager.Instance.PlayOneShot(FMODEvents.Instance.dialogeSound, transform.position);
            yield return new WaitForSeconds(speedwagon);
            if (speedwagon == 0)
            {
                dialogeText.text = oryginal;
                break;
            }
        }

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Z));
        dialogeFinished = true;
        if (!lastText)
        {
            if (_NPCManager.Instance.isItTrainer)
                trainerManager.FinishedBattle = true;
            else
                singleManager.FinishedBattle = true;
            dialogeWindow.SetActive(false);
        }

    }

    public IEnumerator EffectApply(Pokemon p) 
    {
        if (p.turnsToClearEffect == 0)
        {
            //Debug.Log(p.PokemonNameOut() + "sperma?");
            p.effects = Effects.None;
        }
        else
        {
            //Debug.Log(p.PokemonNameOut() + "got a stroke");
            switch (p.effects)
            {
                case Effects.Poison:
                    p.turnsToClearEffect--;
                    p.hp-=1;
                    if(p.hp<=0)p.hp=0;
                    AudioManager.Instance.PlayOneShot(FMODEvents.Instance.hurt, transform.position);
                    yield return StartCoroutine(DialogeShow(p.PokemonNameOut()+" is poisoned!"));
                    break;
                case Effects.Weakness:

                    break;
                case Effects.Blind:
                    p.accuracyX = -90;
                    yield return StartCoroutine(DialogeShow(p.PokemonNameOut() + " is blinded!"));
                    p.turnsToClearEffect--;

                    break;
                case Effects.Burn:
                    p.hp -= 5;
                    if (p.hp <= 0) p.hp = 0;
                    AudioManager.Instance.PlayOneShot(FMODEvents.Instance.hurt, transform.position);
                    yield return StartCoroutine(DialogeShow(p.PokemonNameOut() + " is burning!"));
                    p.turnsToClearEffect--;

                    break;
                case Effects.Buff:
                    
                    p.turnsToClearEffect--;
                    break;
            }
        }
        yield return new WaitForSeconds(0); 
    }


    ///////////////MARTWIAK ??????
    public IEnumerator EndedBattle(string nameDefeated)
    {
        if(_NPCManager.Instance.TrainerName == "Devil")
            SceneManager.LoadScene("Win");
        
        dialogeWindow.SetActive(true);

        yield return StartCoroutine(DialogeShow("<b>" + nameDefeated + "</b> has been defeated!"));


        yield return StartCoroutine(AddLevelUps());

        SceneManager.LoadScene(_PlayerSave.Instance._sceneName);
    }
    public IEnumerator AllPokemonPlayerDead()
    {

        if (_NPCManager.Instance.TrainerName == "Devil")
            SceneManager.LoadScene("Lose");

        dialogeWindow.SetActive(true);

        yield return StartCoroutine(DialogeShow("<b> You </b> has been defeated!"));


        SceneManager.LoadScene(_PlayerSave.Instance._sceneName);
    }

    public IEnumerator PokemonCaught()
    {
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.Catch, transform.position);
        dialogeWindow.SetActive(true);
        enemyAnimator.SetBool("action",true);
        enemyAnimator.SetTrigger("zlapany");
        yield return StartCoroutine(DialogeShow("<b>" + _FightManager.Instance.EnemyPokemon.basicName + "</b> has been caught!"));

        yield return StartCoroutine(AddLevelUps());

        SceneManager.LoadScene(_PlayerSave.Instance._sceneName);
    }



    private IEnumerator AddLevelUps() 
    {
        playerAnimator.SetBool("action", true);
        List<Pokemon> pokemonsInFight = new List<Pokemon>();
        for (int i = 0; i < _PokemonEQ.Instance.pokemonUsedInFight.Count; i++)
            pokemonsInFight.Add(_PokemonEQ.Instance.EqPokemons[_PokemonEQ.Instance.pokemonUsedInFight[i]]);

        foreach (Pokemon p in pokemonsInFight)
        {
            /////TO W NORMALNEJ WERSJI ZAMIAST TEGO:
            //p.xp = p.xpToNextLevel;
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
                    playerAnimator.SetTrigger("evo");
                    yield return StartCoroutine(DialogeShow("<b>" + p.PokemonNameOut() + " EVOLVED!"));
                    p.Evolution();
                    fightSystemManager.setUpMyPokemon();
                    playerAnimator.SetBool("evolved",true);
                    yield return StartCoroutine(DialogeShow("<b>" + p.PokemonNameOut() + " IS NEW EVOLUTION!"));
                }
                Attack newAttack = p.CheckForAttacksAdded();
                if (newAttack != null)
                {
                    if (newAttack.hasSlot)
                    {
                        yield return StartCoroutine(DialogeShow(p.PokemonNameOut() + " LEARNED NEW ATTACK: <b>" + newAttack.attackName + "</b>!"));

                    }
                    else
                    {
                        ChosenNewAttack = false;
                        yield return StartCoroutine(DialogeShow(p.PokemonNameOut() + " LEARNED NEW ATTACK: <b>" + newAttack.attackName + "</b>!"));
                        attackSwap.FullyAttackSwap(p, newAttack);
                        yield return new WaitUntil(() => ChosenNewAttack);
                    }
                }
            }
        }
    }


    private void GiveXPToPokemonsFromTrainer(Pokemon pokemon)
    {
        pokemon.giveXP(_NPCManager.Instance.TrainerPokemons[0].level * _NPCManager.Instance.TrainerPokemons.Length / _PokemonEQ.Instance.pokemonUsedInFight.Count);
    }



    ////////////// CALA SEKWENCJA ATAKU OBU POKEMONOWkys negro
    public IEnumerator PokemonFightCutscene(Pokemon firstPokemon, Attack firstAttack, Pokemon secondPokemon, Attack secondAttack, string dialogeSkipTurn = "")
    {
        playerAnimator.SetBool("action", true);
        enemyAnimator.SetBool("action", true);

        dialogeWindow.SetActive(true);

        yield return StartCoroutine(EffectApply(firstPokemon));

        if (dialogeSkipTurn == "")
        {
            var atk1 = firstAttack.getDamage(firstPokemon, secondPokemon);
            Debug.Log(atk1.Item1);
            yield return StartCoroutine(DialogeShow(
                ProvideAttack(firstPokemon, firstAttack, secondPokemon, atk1.Item1, atk1.Item2)));
        }
        else
            yield return StartCoroutine(DialogeShow(dialogeSkipTurn));

        if (secondPokemon.hp != 0)
            yield return StartCoroutine(EffectApply(secondPokemon));

        //////TEST CZY POKEMON MOZE ZAATAKOWAC (ZE NIE JEST MARTWY PO ATAKU)
        if (secondPokemon.hp != 0)
        {

            var atk2 = secondAttack.getDamage(secondPokemon, firstPokemon);
            Debug.Log(atk2.Item1);

            yield return StartCoroutine(DialogeShow(
                ProvideAttack(secondPokemon, secondAttack, firstPokemon, atk2.Item1, atk2.Item2)));
            if(firstPokemon.hp==0)
                AudioManager.Instance.PlayOneShot(FMODEvents.Instance.Death, transform.position);
        }
        else 
        {
            AudioManager.Instance.PlayOneShot(FMODEvents.Instance.Death, transform.position);
        }

        if (secondPokemon == _PokemonEQ.Instance.ActivePokemon || (secondPokemon != _PokemonEQ.Instance.ActivePokemon && secondPokemon.hp != 0))
            dialogeWindow.SetActive(false);

        playerAnimator.SetBool("action", false); 
        enemyAnimator.SetBool("action", false);
        if (_NPCManager.Instance.isItTrainer)
            trainerManager.FinishedBattle = true;
        else
            singleManager.FinishedBattle = true;

    }


    private string ProvideAttack(Pokemon dealingPokemon, Attack dealingAttack, Pokemon targetPokemon , int atkDamage, string atkPower) 
    {
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.punch, transform.position);

        Animator s;
        if (targetPokemon == _PokemonEQ.Instance.ActivePokemon)
            s = playerAnimator;
        else
            s = enemyAnimator;
        if (atkDamage > 0)
        {
            AudioManager.Instance.PlayOneShot(FMODEvents.Instance.hurt, transform.position);
            s.SetTrigger("dmg");
            targetPokemon.hp -= atkDamage;
            string action = CheckKill(targetPokemon);
            return dealingPokemon.PokemonNameOut() + action + targetPokemon.PokemonNameOut() + " with <b>" + atkPower + "</b> " + dealingAttack.attackName + "!";
        }
        else
        {
            AudioManager.Instance.PlayOneShot(FMODEvents.Instance.missed, transform.position);
            return dealingPokemon.PokemonNameOut() + " missed " + targetPokemon.PokemonNameOut() + " with " + dealingAttack.attackName + "!";
        }
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
