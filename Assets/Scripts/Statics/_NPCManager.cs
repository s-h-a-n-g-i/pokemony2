using System.Collections.Generic;
using UnityEngine;

public class _NPCManager : MonoBehaviour
{
    public static _NPCManager Instance;

    public string NPCInBattle;
    public int TrainerChosenPokemon = 0;
    public Pokemon[] TrainerPokemons = new Pokemon[5];
    public bool isItTrainer = false;


    private HashSet<string> defeatedNPCs = new HashSet<string>();

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

    public void MarkDefeated(string npcId)
    {
        defeatedNPCs.Add(npcId);
    }

    public bool IsDefeated(string npcId)
    {
        return defeatedNPCs.Contains(npcId);
    }

}
