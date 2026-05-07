using System.Collections.Generic;
using UnityEngine;

public class Attack
{
    public string attackName;
    public PokemonTypes attackType;

    public int pp;
    public int maxPp;
    public int damage;
    public int accuracy;
    public int speed;
    public string desc;

    public Effects effect;

    bool isSuper = false;

    public bool hasSlot = true;
    public Attack(string attackName, PokemonTypes attackType, int maxPp, int damage, int accuracy, int speed , string desc) 
    {
        this.attackName = attackName;
        this.attackType = attackType;
        this.maxPp = maxPp;
        this.pp = maxPp;
        this.damage = damage;
        this.accuracy = accuracy;
        this.speed = speed;
        this.desc = desc;

    }


    public int howFastAttackIs(Pokemon s) 
    {
        int r = speed*2;
        r += s.speed;
        return r;
    }

    public (int,string) getDamage(Pokemon deal, Pokemon target) 
    {
        int r;
        if (!isSuper)
            r = damage + deal.atk - target.def;
        else
            r = damage + deal.sAtk - target.sDef;
        if (r < 0) 
            if(isSuper)
                r = 10;
            else
                r = 1;
        float multiplier = DamageMultiplier(target);
        float dmgTemp = r * multiplier;
        string output = "";
        switch (multiplier)
        {
            case 0.5f:
                output = "(No Effective)";
                break;
            case 0.75f:
                output = "(Less Effective)";
                break;
            case 1f:
                output = "(Normal)";
                break;
            case 1.5f:
                output = "(Effective)";
                break;
            case 2f:
                output = "(SUPER Effective)";
                break;
        }

        return (Mathf.CeilToInt(dmgTemp), output);
    }


    private float DamageMultiplier(Pokemon target) 
    {
        if(attackType == PokemonTypes.nothing || attackType == PokemonTypes.Nefilim)
            return 1;

        float s = 1;
        PokemonTypes poketype1 = target.type;
        PokemonTypes poketype2 = target.type2;
        Debug.Log(target.basicName + " " + poketype1 + " " + poketype2);
        Dictionary<PokemonTypes,float> dict = new Dictionary<PokemonTypes,float>();
        switch (target.evoState)
        {
            case 1:
                poketype1 = target.E1type;
                poketype2 = target.E1type2;
                break;
            case 2:
                poketype1 = target.E2type;
                poketype2 = target.E2type2;
                break;
        }
        switch (attackType)
        {
            case PokemonTypes.Angel:
                dict = _GlobalPokemon.CALC_AngelDamage;
                break;

            case PokemonTypes.Demon:
                dict = _GlobalPokemon.CALC_DemonDamage;
                break;

            case PokemonTypes.FallenAngel:
                dict = _GlobalPokemon.CALC_FallenAngelDamage;
                break;

            case PokemonTypes.Archangel:
                dict = _GlobalPokemon.CALC_ArchangelDamage;
                break;

            case PokemonTypes.Seraph:
                dict = _GlobalPokemon.CALC_SeraphDamage;
                break;

            case PokemonTypes.Ghost:
                dict = _GlobalPokemon.CALC_GhostDamage;
                break;
        }

        if (poketype1 == PokemonTypes.nothing && poketype2 == PokemonTypes.nothing)
        {
            Debug.Log("normal?");
            return 1;
        }
        else if (poketype1 == PokemonTypes.nothing)
            s = dict[poketype2];
        else if (poketype2 == PokemonTypes.nothing)
            s = dict[poketype1];
        else
            s = dict[poketype1] > dict[poketype2] ? dict[poketype1] : dict[poketype2];
        
        Debug.Log(s);
        return s;
    }


}
