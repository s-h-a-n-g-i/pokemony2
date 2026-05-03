using UnityEngine;

public class _FightManager : MonoBehaviour
{
    public static _FightManager Instance;

    public static Attack PlayerAttack;
    public static Attack EnemyAttack;
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

    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
