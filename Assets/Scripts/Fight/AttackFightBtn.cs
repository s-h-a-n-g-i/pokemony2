using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AttackFightBtn : MonoBehaviour
{
    [SerializeField] private TMP_Text AttackNameText;
    [SerializeField] private int attackCounter;
    [SerializeField] private FightSystemManager fightManager;
    private Attack atk;
    private Button btn;

    private void Start()
    {
        atk = _PokemonEQ.Instance.ActivePokemon.AttacksActive[attackCounter];
        btn = GetComponent<Button>();    
    }

    void Update()
    {
        UpdateAttack(atk);
    }


    public void UpdateAttack(Attack attack)
    {
        string s = "None";
        bool canpress = false;
        if (attack != null)
            if (attack.attackName != "None" )
            {
                if (attack.pp > 0)
                {
                    canpress = true;
                }
                else 
                {
                    canpress = false;
                }
                s = attack.attackName + " (" + attack.pp + "/" + attack.maxPp + ")";
            }
        btn.interactable = canpress;
        AttackNameText.text = s;
    }

    public void AttackUse()
    {
        atk.pp--;
        fightManager.Attacking(attackCounter);
    }

}
