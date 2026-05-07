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
    public void ChangePokemon()
    {
        Pokemon playerPokemon = _PokemonEQ.Instance.ActivePokemon;

        Pokemon enemyPokemon = _FightManager.Instance.EnemyPokemon;
        Attack enemyAttack = enemyPokemon.GetRandomAttack();

        StartCoroutine(dialogeManager.PokemonFightCutscene(_PokemonEQ.Instance.ActivePokemon, null, _FightManager.Instance.EnemyPokemon, enemyAttack, true));
    }

}
