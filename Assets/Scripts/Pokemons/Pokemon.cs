using JetBrains.Annotations;
using UnityEngine;
public enum PokemonTypes
{
    nothing,
    Angel,
    FallenAngel,
    Demon,
    Seraph,
    Ghost,
    Soulless,
    Nefilim
}
public enum Effects
{
    None,
    Blind,
    Burn,
    Poison,
    Slow,
    Weakness,
    Buff
}

public class Pokemon
{
    [SerializeField] private int pokemonLevelStarted;

    [Header("Info")]
    public string basicName;
    public string nickname;

    public int evoState = 0;

    public Sprite image;

    public PokemonTypes type;
    public PokemonTypes type2;

    [Header("Evolution 1")]
    public Sprite E1image;


    public PokemonTypes E1type;
    public PokemonTypes E1type2;

    [Header("Evolution 2")]
    public Sprite E2image;

    public PokemonTypes E2type;
    public PokemonTypes E2type2;

    public Attacks[] attacks = new Attacks[4];

    [Header("Basic Stats")]
    public int hp;
    public int atk;
    public int def;
    public int sDef;
    public int sAtk;
    public int speed;
    public int xp;

    [Header("IV")]
    public int hpIV;
    public int atkIV;
    public int defIV;
    public int sDefIV;
    public int sAtkIV;
    public int speedIV;

    [Header("Modifiers")]

    public int atkX;
    public int defX;
    public int sDefX;
    public int sAtkX;
    public int speedX;

    public int level;

    [HideInInspector] public int xpToNextLevel;
    [HideInInspector] public bool wasInFight = false;

    public Effects effects;


    private void PokemonSpawnLevelUp(int level) 
    {
        for (int i = 0; i < level; i++) 
        {
            LvLUp();
        }
    }

    public void CreatePokemon(Creatures c, int startingLevel = 0) 
    {
        hp = c.hp;
        atk = c.atk;
        def = c.def;
        sDef = c.sDef;
        sAtk = c.sAtk;
        speed = c.speed;
        xp = 0;

        hpIV = c.hpIV + +Random.Range(1, 4);
        atkIV = c.atkIV + +Random.Range(1, 4);
        defIV = c.defIV + +Random.Range(1, 4);
        sDefIV = c.sDefIV + +Random.Range(1, 4);
        sAtkIV = c.sAtkIV + +Random.Range(1, 4);
        speedIV = c.speedIV + Random.Range(1, 4);

        image = c.image;
        basicName = c.basicName;

        attacks = c.attacks;

        level = startingLevel;

        //PokemonSpawnLevelUp(level);

}


    public void LvLUp() 
    {
        xp = 0;
        xpToNextLevel = xpToNextLevel / 2 + xpToNextLevel;
        StatsUp();
    }

    public void Evolution() 
    {
        StatsUp();
        StatsUp();
        evoState++;
    }

    private void StatsUp()
    {
        hp += hpIV;
        atk += atkIV;
        def += defIV;
        sDef += sDefIV;
        sAtk += sAtkIV;
        speed += speedIV;
    }

}
