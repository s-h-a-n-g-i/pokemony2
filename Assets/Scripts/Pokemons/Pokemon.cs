using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;
public enum PokemonTypes
{
    nothing,
    Angel,
    Archangel,
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
[System.Serializable]
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

    public string basicNameEvo1;
    public Sprite E1image;
    public int evoLevel1;

    public PokemonTypes E1type;
    public PokemonTypes E1type2;

    [Header("Evolution 2")]

    public string basicNameEvo2;
    public Sprite E2image;
    public int evoLevel2;

    public PokemonTypes E2type;
    public PokemonTypes E2type2;

    public Attack[] AttacksActive = new Attack[4];

    [Header("Basic Stats")]
    public int hp;
    public int maxHp;
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

    public int level = 0;

    public bool flying;

    [HideInInspector] public int xpToNextLevel;
    [HideInInspector] public bool wasInFight = false;

    public Effects effects;

    private Dictionary<int,Attack> AttackDict = new Dictionary<int,Attack>();

    public Pokemon(PokemonSO c, int startingLevel = 0) 
    {
        maxHp = c.hp;
        hp = c.hp;
        atk = c.atk;
        def = c.def;
        sDef = c.sDef;
        sAtk = c.sAtk;
        speed = c.speed;
        xp = 0;
        xpToNextLevel = 4;

        image = c.image;
        basicName = c.basicName;
        nickname = c.basicName;

        type = c.type;
        type2 = c.type2;

        basicNameEvo1 = c.basicNameEvo1;
        evoLevel1 = c.evoLevel1;
        E1image = c.E1image;
        E1type = c.E1type;
        E1type = c.E1type2;


        basicNameEvo2 = c.basicNameEvo2;
        evoLevel2 = c.evoLevel2;
        E2image = c.E2image;
        E2type = c.E2type;
        E2type = c.E2type2;

        hpIV = c.hpIV + +Random.Range(-1, 1);
        atkIV = c.atkIV + +Random.Range(-1, 1);
        defIV = c.defIV + +Random.Range(-1, 1);
        sDefIV = c.sDefIV + +Random.Range(-1, 1);
        sAtkIV = c.sAtkIV + +Random.Range(-1, 1);
        speedIV = c.speedIV + Random.Range(-1, 1);


        flying = c.flying;

        ApplyAttacksToClass(c.attacks);

        for (int i = 0; i < startingLevel; i++)
        {
            LvLUp();
        }
        if(c.attacksOnLevelUp != null)
            for (int i = 0; i < c.attacksOnLevelUp.Length;i++) 
            {
                AttackSO s = c.attacksOnLevelUp[i].attack;
                AttackDict.Add(
                    c.attacksOnLevelUp[i].level,
                    new Attack(s.name, s.attackType, s.maxPp, s.damage, s.accuracy, s.speed, s.desc));

            }
        
    }


    private void ApplyAttacksToClass(AttackSO[] s) 
    {
        for (int i = 0; i < s.Length; i++) 
        {
            if (s[i] != null) 
                if(s[i].name != "None")
                    AttacksActive[i] = new Attack(s[i].name, s[i].attackType, s[i].maxPp, s[i].damage, s[i].accuracy, s[i].speed, s[i].desc);
        }
    }

    //////////////ALL ABOUT LEVEL 
    
    public bool CheckForLevelUp() 
    {
        if (xp < xpToNextLevel) return false;
        return true;
    }
    public bool CheckForEvolution()
    {
        if (level == evoLevel1 || level == evoLevel2) return true;
        return false;
    }
    public Attack CheckForAttacksAdded() 
    {
        if (AttackDict.TryGetValue(level, out Attack s)) 
        {
            for (int i = 0; i < 4; i++) 
            {
                if (AttacksActive[i] == null)
                {
                    AttacksActive[i] = s;
                    return s;
                }
            };
            s.hasSlot = false;
            return s;
        }
        return null;
    }

    public void LvLUp() 
    {
        xp -= xpToNextLevel;
        if(xp<0) xp = 0;
        xpToNextLevel = xpToNextLevel / 2 + xpToNextLevel;
        level++;
        StatsUp();
    }

    public void Evolution() 
    {
        StatsUp();
        IVsUp();
        evoState++;
        switch (evoState)
        {
            case 1:
                if (nickname == basicName)
                    nickname = basicNameEvo1;
                basicName = basicNameEvo1;
                image = E1image;
                break;
            case 2:
                basicName = basicNameEvo2;
                if (nickname == basicNameEvo1)
                    nickname = basicNameEvo2;
                image = E2image;
                break;
        }
    }

    private void StatsUp()
    {
        maxHp += hpIV;
        atk += atkIV;
        def += defIV;
        sDef += sDefIV;
        sAtk += sAtkIV;
        speed += speedIV;
        hp = maxHp;
    }

    private void IVsUp()
    {
        hpIV ++;
        atkIV ++;
        defIV ++;
        sDefIV ++;
        sAtkIV ++;
        speedIV ++;
    }

    public void giveXP(int enemyPokemonLevel) 
    {
        float s = (float)enemyPokemonLevel / 2f*3f;
        if (s <= 2) 
        {
            s = 2;
        }
        xp += (int)s;
    }
    /////////////BASIC THINGS
    public string PokemonNameOut() 
    {
        string n;
        n = basicName;
        if (nickname != null) n = nickname;
        n += " (" + level + ")";
        
        return n;
    }
    public PokemonTypes[] TypesOfPokemon()
    {
        if (evoState == 1)
            return new PokemonTypes[] { E1type, E1type2 };
        else if (evoState == 2)
            return new PokemonTypes[] { E2type, E2type2 };
        else
            return new PokemonTypes[] { type, type2 };
    }



    public Attack GetRandomAttack()
    {
        List<Attack> attackAvaible = new List<Attack>();

        for (int i = 0; i < AttacksActive.Count(); i++)
            if (AttacksActive[i] != null)
                if (AttacksActive[i].attackName != "None") 
                {
                    attackAvaible.Add(AttacksActive[i]);
                }


        return attackAvaible[Random.Range(0, attackAvaible.Count)];

    }

    public bool checkHit(Attack atk) 
    {
        int roll = Random.Range(0,100);

        if (atk.accuracy > roll) return false;

        return true;
        
    }


    public bool catchCheck() 
    {
        int s = UnityEngine.Random.Range(0,maxHp);

        if (s>=hp) 
            return true;
        return false;
    }



    public string GetPokemonTypes() 
    {
        switch (evoState) 
        {
            case 0:
                return type + " " + type2;
            case 1:
                return E1type + " " + E1type2;
            case 2:
                return E2type + " " + E2type2;

        }
        return "";
    }

}



////////////////////////KALKULATOR//////////////////////////


