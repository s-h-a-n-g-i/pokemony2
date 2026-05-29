using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeButton : MonoBehaviour
{
    public void Escape() 
    {
        SceneManager.LoadScene(_PlayerSave.Instance._sceneName);
    }
}
