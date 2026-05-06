using UnityEngine;

public class PlayerSave : MonoBehaviour
{
    public static PlayerSave Instance;

    public bool placed = true;
    public Vector3 _playerPosition;
    public Vector2 _playerRotation = new Vector2(0,-1);
    public string _sceneName = "";
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
