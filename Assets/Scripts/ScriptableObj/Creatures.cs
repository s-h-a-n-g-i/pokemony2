using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "ScriptableObjects/Creatures")]
public class Creatures : ScriptableObject
{
    
    [Header("Info")]
    public string basicName;

    public Sprite image;
    public Sprite E1image;
    public Sprite E2image;

    public PokemonTypes type;
    public PokemonTypes type2;


    public PokemonTypes E1type;
    public PokemonTypes E1type2;


    public PokemonTypes E2type;
    public PokemonTypes E2type2;

    public Attacks[] attacks = new Attacks[4];

    public Effects effects;
    [Header("Basic Stats")]
    public int hp;
    public int atk;
    public int def;
    public int sDef;
    public int sAtk;
    public int speed;

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

    public Attacks[] attacksOnLevelUps;
    public int[] attacksOnLevels;
}
