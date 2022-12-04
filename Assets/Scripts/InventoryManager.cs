using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class InventoryManager : MonoBehaviour
{
    public delegate void OnInventoryChangedEvent();
    public static OnInventoryChangedEvent onInventoryChangedEvent;
    private Dictionary<DataEntity,InventoryItem> m_itemDictionary;
    
    public List<InventoryItem> inventory {get; private set;}
    public InventoryType type {get; private set;}


    public InventoryManager SetType(InventoryType t){
        type = t;
        return this;
    }

    private void Awake() {
        inventory = new List<InventoryItem>();
        m_itemDictionary = new Dictionary<DataEntity, InventoryItem>();
    }

    /// <returns>InventoryItem containing passed data.</returns>
    public InventoryItem Get (DataEntity referenceData){
        if(m_itemDictionary.TryGetValue(referenceData, out InventoryItem value)){
            return value;
        }
        return null;
    }

    /// <summary>
    /// Adds or increases count of passed item from inventory.
    /// </summary>
    public void Add(DataEntity referenceData){
        if(IsInInventory(referenceData, out InventoryItem value)){
            value.AddToStack();
        }
        else{
            InventoryItem newItem = new InventoryItem(referenceData);
            inventory.Add(newItem);
            m_itemDictionary.Add(referenceData, newItem);
        }
        //PrintInventory();
        onInventoryChangedEvent();
    }

    /// <summary>
    /// Reduces or Removes passed item from inventory.
    /// </summary>

    public void Remove(DataEntity referenceData){
        if(IsInInventory(referenceData, out InventoryItem value))
            {
            value.RemoveFromStack();
            if(value.stackSize == -1){
                inventory.Remove(value);
                m_itemDictionary.Remove(referenceData);
            }
        }
        onInventoryChangedEvent();
        //PrintInventory();
    }
    
    /// <summary>
    /// Checks to see if passed entity is included in this inventory. 
    /// </summary>
    /// <returns> InventoryItem containing passed entity as data. </returns>
    public bool IsInInventory(DataEntity entity, out InventoryItem value){
        switch (type){
            case InventoryType.Equipment:
                print("Equipment");
                Equipment equip = entity as Equipment;
                foreach (Equipment item in m_itemDictionary.Keys){
                    if (item.partsRequired.SequenceEqual(equip.partsRequired) && item.usedMaterials.SequenceEqual(equip.usedMaterials)){
                        if(m_itemDictionary.TryGetValue(item, out InventoryItem val)){
                            value = val;
                            return true;
                        }
                    } 
                }
                value = null;
                return false;
            case InventoryType.RawMaterials:
                //print("Raw");
                RawMaterial raw = entity as RawMaterial;
                //print(raw.name);
                if(m_itemDictionary.TryGetValue(raw, out InventoryItem val2)){
                    value = val2;
                    return true;
                };
                value = null;
                return false;

            default:
                print("Default Case???");
                value = null;
                return false;
        }
    }


    private void PrintInventory(){
        foreach (InventoryItem item in inventory){
            Debug.Log(item.stackSize + " " + item.data.name);
        }
    }

}

/// <summary>Possible inventory types.</summary>
/// <remarks>Null indicates the players inventory</remarks>
public enum InventoryType{
        Null,
        RawMaterials,
        Equipment,
        CraftedMaterials
    }
