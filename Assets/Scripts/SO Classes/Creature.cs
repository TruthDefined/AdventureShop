using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Creature", menuName = "ScriptableObjects/Creature")]
public class Creature : HistoricalEntity
{
    public Species species;
    public InventoryManager inventory;
    public InventoryManager equipment;
    public GameObject artPrefab = null;

    //TODO: Figure out how we're going to do stats in this game???
    public int[] stats;

    public void Init(string name, DataEntity home, Species species, Equipment[] startingGear = null, RawMaterial[] startingMats = null){
        base.Init(name, home);
        this.species = species;
        foreach(Equipment item in startingGear){
            this.equipment.Add(item);
        }
        foreach(RawMaterial item in startingMats){
            this.inventory.Add(item);
        }
    }
}
