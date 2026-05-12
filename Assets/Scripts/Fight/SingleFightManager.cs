using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SingleFightManager : MonoBehaviour
{
    [Header("Enemy Pokemon")]
    [SerializeField] private Image enemyPokemonImage;
    [SerializeField] private TMP_Text enemyPokemonName;
    private DialogeFightManager dialogeManager;
    public bool FinishedBattle = true;

    void Start()
    {
        GetComponent<TrainerManager>().ResetTrainer() ;
        dialogeManager = GetComponent<DialogeFightManager>();
        if (!_NPCManager.Instance.isItTrainer)
            setUpEnemyPokemon();
    }

    void Update()
    {
        if(FinishedBattle)
            CheckDeadPokemon();
    }

    public void CheckDeadPokemon()
    {
        if (_FightManager.Instance.EnemyPokemon.hp <= 0)
            {
                //Debug.Log("Dead"); 
                FinishedBattle = false;
                dialogeManager.StopAllCoroutines();
                dialogeManager.StartCoroutine(dialogeManager.EndedBattle(_NPCManager.Instance.TrainerName));
                //StartCoroutine();
            }
    }

    private void setUpEnemyPokemon()
    {
        enemyPokemonImage.sprite = _FightManager.Instance.EnemyPokemon.image;
        enemyPokemonName.text = _FightManager.Instance.EnemyPokemon.PokemonNameOut();
    }

    public void Attack(int playerAttackCounter)
    {
        FinishedBattle = false;
        Pokemon enemyPokemon = _FightManager.Instance.EnemyPokemon;
        Attack enemyAttack = enemyPokemon.GetRandomAttack();

        Pokemon playerPokemon = _PokemonEQ.Instance.ActivePokemon;
        Attack playerAttack = playerPokemon.AttacksActive[playerAttackCounter];

        if (playerAttack.howFastAttackIs(playerPokemon) >= enemyAttack.howFastAttackIs(enemyPokemon))
            StartCoroutine(dialogeManager.PokemonFightCutscene(_PokemonEQ.Instance.ActivePokemon, playerPokemon.AttacksActive[playerAttackCounter], _FightManager.Instance.EnemyPokemon, enemyAttack));
        else
            StartCoroutine(dialogeManager.PokemonFightCutscene(_FightManager.Instance.EnemyPokemon, enemyAttack, _PokemonEQ.Instance.ActivePokemon, playerPokemon.AttacksActive[playerAttackCounter]));

    }

    //////////////////////////LAPANIE POKEMONOW!!!!
    public void ChangePokemon()
    {
        FinishedBattle = false;
        Pokemon playerPokemon = _PokemonEQ.Instance.ActivePokemon;

        Pokemon enemyPokemon = _FightManager.Instance.EnemyPokemon;
        Attack enemyAttack = enemyPokemon.GetRandomAttack();

        StartCoroutine(dialogeManager.PokemonFightCutscene(_PokemonEQ.Instance.ActivePokemon, null, _FightManager.Instance.EnemyPokemon, enemyAttack, "Pokemon changed to <b>" + _PokemonEQ.Instance.ActivePokemon.PokemonNameOut() + "</b>"));
    }

    public void CatchPokemonTrue()
    {
        FinishedBattle = false;
        bool appliedPokemon = false;

        for (int i = 0; i < _PokemonEQ.Instance.EqPokemons.Length; i++)
        {
            if (_PokemonEQ.Instance.EqPokemons[i] == null | _PokemonEQ.Instance.EqPokemons[i].basicName==string.Empty)
            {
                appliedPokemon = true;
                _PokemonEQ.Instance.EqPokemons[i] = _FightManager.Instance.EnemyPokemon;
                break;
            }
        }
        if (!appliedPokemon) 
        {
            _PokemonEQ.Instance.AllHavePokemons.Add(_FightManager.Instance.EnemyPokemon);
        }

        dialogeManager.StartCoroutine(dialogeManager.PokemonCaught());
    }
    public void CatchPokemonFalse()
    {
        FinishedBattle = false;
        Pokemon playerPokemon = _PokemonEQ.Instance.ActivePokemon;

        Pokemon enemyPokemon = _FightManager.Instance.EnemyPokemon;
        Attack enemyAttack = enemyPokemon.GetRandomAttack();

        StartCoroutine(dialogeManager.PokemonFightCutscene(_PokemonEQ.Instance.ActivePokemon, null, _FightManager.Instance.EnemyPokemon, enemyAttack, "<b>" + enemyPokemon.basicName + "</b> has escaped from your soul"));
    }



}
