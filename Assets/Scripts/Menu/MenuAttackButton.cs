using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuAttackButton : MonoBehaviour
{
    [SerializeField] private PokemonCheck desc;
    [SerializeField] private TMP_Text buttonText;
    [SerializeField] private int attackId;
    private void OnEnable()
    {
        buttonText.text = desc.IsThisAttackAviable(attackId);
        if (desc.IsThisAttackAviable(attackId) == "No Attack")
            GetComponent<Button>().interactable = false;
        else
            GetComponent<Button>().interactable = true;

    }

    public void OnButtonPressed() 
    {
        desc.Attack(attackId);
    }
}
