using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDataDisplayController : MonoBehaviour
{
    public LoadTestParty parent;
    public GameObject m_slotPrefab;
    public GameObject m_itemPrefab;
    private List<GameObject> _inventorySlots = new List<GameObject>();
    public List<GameObject> inventorySlots{
        get{
            return _inventorySlots;
        }
        set{
            _inventorySlots = value;
        }
    }

    private void Start() {
        _inventorySlots = new List<GameObject>();
        parent = GetComponentInParent<LoadTestParty>();
        //CreateInventorySlot();
    }

    /// <summary>
    /// Create Inventory Slot for Draggable UI Icon Items
    /// </summary>
    /// <returns></returns>
     public GameObject CreateInventorySlot(){
        GameObject obj = Instantiate(m_slotPrefab);
        obj.transform.SetParent(transform,false);
        _inventorySlots.Add(obj);
        obj.name = "Inventory Slot - " + _inventorySlots.Count;
        return obj;
    }
    /// <summary>
    /// Add InventoryItem Draggable UI Icon Item to the newly created slot
    /// </summary>
    /// <param name="slot"></param>
    /// <param name="item"></param>
    // public void AddItemToSlot(GameObject slot, InventoryItem item){
    //     GameObject obj = Instantiate(m_itemPrefab);
    //     obj.transform.SetParent(slot.transform,false);
    //     obj.name = item.data.name ;
    //     UIInventoryItemContainer container = obj.GetComponent<UIInventoryItemContainer>();
    //     container.item = item;
    // }

    public void AddItemToSlot(GameObject slot, InventoryItem item){
        GameObject obj = Instantiate(m_itemPrefab);
        obj.transform.SetParent(slot.transform,false);
        obj.name = item.containedItem;
        UIContainer container = obj.GetComponent<UIContainer>();
        container.item = item;
    }

    public void Refresh<T>(List<T> newStuff){
        foreach(GameObject slot in _inventorySlots){
            Destroy(slot);
        }
        foreach(var thing in newStuff){
            //Debug.Log("Add Adventurer");
            GameObject slot = CreateInventorySlot();
            InventoryItem inItem = new InventoryItem(thing as DataEntity);
            AddItemToSlot(slot, inItem);
        }
    }
    public void Refresh(InventoryManager inventoryManager){
        foreach(GameObject slot in _inventorySlots){
            Destroy(slot);
        }
        foreach(KeyValuePair<string,InventoryItem> item in inventoryManager.inventory){
                GameObject slot = CreateInventorySlot();
                //_inventorySlots.Add(invSlot);
                InventoryItem inItem = new InventoryItem(item.Value.data);
                AddItemToSlot(slot, inItem);
            }
    }

    public void displayAdventurer(Adventurer adv){
        parent.showEquipmentOnAdventurer(adv);
    }
    public void displayParty(AdventurerParty party){
        parent.showAdventurersInParty(party);
    }
}
