using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton Instance { get; private set; }
    public InventoryManager Player_Raw_Inventory { get; private set; }
    public InventoryManager Player_Equipment_Inventory {get; private set;}
    public UIManager UIManager { get; private set; }
    public CraftingManager CraftingManager {get; private set;}
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        //If UI Manager is setup on Gameobject, grab it. Else, make a new one
        if(GetComponent<UIManager>()){
            UIManager = GetComponent<UIManager>();
        }
        else{
            UIManager = gameObject.AddComponent<UIManager>();
        }

        if(GetComponent<CraftingManager>()){
            CraftingManager = GetComponent<CraftingManager>();
        }
        else{
            CraftingManager = gameObject.AddComponent<CraftingManager>();
        }

        Player_Raw_Inventory = gameObject.AddComponent<InventoryManager>().SetType(InventoryType.RawMaterials);
        Player_Equipment_Inventory = gameObject.AddComponent<InventoryManager>().SetType(InventoryType.Equipment);
    }
    private void Start() {
        print(Player_Raw_Inventory.type);
        foreach(RawMaterial mat in CraftingManager.rawMaterials){
            Player_Raw_Inventory.Add(mat);
        }
    }
}
