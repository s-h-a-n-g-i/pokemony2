using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadMenu : MonoBehaviour
{
    public void ChangeScene(string sceneName) 
    {
        SceneManager.LoadScene(sceneName);
    }
}
