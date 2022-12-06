using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryController : MonoBehaviour
{

    public GameObject m_slotPrefab;
    public GameObject m_itemPrefab;
    public InventoryType type;
    public GameObject owner;

    private List<GameObject> inventorySlots = new List<GameObject>();
    private void Start() {
        InventoryManager.onInventoryChangedEvent += OnUpdateInventory;
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
                    foreach (InventoryItem item in Singleton.Instance.Player_Equipment_Inventory.inventory)
                    {
                        GameObject slot = CreateInventorySlot();
                        AddItemToSlot(slot, item);
                    }
                }
                break;
            case InventoryType.RawMaterials:
                if(!owner){
                    inventorySlots = new List<GameObject>();
                    foreach (InventoryItem item in Singleton.Instance.Player_Raw_Inventory.inventory)
                    {
                        GameObject slot = CreateInventorySlot();
                        AddItemToSlot(slot, item);
                    }
                }
                break;
            default:
                break;
        }
    }

    private GameObject CreateInventorySlot(){
        GameObject obj = Instantiate(m_slotPrefab);
        obj.transform.SetParent(transform,false);
        inventorySlots.Add(obj);
        obj.name = "Inventory Slot - " + inventorySlots.Count;
        return obj;
    }

    private void AddItemToSlot(GameObject slot, InventoryItem item){
        GameObject obj = Instantiate(m_itemPrefab);
        obj.transform.SetParent(slot.transform,false);
        obj.name = item.data.name ;
        UIInventoryItemContainer container = obj.GetComponent<UIInventoryItemContainer>();
        container.item = item;
    }
}
