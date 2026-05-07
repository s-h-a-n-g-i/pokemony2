using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AttackSwapToNew : MonoBehaviour
{
    [SerializeField] GameObject wholeObject;
    [SerializeField] TMP_Text text;

    [HideInInspector] public Pokemon pokemon;
    private Attack atkToChange;
    private DialogeFightManager dialogeFightManager;

    private void Start()
    {
        wholeObject.SetActive(false);
        dialogeFightManager = GetComponent<DialogeFightManager>();
    }

    public void FullyAttackSwap(Pokemon p,Attack atk) 
    {
        pokemon = p;
        atkToChange = atk;
        wholeObject.SetActive(true);
        text.text = "New attack to learn: <b>" + atk.attackName + "</b>";
    }

    public void ForgetAttack() 
    {
        SceneManager.LoadScene(PlayerSave.Instance._sceneName);
    }

    public void ChangeAttack(int attackChangedCounter) 
    {
        pokemon.AttacksActive[attackChangedCounter] = atkToChange;
        wholeObject.SetActive(false);
        dialogeFightManager.ChosenNewAttack = true;
    }

    public void ForgottenAttack()
    {
        wholeObject.SetActive(false);
        dialogeFightManager.ChosenNewAttack = true;
    }
}
