using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

/// <summary>
/// Inventory management class for all entitys (and the player) that has an inventory
/// </summary>
/// <value>List (InventoryItem) inventory</value>
public class InventoryManager
{
    public delegate void OnInventoryChangedEvent();
    public static OnInventoryChangedEvent onInventoryChangedEvent;
    //TODO: I probably dont need both the Dictionary and List here... i should fix that
    private Dictionary<DataEntity,InventoryItem> m_itemDictionary = new Dictionary<DataEntity, InventoryItem>();
    
    public List<InventoryItem> inventory {get; private set;} = new List<InventoryItem>();
    public InventoryType type {get; private set;}

    public InventoryManager(InventoryType type){
        SetType(type);
    }

    public InventoryManager SetType(InventoryType t){
        type = t;
        return this;
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
                RawMaterial raw = entity as RawMaterial;

                if(m_itemDictionary.TryGetValue(raw, out InventoryItem val2)){
                    value = val2;
                    return true;
                };
                value = null;
                return false;

            default:
                Debug.Log("Default Case???");
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
