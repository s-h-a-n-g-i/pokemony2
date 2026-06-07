using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    [SerializeField] private PokemonSO[] pokemonDatabase;

    private const int SlotCount = 3;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SaveGameOnSlot(int slot)
    {
        if (!IsValidSlot(slot)) return;
        SaveData data = new SaveData();

        data.male = _PlayerSave.Instance.male;
        data.playerName = _PlayerSave.Instance.playerName;
        data.sceneName = _PlayerSave.Instance._sceneName;
        data.playerPosition = _PlayerSave.Instance._playerPosition;
        data.playerRotation = _PlayerSave.Instance._playerRotation;

        if (_NPCManager.Instance != null)
            data.defeatedNPCs = _NPCManager.Instance.GetDefeatedNPCs();

        if (_PokemonEQ.Instance != null)
        {
            data.eqPokemons = SavePokemonArray(_PokemonEQ.Instance.EqPokemons);
            data.allPokemons = SavePokemonList(_PokemonEQ.Instance.AllHavePokemons);
        }

        //Debug.Log(slot);
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(GetSlotPath(slot), json);

        //Debug.Log("Game saved on slot " + slot);
        Debug.Log(GetSlotPath(slot));
    }

    public string GetPlayerNameFromSlot(int slot) 
    {
        if (!IsValidSlot(slot)) return null;

        string path = GetSlotPath(slot);
        if (!File.Exists(path))
        {
            //Debug.LogWarning("Save slot " + slot + " does not exist.");
            return null;
        }
        string json = File.ReadAllText(path);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        return data.playerName;

    }
    public void LoadGameFromSlot(int slot)
    {
        //Debug.Log(slot);
        if (!IsValidSlot(slot)) return;
        string path = GetSlotPath(slot);
        if (!File.Exists(path))
        {
            //Debug.LogWarning("Save slot " + slot + " does not exist.");
            return;
        }

        string json = File.ReadAllText(path);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        _PlayerSave.Instance.male = data.male;
        _PlayerSave.Instance.playerName = data.playerName;
        _PlayerSave.Instance._sceneName = data.sceneName;
        _PlayerSave.Instance._playerPosition = data.playerPosition;
        _PlayerSave.Instance._playerRotation = data.playerRotation;
        _PlayerSave.Instance.placed = false;

        if (_NPCManager.Instance != null)
            _NPCManager.Instance.SetDefeatedNPCs(data.defeatedNPCs);

        if (_PokemonEQ.Instance != null)
        {
            Dictionary<string, Pokemon> loadedPokemons = new Dictionary<string, Pokemon>();

            _PokemonEQ.Instance.AllHavePokemons = LoadPokemonList(data.allPokemons, loadedPokemons);
            _PokemonEQ.Instance.EqPokemons = LoadPokemonArray(data.eqPokemons, 5, loadedPokemons);

            _PokemonEQ.Instance.ActivePokemon = null;

            foreach (Pokemon pokemon in _PokemonEQ.Instance.EqPokemons)
            {
                if (pokemon != null)
                {
                    _PokemonEQ.Instance.ActivePokemon = pokemon;
                    break;
                }
            }
        }


        //Debug.Log("Game loaded from slot " + slot);
    }

    public bool HasSave(int slot)
    {
        return IsValidSlot(slot) && File.Exists(GetSlotPath(slot));
    }

    public void DeleteSave(int slot)
    {
        if (!IsValidSlot(slot)) return;

        string path = GetSlotPath(slot);
        if (File.Exists(path))
            File.Delete(path);
    }

    private List<PokemonSaveData> SavePokemonArray(Pokemon[] pokemons)
    {
        List<PokemonSaveData> saved = new List<PokemonSaveData>();

        if (pokemons == null)
            return saved;

        foreach (Pokemon pokemon in pokemons)
            saved.Add(PokemonSaveData.FromPokemon(pokemon));

        return saved;
    }

    private List<PokemonSaveData> SavePokemonList(List<Pokemon> pokemons)
    {
        List<PokemonSaveData> saved = new List<PokemonSaveData>();

        if (pokemons == null)
            return saved;

        foreach (Pokemon pokemon in pokemons)
            saved.Add(PokemonSaveData.FromPokemon(pokemon));

        return saved;
    }

    private Pokemon[] LoadPokemonArray(
        List<PokemonSaveData> savedPokemons,
        int size,
        Dictionary<string, Pokemon> loadedPokemons)
    {
        Pokemon[] pokemons = new Pokemon[size];

        if (savedPokemons == null)
            return pokemons;

        for (int i = 0; i < savedPokemons.Count && i < size; i++)
            pokemons[i] = LoadPokemon(savedPokemons[i], loadedPokemons);

        return pokemons;
    }

    private List<Pokemon> LoadPokemonList(
        List<PokemonSaveData> savedPokemons,
        Dictionary<string, Pokemon> loadedPokemons)
    {
        List<Pokemon> pokemons = new List<Pokemon>();

        if (savedPokemons == null)
            return pokemons;

        foreach (PokemonSaveData savedPokemon in savedPokemons)
        {
            Pokemon pokemon = LoadPokemon(savedPokemon, loadedPokemons);
            if (pokemon != null)
                pokemons.Add(pokemon);
        }

        return pokemons;
    }

    private Pokemon LoadPokemon(
        PokemonSaveData data,
        Dictionary<string, Pokemon> loadedPokemons)
    {
        if (data == null || data.isEmpty)
            return null;

        if (string.IsNullOrEmpty(data.uniqueId))
            data.uniqueId = Guid.NewGuid().ToString();

        if (loadedPokemons.TryGetValue(data.uniqueId, out Pokemon existingPokemon))
        {
            ApplyPokemonData(existingPokemon, data);
            return existingPokemon;
        }

        PokemonSO pokemonSO = FindPokemonSO(data.saveId);
        if (pokemonSO == null)
        {
            //Debug.LogWarning("Missing PokemonSO for save id: " + data.saveId);
            return null;
        }

        Pokemon pokemon = new Pokemon(pokemonSO, 0);
        ApplyPokemonData(pokemon, data);

        loadedPokemons.Add(pokemon.uniqueId, pokemon);
        return pokemon;
    }

    private void ApplyPokemonData(Pokemon pokemon, PokemonSaveData data)
    {
        pokemon.saveId = data.saveId;
        pokemon.uniqueId = data.uniqueId;

        pokemon.basicName = data.basicName;
        pokemon.nickname = data.nickname;
        pokemon.evoState = data.evoState;

        pokemon.hp = data.hp;
        pokemon.maxHp = data.maxHp;
        pokemon.atk = data.atk;
        pokemon.def = data.def;
        pokemon.sDef = data.sDef;
        pokemon.sAtk = data.sAtk;
        pokemon.speed = data.speed;
        pokemon.xp = data.xp;
        pokemon.level = data.level;
        pokemon.xpToNextLevel = data.xpToNextLevel;

        pokemon.hpIV = data.hpIV;
        pokemon.atkIV = data.atkIV;
        pokemon.defIV = data.defIV;
        pokemon.sDefIV = data.sDefIV;
        pokemon.sAtkIV = data.sAtkIV;
        pokemon.speedIV = data.speedIV;

        pokemon.atkX = data.atkX;
        pokemon.defX = data.defX;
        pokemon.sDefX = data.sDefX;
        pokemon.sAtkX = data.sAtkX;
        pokemon.speedX = data.speedX;

        pokemon.flying = data.flying;
        pokemon.wasInFight = data.wasInFight;
        pokemon.effects = data.effects;

        for (int i = 0; i < pokemon.AttacksActive.Length; i++)
            pokemon.AttacksActive[i] = null;

        if (data.attacks == null)
            return;

        for (int i = 0; i < pokemon.AttacksActive.Length && i < data.attacks.Count; i++)
            pokemon.AttacksActive[i] = data.attacks[i].ToAttack();
    }

    private PokemonSO FindPokemonSO(string saveId)
    {
        foreach (PokemonSO pokemon in pokemonDatabase)
            if (pokemon != null && pokemon.name == saveId)
                return pokemon;

        return null;
    }

    private string GetSlotPath(int slot)
    {
        return Path.Combine(Application.persistentDataPath, "save-slot-" + slot + ".json");
    }

    private bool IsValidSlot(int slot)
    {
        return slot >= 1 && slot <= SlotCount;
    }
}

[Serializable]
public class SaveData
{
    public bool male;
    public string playerName;
    public string sceneName;
    public Vector3 playerPosition;
    public Vector2 playerRotation;

    public List<string> defeatedNPCs = new List<string>();

    public List<PokemonSaveData> eqPokemons = new List<PokemonSaveData>();
    public List<PokemonSaveData> allPokemons = new List<PokemonSaveData>();
}

[Serializable]
public class PokemonSaveData
{
    public bool isEmpty;

    public string saveId;
    public string uniqueId;

    public string basicName;
    public string nickname;
    public int evoState;

    public int hp;
    public int maxHp;
    public int atk;
    public int def;
    public int sDef;
    public int sAtk;
    public int speed;
    public int xp;
    public int level;
    public int xpToNextLevel;

    public int hpIV;
    public int atkIV;
    public int defIV;
    public int sDefIV;
    public int sAtkIV;
    public int speedIV;

    public int atkX;
    public int defX;
    public int sDefX;
    public int sAtkX;
    public int speedX;

    public bool flying;
    public bool wasInFight;
    public Effects effects;

    public List<AttackSaveData> attacks = new List<AttackSaveData>();

    public static PokemonSaveData FromPokemon(Pokemon pokemon)
    {
        PokemonSaveData data = new PokemonSaveData();

        if (pokemon == null || string.IsNullOrEmpty(pokemon.basicName))
        {
            data.isEmpty = true;
            return data;
        }

        if (string.IsNullOrEmpty(pokemon.uniqueId))
            pokemon.uniqueId = Guid.NewGuid().ToString();

        data.saveId = pokemon.saveId;
        data.uniqueId = pokemon.uniqueId;

        data.basicName = pokemon.basicName;
        data.nickname = pokemon.nickname;
        data.evoState = pokemon.evoState;

        data.hp = pokemon.hp;
        data.maxHp = pokemon.maxHp;
        data.atk = pokemon.atk;
        data.def = pokemon.def;
        data.sDef = pokemon.sDef;
        data.sAtk = pokemon.sAtk;
        data.speed = pokemon.speed;
        data.xp = pokemon.xp;
        data.level = pokemon.level;
        data.xpToNextLevel = pokemon.xpToNextLevel;

        data.hpIV = pokemon.hpIV;
        data.atkIV = pokemon.atkIV;
        data.defIV = pokemon.defIV;
        data.sDefIV = pokemon.sDefIV;
        data.sAtkIV = pokemon.sAtkIV;
        data.speedIV = pokemon.speedIV;

        data.atkX = pokemon.atkX;
        data.defX = pokemon.defX;
        data.sDefX = pokemon.sDefX;
        data.sAtkX = pokemon.sAtkX;
        data.speedX = pokemon.speedX;

        data.flying = pokemon.flying;
        data.wasInFight = pokemon.wasInFight;
        data.effects = pokemon.effects;

        foreach (Attack attack in pokemon.AttacksActive)
            data.attacks.Add(AttackSaveData.FromAttack(attack));

        return data;
    }
}

[Serializable]
public class AttackSaveData
{
    public bool isEmpty;

    public string attackName;
    public PokemonTypes attackType;
    public int pp;
    public int maxPp;
    public int damage;
    public int accuracy;
    public int speed;
    public string desc;
    public Effects effect;

    public static AttackSaveData FromAttack(Attack attack)
    {
        AttackSaveData data = new AttackSaveData();

        if (attack == null)
        {
            data.isEmpty = true;
            return data;
        }

        data.attackName = attack.attackName;
        data.attackType = attack.attackType;
        data.pp = attack.pp;
        data.maxPp = attack.maxPp;
        data.damage = attack.damage;
        data.accuracy = attack.accuracy;
        data.speed = attack.speed;
        data.desc = attack.desc;
        data.effect = attack.effect;

        return data;
    }

    public Attack ToAttack()
    {
        if (isEmpty)
            return null;

        Attack attack = new Attack(attackName, attackType, maxPp, damage, accuracy, speed, desc);
        attack.pp = pp;
        attack.effect = effect;

        return attack;
    }
}