using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveLoadGameButton : MonoBehaviour
{

    [SerializeField] DialogeManager dialoge;
    [SerializeField] private bool loadgame = false; 
    [SerializeField] private int saveId;
    [SerializeField] private TMP_Text buttontext;
    public void OnEnable()
    {
        //Debug.Log("saveslot: " + saveId);
        if (SaveManager.Instance.HasSave(saveId))
        {
            buttontext.text = SaveManager.Instance.GetPlayerNameFromSlot(saveId);
            if (loadgame) GetComponent<Button>().interactable = true;
        }
        else
        {
            buttontext.text = "Empty slot " + saveId;
            if(loadgame) GetComponent<Button>().interactable = false;
        }
    }

    public void Save()
    {
        dialoge.saveChosen = true;
        Debug.Log("saveslot: "+ saveId);
        SaveManager.Instance.SaveGameOnSlot(saveId);
    }
    public void Load()
    {
        SaveManager.Instance.LoadGameFromSlot(saveId);
        Debug.Log("saveslot: " + saveId);
        _PlayerSave.Instance.placed = false;
        SceneManager.LoadScene(_PlayerSave.Instance._sceneName);
    }
}
