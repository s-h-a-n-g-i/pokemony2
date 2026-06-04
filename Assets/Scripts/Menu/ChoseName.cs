using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChoseName : MonoBehaviour
{
    [SerializeField] private TMP_InputField input;

    void Update()
    {
        if (input.text.Length > 0)
            GetComponent<Button>().interactable = true;
        else
            GetComponent<Button>().interactable = true;
    }

    public void NameChosenGoNext() 
    {
        _PlayerSave.Instance.playerName = input.text;
        SceneManager.LoadScene("Starter");
    }
}
