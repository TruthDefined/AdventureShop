using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using System.Linq;

/// <summary>
/// Inventory management class for all entitys (and the player) that has an inventory
/// </summary>
/// <value>List (InventoryItem) inventory</value>
public class InventoryManager
{

    //<"Copper Ore", InventoryItem(List<Copper Ore>)>
    public Dictionary<string,InventoryItem> inventory {get; protected set;} = new Dictionary<string, InventoryItem>();
    
    /// <returns>InventoryItem containing passed data.</returns>
    public virtual InventoryItem GetItem (string nameOfItem){
        if(inventory.TryGetValue(nameOfItem, out InventoryItem value)){
            return value;
        }
        Debug.Log($"No item of type {nameOfItem} was found");
        return null;
    }
    public virtual DataEntity PullEntity (string nameOfItem){
        if(inventory.TryGetValue(nameOfItem, out InventoryItem value)){
            return value.Get();
        }
        Debug.Log($"No item of type {nameOfItem} was found");
        return null;
    }

    /// <summary>
    /// Adds or increases count of passed item from inventory.
    /// </summary>
    public virtual void Add(DataEntity referenceData, Creature newOwner = null){
        if(TypeIsInInventory(referenceData, out InventoryItem value)){
            value.Add(referenceData);
        }
        else{
            InventoryItem newItem = new InventoryItem(referenceData);
            //newItem.Add(referenceData);
            inventory.Add(referenceData.name, newItem);
        }
        try
        {
            HistoricalEntity hist = referenceData as HistoricalEntity;
            if(newOwner){
                hist.AddToHistory(newOwner);
                //Debug.Log("Added to history");
            }
            
        }
        catch (System.Exception)
        {
            Debug.Log("Not Historical Entity");
            throw;
        }
        //PrintInventory();
        //onInventoryChangedEvent();
    }

    /// <summary>
    /// Reduces or Removes passed item from inventory.
    /// </summary>

    public virtual DataEntity Remove(DataEntity referenceData){
        if(inventory.TryGetValue(referenceData.name, out InventoryItem value)){
            DataEntity temp = null;
            if(value.data.Count > 0)
            {
                temp = value.data[0];
                value.data.Remove(temp);
                //Debug.Log($"{value.data.Count} {value.containedItem} left in inventory");
            }
            if(value.data.Count == 0){
                inventory.Remove(referenceData.name);
                //Debug.Log($"{value.data.Count} {value.containedItem} left in inventory");
            }
            return temp;
            // value.Get(referenceData);
            // value = null;
        } else{
            Debug.Log($"No item of type {referenceData.name} was found");
            return null;
        }
        //nInventoryChangedEvent();
        //PrintInventory();
    }
    
    /// <summary>
    /// Checks to see if passed entity is included in this inventory. 
    /// </summary>
    /// <returns> InventoryItem containing passed entity as data. </returns>
    public bool TypeIsInInventory(DataEntity entity, out InventoryItem value){
       if(entity){
            if(inventory.TryGetValue(entity.name, out InventoryItem val)){
                value = val;
                return true;
            }
            else{
                value = null;
                return false;
            }
       }
       value = null;
       return false;
        // switch (type){
        //     //TODO: Not working correctly. Likley have to overhault inventory system....
        //     case InventoryType.Equipment:
        //         Equipment equip = entity as Equipment;
        //         foreach (Equipment item in _itemDictionary.Keys){
        //             Debug.Log("Going through items " + item.name);
        //             if(_itemDictionary.TryGetValue(equip, out InventoryItem val)){
        //                 value = val;
        //                 return true;
        //             };
        //             // if (item.partsRequired.SequenceEqual(equip.partsRequired) && item.usedMaterials.SequenceEqual(equip.usedMaterials)){
        //             //     if(m_itemDictionary.TryGetValue(item, out InventoryItem val)){
        //             //         value = val;
        //             //         return true;
        //             //     }
        //             // } 
        //         }
        //         value = null;
        //         return false;
        //     case InventoryType.RawMaterials:
        //         RawMaterial raw = entity as RawMaterial;

        //         if(_itemDictionary.TryGetValue(raw, out InventoryItem val2)){
        //             value = val2;
        //             return true;
        //         };
        //         value = null;
        //         return false;

        //     default:
        //         Debug.Log("Default Case???");
        //         value = null;
        //         return false;
        // }
    }

}

/// <summary>Possible inventory types.</summary>
public enum InventoryType{
        Null,
        RawMaterials,
        Equipment,
        CraftedMaterials
    }
