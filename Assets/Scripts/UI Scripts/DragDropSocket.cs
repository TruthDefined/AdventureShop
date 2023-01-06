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

    //TODO: Bug when re-adding equipment to original owner
    //TODO: Does not remove equipment from Player inventory when assigned to Adventurer
    public bool addToSocket( GameObject droppedItem){
        UIContainer current = GetComponentInChildren<UIContainer>();
        UIContainer dropped = droppedItem.GetComponentInChildren<UIContainer>();
        Debug.Log($"Contained Item == {current.item.data[0].GetType()}");
        if(current.item.data[0].GetType() == typeof(AdventurerParty)){
            if(dropped.item.data[0].GetType() == typeof(Adventurer)){
                AdventurerParty party = current.item.data[0] as AdventurerParty;
                party.adventurers.Add(dropped.item.data[0] as Adventurer);
                GetComponentInParent<UIDataDisplayController>().displayParty(party);
                return true;
            }
        }
        else if(current.item.data[0].GetType() == typeof(Adventurer)){
            if(dropped.item.data[0].GetType() == typeof(Equipment)){
                Equipment item = dropped.item.data[0] as Equipment;
                Debug.Log($"***Item: {item.name}");
                Creature lastOwner = item.GetHistory[item.GetHistory.Count-1] as Creature;
                //Adventurer lastAdventurer = item.GetHistory[0] as Adventurer;
                Debug.Log("LastOwner: " + lastOwner.name);
                Adventurer adventurer = current.item.data[0] as Adventurer;
                adventurer.equipment.Add(dropped.item.data[0] as Equipment);
                lastOwner.equipment.Remove(dropped.item.data[0] as Equipment);
                GetComponentInParent<UIDataDisplayController>().displayAdventurer(adventurer);
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
