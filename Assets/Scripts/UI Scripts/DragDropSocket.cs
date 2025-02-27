using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class DragDropSocket : MonoBehaviour, IDropHandler
{
    private Transform oldParent;
    
    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        //Grab the dropped item from the event
        GameObject droppedItem = eventData.pointerDrag;
        //Grab the draggable item script from the item
        DraggableItem droppedDraggableItem = droppedItem.GetComponent<DraggableItem>();
        //Save the old owner of the current slot, in case they need to be switched
        DraggableItem oldDraggable;
        //Save the parent of the dropped item, for similar reasons
        oldParent = droppedDraggableItem.parentAfterDrag;
        Debug.Log($"Old Parent: {oldParent.name}");
        Debug.Log(droppedItem.name + " dropped on " + transform.parent.name);
        
        if(sameTypeAsSocket(droppedItem)){
            //if the slot is empty
            if(transform.childCount == 0){
                //set this item to the slot
                droppedDraggableItem.parentAfterDrag = transform;
            }
            //if the slot is full
            else if(transform.childCount == 1){
                //trade item in current slot and item in origin slot
                oldDraggable = transform.GetComponentInChildren<DraggableItem>();
                oldDraggable.transform.SetParent(oldParent);
                droppedDraggableItem.parentAfterDrag = transform;
            }
            else{
                Debug.Log("Too many items in: " + transform.parent.name);
            }
        }
        else{
            if(addToSocket(droppedItem)){
            //Debug.Log("Added!");
            }
        }

        
    }

    //TODO: Bug when re-adding equipment to original owner
    //TODO: Does not remove equipment from Player inventory when assigned to Adventurer
    public bool addToSocket( GameObject droppedItem){
        UIContainer currentContainer = GetComponentInChildren<UIContainer>();
        UIContainer droppedContainer = droppedItem.GetComponentInChildren<UIContainer>();
        //Debug.Log($"Contained Item == {current.item.data[0].GetType()}");
        if(currentContainer.item.data[0].GetType() == typeof(AdventurerParty)){
            if(droppedContainer.item.data[0].GetType() == typeof(Adventurer)){
                AdventurerParty party = currentContainer.item.data[0] as AdventurerParty;
                party.adventurers.Add(droppedContainer.item.data[0] as Adventurer);
                
                GetComponentInParent<UIDataDisplayController>().displayParty(party);
                return true;
            }
        }
        else if(currentContainer.item.data[0].GetType() == typeof(Adventurer)){
            if(droppedContainer.item.data[0].GetType() == typeof(Equipment)){
                //Grabe the first item from the InventoryItem Dictionary
                Equipment item = droppedContainer.item.data[0] as Equipment;
                //Get the last owner of the current piece of Equipment contained in InventoryItem Dictionary
                Creature lastOwner = item.GetHistory[item.GetHistory.Count-1] as Creature;
                //Grab the first(and only) adventurer in current InventoryItem Dictionary
                Adventurer adventurer = currentContainer.item.data[0] as Adventurer;
                //Add Item to new inventory
                adventurer.equipment.Add(droppedContainer.item.data[0] as Equipment,adventurer);
                //Remove Item from old inventory
                if(lastOwner == Singleton.Instance.PlayerGuild.guildMaster){
                    Singleton.Instance.Player_Equipment_Inventory.Remove(droppedContainer.item.data[0] as Equipment);
                    Debug.Log("Remove from player inventory");
                }
                else{
                    lastOwner.equipment.Remove(droppedContainer.item.data[0] as Equipment);
                }
                
                GetComponentInParent<UIDataDisplayController>().displayAdventurer(adventurer);
                EventSystem.current.SetSelectedGameObject(currentContainer.transform.parent.gameObject);
                return true;
            }
        }
        return false;
    }
        
    public bool sameTypeAsSocket(GameObject droppedItem){
        if(GetComponentInParent<UIInventoryController>()){
            if(droppedItem.GetComponent<DraggableItem>().parentAfterDrag.GetComponentInParent<UIInventoryController>().type == GetComponentInParent<UIInventoryController>().type){
                return true;
            }
            else{
                Debug.Log("Not a valid drop location");
                return false;
            }
        }
        Debug.Log("Not Inventory");
        //TODO: When crafted item is dropped on adventurer, they are not seen as having an inventory so this triggers and returns false.
        return false;
        
    }
}
