using System;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    public bool placed = true;
    public Vector3 _playerPosition;
    public Vector2 _playerRotation = new Vector2(0, -1);
    public string _sceneName = "";

    GameObject gameManager;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        gameManager = gameObject;
    }

    public static void SaveGameOnSlot(int slot)
    {
        string savePath = "save";

        switch (slot)
        {
            case 1:
                savePath = "save/1.json";
                break;
            case 2:
                savePath = "save/2.json";
                break;
            case 3:
                savePath = "save/3.json";
                break;
        }

        if (!File.Exists(savePath)) 
            File.Create(savePath);

    }

    

}