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

[CreateAssetMenu(menuName = "ScriptableObjects/Creatures")]
public class Creatures : ScriptableObject
{
    [Header("Info")]
    public string basicName;
    public string nickname;

    public Sprite image;
    public Sprite icon;

    public PokemonTypes type;
    public PokemonTypes type2;

    public Attacks[] attacks = new Attacks[4];

    public Effects effects;
    [Header("Basic Stats")]
    public int hp;
    public int atk;
    public int def;
    public int sDef;
    public int sAtk;
    public int speed;
    public int xp;

    [Header("Modifiers")]

    public int atkX;
    public int defX;
    public int sDefX;
    public int sAtkX;
    public int speedX;

    [HideInInspector] public int xpToNextLevel;
}
