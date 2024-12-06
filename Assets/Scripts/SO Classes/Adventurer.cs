using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Adventurer", menuName = "ScriptableObjects/Adventurer")]

public class Adventurer : Creature
{
    public AdventurerClass adventurerClass;
    //public AdventurerParty currentParty;

    public void Init(string name, Location home, Species species, List<Equipment> gear, AdventurerClass adventurerClass){
        base.Init(name, home, species, gear);
        this.adventurerClass = adventurerClass;
    }

}