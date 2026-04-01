using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;
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

    public int level;

    public bool flying;

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

        type = c.type;
        type2 = c.type2;

        E1type = c.E1type;
        E1type = c.E1type2;

        E2type = c.E2type;
        E2type = c.E2type2;

        hpIV = c.hpIV + +Random.Range(-1, 1);
        atkIV = c.atkIV + +Random.Range(-1, 1);
        defIV = c.defIV + +Random.Range(-1, 1);
        sDefIV = c.sDefIV + +Random.Range(-1, 1);
        sAtkIV = c.sAtkIV + +Random.Range(-1, 1);
        speedIV = c.speedIV + Random.Range(-1, 1);

        image = c.image;
        basicName = c.basicName;
        basicNameEvo1 = c.basicNameEvo1;
        basicNameEvo2 = c.basicNameEvo2;

        flying = c.flying;

        ApplyAttacksToClass(c.attacks);

        level = startingLevel;

        PokemonSpawnLevelUp(level);

}

    private void ApplyAttacksToClass(AttackSO[] s) 
    {
        for (int i = 0; i < s.Length; i++) 
        {
            if (s[i] != null || s[i].name != "None") 
            {
                AttacksActive[i] = new Attack(s[i].name, s[i].attackType, s[i].maxPp, s[i].damage, s[i].accuracy, s[i].speed, s[i].desc);
            }
        }
    }


    public void LvLUp() 
    {
        xp = 0;
        xpToNextLevel = xpToNextLevel / 2 + xpToNextLevel;
        StatsUp();
        if (level == evoLevel1 || level == evoLevel2) Evolution();
    }

    public void Evolution() 
    {
        StatsUp();
        IVsUp();
        evoState++;
        if (nickname != null || nickname != "") 
            switch (evoState)
            {
                case 1:
                    basicName = basicNameEvo1;
                    break;
                case 2:
                    basicName = basicNameEvo2;
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

    public int giveXP(int enemyPokemonLevel) 
    {
        return enemyPokemonLevel / 2*3;
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

    public string PokemonNameOut() 
    {
        string n;
        n = basicName;
        if (nickname != null) n = nickname;
        n += " (" + level + ")";
        
        return n;
    }

    public Attack GetRandomAttack() 
    {
        List<Attack> attacks = new List<Attack>();
        foreach (Attack attack in AttacksActive)
            attacks.Add(attack);

        for (int i = 0; i < attacks.Count; i++) 
            if (attacks[i].attackName == "None" || attacks[i] == null) attacks.Remove(attacks[i]);


        Attack a = attacks[Random.Range(0, attacks.Count)];
        
        return a;

    }

    public bool checkHit(Attack atk) 
    {
        int roll = Random.Range(0,100);

        if (atk.accuracy > roll) return false;

        return true;
        //hp -= atk.damage;
        
        //if (hp <= 0) 
        //{
        //    pokemonDead();
        //}
    }


    private void pokemonDead() 
    {
        
    }
}



////////////////////////KALKULATOR//////////////////////////


