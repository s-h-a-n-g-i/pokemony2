using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "ScriptableObjects/PokemonSO")]
public class PokemonSO : ScriptableObject
{
    
    [Header("Info")]
    public string basicName;
    public bool flying;

    public Sprite image;

    public PokemonTypes type;
    public PokemonTypes type2;


    [Header("1st Evolution")]
    public string basicNameEvo1;
    public Sprite E1image;
    public int evoLevel1;
    public PokemonTypes E1type;
    public PokemonTypes E1type2;


    [Header("2nd Evolution")]
    public string basicNameEvo2;
    public Sprite E2image;
    public int evoLevel2;
    public PokemonTypes E2type;
    public PokemonTypes E2type2;

    public AttackSO[] attacks = new AttackSO[4];

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

    public AttacksLevelups[] attacksOnLevelUp;

    //public AttackSO[] attacksOnLevelUps;
    //public int[] attacksOnLevels;
}
