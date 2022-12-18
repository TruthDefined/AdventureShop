using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Adventuring Party", menuName = "ScriptableObjects/Adventuring Party")]

public class AdventurerParty : HistoricalEntity
{
    /// <summary>
    /// Adventurers in party.
    /// </summary>
    public List<Adventurer> adventurers = new List<Adventurer>();
    public Sprite crest;
    /// <summary>
    /// Unused Party Gear
    /// </summary>
    public InventoryManager equipment;
    /// <summary>
    /// Party Inventory
    /// </summary>
    public InventoryManager inventory;

    public void Init(string name, Location location, List<Adventurer> adventurers, Sprite crest = null){
        base.Init(name, location);
        this.adventurers = adventurers;
        this.crest = crest;
    }
    
}
