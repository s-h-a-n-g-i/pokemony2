using TMPro;
using UnityEngine;

public class ChangeAttackBtn : MonoBehaviour
{
    [SerializeField] private AttackSwapToNew atkSwap;
    [SerializeField] private TMP_Text attackName;
    [SerializeField] private int attackCounter;
    private Attack attack;
    
    private void OnEnable()
    {
        attack = atkSwap.pokemon.AttacksActive[attackCounter];
        UpdateAttack();
    }

    public void UpdateAttack()
    {
        string s = "None";
        if (attack != null)
            if (attack.attackName != "None")
                s = attack.attackName + " (" + attack.maxPp + "/" + attack.pp + ")";
        attackName.text = s;
    }

}
