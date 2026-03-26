using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PokemonHPManager : MonoBehaviour
{
    [Header("Player Pokemon")]
    [SerializeField] private Image ImageLineHP_player;
    [SerializeField] private Image ImageLineHPaddon_player;
    [SerializeField] private TMP_Text playerHP;

    [Header("Enemy Pokemon")]
    [SerializeField] private Image ImageLineHP_enemy;
    [SerializeField] private Image ImageLineHPaddon_enemy;


    [Header("Settings")]
    [SerializeField] private PokemonInFightSO battle;
    [SerializeField] private CreatureEq player;
    [SerializeField] private TrainerManager trainerManager;

    void Start()
    {
        
    }

    void Update()
    {
        PlayerUpdate();
        if (battle.isThisTrainer)
            TrainerUpdate();
        else
            SinglePokemonUpdate();
    }

    private void PlayerUpdate() 
    {
        playerHP.text = player.ActivePokemon.maxHp +" / " + player.ActivePokemon.hp;

        float hpsize = (float)player.ActivePokemon.hp / (float)player.ActivePokemon.maxHp;
        float colorred = (hpsize * (-1) + 1);

        ImageLineHP_player.gameObject.transform.localScale = new Vector3(hpsize,1,1);
        ImageLineHP_player.color = new Color(colorred, hpsize, 1);
        ImageLineHPaddon_player.color = new Color(colorred, hpsize, 1);

    }
    private void TrainerUpdate() 
    {
        float hpsize = (float)battle.pokemonsToBattleTrainer[trainerManager.chosenPokemon].hp / (float)battle.pokemonsToBattleTrainer[trainerManager.chosenPokemon].maxHp;
        float colorred = (hpsize * (-1) + 1);

        ImageLineHP_enemy.gameObject.transform.localScale = new Vector3(hpsize, 1, 1);
        ImageLineHP_enemy.color = new Color(colorred, hpsize, 1);
        ImageLineHPaddon_enemy.color = new Color(colorred, hpsize, 1);
    }
    private void SinglePokemonUpdate() 
    {
        float hpsize = (float)battle.pokemonToBattle.hp / (float)battle.pokemonToBattle.maxHp;
        float colorred = (hpsize * (-1) + 1);

        ImageLineHP_enemy.gameObject.transform.localScale = new Vector3(hpsize, 1, 1);
        ImageLineHP_enemy.color = new Color(colorred,hpsize,1);
        ImageLineHPaddon_enemy.color = new Color(colorred, hpsize, 1);
    }


}
