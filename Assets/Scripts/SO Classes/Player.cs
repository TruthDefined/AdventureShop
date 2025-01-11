using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Player", menuName = "ScriptableObjects/Player")]
public class Player : HistoricalEntity
{
    public PlayerInventoryManager inventory;
    public PlayerInventoryManager equipment;
    public GameObject artPrefab = null;
    public void Init(string name, Location home, Species species, List<Equipment> startingGear = null, List<RawMaterial> startingMats = null){
        base.Init(name, home);
        this.equipment = new PlayerInventoryManager();
        this.inventory = new PlayerInventoryManager();
        if(startingGear != null){
            foreach(Equipment item in startingGear){
                this.equipment.Add(item, this);
            }
        }
        if(startingMats != null){
            foreach(RawMaterial item in startingMats){
                this.inventory.Add(item, this);
            }
        }
        // if(artPrefab == null) this.artPrefab = new GameObject();
        // this.artPrefab.name = "Creature Art Prefab";
        //Destroy(artPrefab);
        // artPrefab.AddComponent<SpriteRenderer>();
        // artPrefab.GetComponent<SpriteRenderer>().sprite = Sprite.Create(Texture2D.blackTexture,new Rect(0,0,10,10), Vector2.zero);
    }
}