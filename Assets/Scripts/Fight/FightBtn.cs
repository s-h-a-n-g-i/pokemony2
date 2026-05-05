using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FightBtn : MonoBehaviour
{
    [SerializeField] private TMP_Text AttackNameText;
    [SerializeField] private int attackCounter;
    [SerializeField] private FightSystemManager fightManager;
    


    void Update()
    {
        UpdateAttack(_PokemonEQ.Instance.ActivePokemon.AttacksActive[attackCounter]);
    }


    public void UpdateAttack(Attack attack)
    {
        AttackNameText.text = attack.attackName + " (" + attack.maxPp + "/" +attack.pp + ")";
        
    }

    public void AttackUse()
    {
        fightManager.Attacking(attackCounter);
    }

}
