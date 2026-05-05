using UnityEngine;

public class _FightManager : MonoBehaviour
{
    public static _FightManager Instance;
    public Pokemon EnemyPokemon;
    public Attack PlayerAttack;
    public Attack EnemyAttack;
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

    public void SetupFight() 
    {
    
    }

}
