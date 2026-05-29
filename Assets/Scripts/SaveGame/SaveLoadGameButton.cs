using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadGameButton : MonoBehaviour
{

    [SerializeField] DialogeManager dialoge;
    [SerializeField] private int saveId;
    [SerializeField] private TMP_Text buttontext;
    public void OnEnable()
    {
        if (SaveManager.Instance.HasSave(saveId))
            buttontext.text = SaveManager.Instance.GetPlayerNameFromSlot(saveId);
        else
            buttontext.text = "Slot " + saveId;
    }

    public void Save()
    {
        dialoge.saveChosen = true;
        SaveManager.Instance.SaveGameOnSlot(saveId);
    }
    public void Load()
    {
        SaveManager.Instance.LoadGameFromSlot(saveId);
        _PlayerSave.Instance.placed = false;
        SceneManager.LoadScene(_PlayerSave.Instance._sceneName);
    }
}
