using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using System.Linq;

/// <summary>
/// Inventory management class for all entitys (and the player) that has an inventory
/// </summary>
/// <value>List (InventoryItem) inventory</value>
public class PlayerInventoryManager: InventoryManager
{
    public delegate void OnInventoryChangedEvent();
    public static OnInventoryChangedEvent onInventoryChangedEvent;
    
    public override void Add(DataEntity referenceData, Creature Owner = null){
        base.Add(referenceData);
        onInventoryChangedEvent();
    }
    public void Add(DataEntity referenceData, Player lastOwner = null){
        base.Add(referenceData);
        onInventoryChangedEvent();
    }
    public override DataEntity Remove(DataEntity referenceData){
        DataEntity temp = base.Remove(referenceData);
        onInventoryChangedEvent();
        return temp;
    }
}