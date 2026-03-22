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

    public int getDamage(Pokemon s) 
    {
        int r = damage + s.atk;
        return r;
    }

}
