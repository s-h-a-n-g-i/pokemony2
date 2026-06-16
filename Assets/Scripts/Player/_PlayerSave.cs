using UnityEngine;

public class _PlayerSave : MonoBehaviour
{
    public static _PlayerSave Instance;

    public bool placed = true;
    public bool shoes = false;
    public string playerName = "Shangi";
    public Vector3 _playerPosition;
    public Vector2 _playerRotation = new Vector2(0,-1);
    public string _sceneName = "";
    public bool male = true;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    

}
