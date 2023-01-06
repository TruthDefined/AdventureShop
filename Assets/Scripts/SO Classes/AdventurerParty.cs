using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CreateAssetMenu(fileName = "Adventuring Party", menuName = "ScriptableObjects/Adventuring Party")]

public class AdventurerParty : HistoricalEntity
{
    /// <summary>
    /// Adventurers in party.
    /// </summary>
    public List<Adventurer> adventurers = new List<Adventurer>();
    //public Sprite icon;
    /// <summary>
    /// Unused Party Gear
    /// </summary>
    public InventoryManager equipment = new InventoryManager();
    /// <summary>
    /// Party Inventory
    /// </summary>
    public InventoryManager inventory = new InventoryManager();

    public void Init(string name, Location location, List<Adventurer> adventurers, Sprite crest = null){
        base.Init(name, location);
        this.adventurers = adventurers;
        if(crest == null){
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
