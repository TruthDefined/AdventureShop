using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CreateAssetMenu(fileName = "Adventuring Guild", menuName = "ScriptableObjects/Adventuring Guild")]

public class AdventurerGuild: HistoricalEntity
{
    /// <summary>
    /// Adventurer's in Guild
    /// </summary>
    public List<Adventurer> adventurers = new List<Adventurer>();
    /// <summary>
    /// Parties in Guild.
    /// </summary>
    public List<AdventurerParty> parties = new List<AdventurerParty>();
    //public Sprite icon;
    /// <summary>
    /// Unused Guild Gear
    /// </summary>
    public InventoryManager equipment = new InventoryManager();
    /// <summary>
    /// Guild Inventory
    /// </summary>
    public InventoryManager inventory = new InventoryManager();

    public void Init(string name, Location location, List<AdventurerParty> parties, List<Adventurer> adventurers, Sprite crest = null){
        base.Init(name, location);
        this.parties = parties;
        this.adventurers = adventurers;
        if(crest == null){
            //TODO: Make a different set of sprites for guild emblems
            string[] textureFolder = new string[]{$"Assets/Sprites/PartyIcons"};
            string[] guid = AssetDatabase.FindAssets("",textureFolder);
            if( guid.Length > 0 ){
                string path = AssetDatabase.GUIDToAssetPath(guid[Random.Range(0,guid.Length)]);
                this.icon = AssetDatabase.LoadAssetAtPath<Sprite>(path);
                Debug.Log("Crest Set as " + AssetDatabase.LoadAssetAtPath<Sprite>(path).name);
            }
                // if(guid.Length == 1){
                //     string path = AssetDatabase.GUIDToAssetPath(guid[0]);
                //     data.icon = AssetDatabase.LoadAssetAtPath<Sprite>(path);
        }
    }
}
