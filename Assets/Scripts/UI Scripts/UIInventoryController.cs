using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryController : UIDataDisplayController
{
    public InventoryType type;
    public GameObject owner;
    private void Start() {
        PlayerInventoryManager.onInventoryChangedEvent += OnUpdateInventory;
        OnUpdateInventory();
    }

    private void OnUpdateInventory(){
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        DrawInventory();
    }


    private void DrawInventory(){
        switch (type){
            case InventoryType.Equipment:
                if(!owner){
                    inventorySlots = new List<GameObject>();
                    foreach (KeyValuePair<string,InventoryItem> item in Singleton.Instance.Player_Equipment_Inventory.inventory)
                    {
                        GameObject slot = CreateInventorySlot();
                        AddItemToSlot(slot, item.Value);
                    }
                }
                break;
            case InventoryType.RawMaterials:
                if(!owner){
                    inventorySlots = new List<GameObject>();
                    foreach (KeyValuePair<string,InventoryItem> item in Singleton.Instance.Player_Raw_Inventory.inventory)
                    {
                        GameObject slot = CreateInventorySlot();
                        AddItemToSlot(slot, item.Value);
                    }
                }
                break;
            default:
                break;
        }
    }
}
