using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/CreatureEq")]
public class CreatureEq : ScriptableObject
{
    public List<Pokemon> creaturesCaptured;
    public Pokemon[] Equipped = new Pokemon[5];
}
