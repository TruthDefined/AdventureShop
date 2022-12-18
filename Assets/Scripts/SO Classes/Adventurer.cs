using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Adventurer", menuName = "ScriptableObjects/Adventurer")]

public class Adventurer : Creature
{
    public AdventurerClass adventurerClass;

    public void Init(string name, Location home, Species species, Equipment[] startingGear, AdventurerClass adventurerClass){
        base.Init(name, home, species, startingGear);
        this.adventurerClass = adventurerClass;
    }
}