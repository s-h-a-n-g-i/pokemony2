using UnityEngine;
using UnityEngine.UI;

public class ChoseMaleOrFemale : MonoBehaviour
{
    [SerializeField] ChoseMaleOrFemale buttonConfirm;
    [SerializeField] GameObject thisOneHide;
    [SerializeField] GameObject thisOneShow;

    [HideInInspector] public bool chosen = false;
    public void ChoseMale()
    {
        _PlayerSave.Instance.male = true;
        buttonConfirm.chosen = true;
    }
    public void ChoseFemale()
    {
        _PlayerSave.Instance.male = false;
        buttonConfirm.chosen = true;
    }

    private void Update()
    {
        if (buttonConfirm == null)
            GetComponent<Button>().interactable = chosen;
    }

    public void ChosenGoNext()
    {
        thisOneHide.SetActive(false);
        thisOneShow.SetActive(true);
    }
}
