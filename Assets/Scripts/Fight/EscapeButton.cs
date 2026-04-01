using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeButton : MonoBehaviour
{
    public void Escape() 
    {
        SceneManager.LoadScene(PlayerSave._sceneName);
    }
}
