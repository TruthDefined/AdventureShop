using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
        Debug.Log(oldParent.name);
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

        if(addToSocket(droppedItem)){
            Debug.Log("Added!");
        }
    }

    public bool addToSocket( GameObject droppedItem){
        UIContainer current = GetComponentInChildren<UIContainer>();
        UIContainer dropped = droppedItem.GetComponentInChildren<UIContainer>();
        if(current.item.data.GetType() == typeof(AdventurerParty)){
            if(dropped.item.data.GetType() == typeof(Adventurer)){
                AdventurerParty party = current.item.data as AdventurerParty;
                party.adventurers.Add(dropped.item.data as Adventurer);
                GetComponentInParent<UIDataDisplayController>().displayParty(party);
                return true;
            }
        }
        else if(current.item.data.GetType() == typeof(Adventurer)){
            if(dropped.item.data.GetType() == typeof(Equipment)){
                Equipment item = dropped.item.data as Equipment;
                Debug.Log(item.GetHistory.Count);
                Adventurer lastAdventurer = item.GetHistory[item.GetHistory.Count-1] as Adventurer;
                //Adventurer lastAdventurer = item.GetHistory[0] as Adventurer;
                Debug.Log("LastOwner: " + lastAdventurer.name);
                Adventurer adventurer = current.item.data as Adventurer;
                
                lastAdventurer.equipment.Remove(dropped.item.data as Equipment);
                adventurer.equipment.Add(dropped.item.data as Equipment);
                GetComponentInParent<UIDataDisplayController>().displayAdventurer(adventurer);
                //TODO: Add Equipment to Adventurer;
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
        return false;
        
    }
}
