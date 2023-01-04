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

    public void Init(string name, Location home, Species species, List<Equipment> startingGear = null, List<RawMaterial> startingMats = null){
        base.Init(name, home);
        this.species = species;
        this.equipment = new InventoryManager(InventoryType.Equipment);
        this.inventory = new InventoryManager(InventoryType.RawMaterials);
        if(startingGear != null){
            foreach(Equipment item in startingGear){
            this.equipment.Add(item);
            }
        }
        if(startingMats != null){
            foreach(RawMaterial item in startingMats){
                this.inventory.Add(item);
            }
        }
        if(artPrefab == null) this.artPrefab = new GameObject();
        // artPrefab.AddComponent<SpriteRenderer>();
        // artPrefab.GetComponent<SpriteRenderer>().sprite = Sprite.Create(Texture2D.blackTexture,new Rect(0,0,10,10), Vector2.zero);
    }
}
