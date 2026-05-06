using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FightBtn : MonoBehaviour
{
    [SerializeField] private TMP_Text AttackNameText;
    [SerializeField] private int attackCounter;
    [SerializeField] private FightSystemManager fightManager;

    private Button btn;

    private void Start()
    {
        btn = GetComponent<Button>();    
    }

    void Update()
    {
        UpdateAttack(_PokemonEQ.Instance.ActivePokemon.AttacksActive[attackCounter]);
    }


    public void UpdateAttack(Attack attack)
    {
        if (attack != null)
            if (attack.attackName != "None")
            {
                btn.interactable = true;
                AttackNameText.text = attack.attackName + " (" + attack.maxPp + "/" + attack.pp + ")";
            } 
            else
            {
                btn.interactable = false;
                AttackNameText.text = "None";
            }
        else
        {
            btn.interactable = false;
            AttackNameText.text = "None";
        }


    }

    public void AttackUse()
    {
        fightManager.Attacking(attackCounter);
    }

}
