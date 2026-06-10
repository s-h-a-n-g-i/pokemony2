using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EscapeButton : MonoBehaviour
{
    public void OnEnable()
    {
        if (_NPCManager.Instance.canescape)
            GetComponent<Button>().interactable = true;
        else
            GetComponent<Button>().interactable = false;
    }

    public void Escape() 
    {
        SceneManager.LoadScene(_PlayerSave.Instance._sceneName);
    }
}
