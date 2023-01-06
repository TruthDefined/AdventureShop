using System.Collections.Generic;
using UnityEngine;
public class InventoryItem
{
    public List<DataEntity> data {get; protected set;} = new List<DataEntity>();
    public string containedItem {get; protected set;} = "";
    public Sprite icon;

    public InventoryItem(DataEntity source){
        
        containedItem = source.name;
        data.Add(source);
    }
    public InventoryItem(List<DataEntity> source){
        containedItem = source[0].name;
        data = source;
        // foreach (var item in source)
        // {
        //     data.Add(item);
        // }
    }

    public bool Add(DataEntity source){
        if(containedItem == source.name){
            data.Add(source);
            return true;
        }
        else{
            Debug.Log($"Cannot add {source.name} to {containedItem} Inventory Item");
            return false;
        }
    }

    //Grab the first item and remove it from the inventory
    public DataEntity Get(){
        DataEntity pulledObject = data[0];
        data.Remove(pulledObject);
        return pulledObject;
    }

    //Grabs a specific item and removes it from the invetory
    public DataEntity Get(DataEntity target){
        DataEntity pulledObject = data[data.IndexOf(target)];
        data.Remove(pulledObject);
        return pulledObject;

    }
}