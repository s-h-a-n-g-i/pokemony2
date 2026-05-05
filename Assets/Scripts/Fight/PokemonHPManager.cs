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
    [SerializeField] private TrainerManager trainerManager;

    void Start()
    {
        
    }

    void Update()
    {
        PlayerUpdate();
        if (_NPCManager.Instance.isItTrainer)
            TrainerUpdate();
        else
            SinglePokemonUpdate();
    }

    private void PlayerUpdate() 
    {
        playerHP.text = _PokemonEQ.Instance.ActivePokemon.hp +" / " + _PokemonEQ.Instance.ActivePokemon.maxHp;

        float hpsize = (float)_PokemonEQ.Instance.ActivePokemon.hp / (float)_PokemonEQ.Instance.ActivePokemon.maxHp;
        float colorred = (hpsize * (-1) + 1);

        ImageLineHP_player.gameObject.transform.localScale = new Vector3(hpsize,1,1);
        ImageLineHP_player.color = new Color(colorred, hpsize, 1);
        ImageLineHPaddon_player.color = new Color(colorred, hpsize, 1);

    }
    private void TrainerUpdate() 
    {
        float hpsize = (float)_NPCManager.Instance.TrainerPokemons[trainerManager.chosenPokemon].hp / (float)_NPCManager.Instance.TrainerPokemons[trainerManager.chosenPokemon].maxHp;
        float colorred = (hpsize * (-1) + 1);

        ImageLineHP_enemy.gameObject.transform.localScale = new Vector3(hpsize, 1, 1);
        ImageLineHP_enemy.color = new Color(colorred, hpsize, 1);
        ImageLineHPaddon_enemy.color = new Color(colorred, hpsize, 1);
    }
    private void SinglePokemonUpdate() 
    {
        float hpsize = (float)_FightManager.Instance.EnemyPokemon.hp / (float)_FightManager.Instance.EnemyPokemon.maxHp;
        float colorred = (hpsize * (-1) + 1);

        ImageLineHP_enemy.gameObject.transform.localScale = new Vector3(hpsize, 1, 1);
        ImageLineHP_enemy.color = new Color(colorred,hpsize,1);
        ImageLineHPaddon_enemy.color = new Color(colorred, hpsize, 1);
    }


}
