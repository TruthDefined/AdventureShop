using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropSocket : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public bool mouseover = false;
    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        GameObject droppedItem = eventData.pointerDrag;
        DraggableItem droppedDraggableItem = droppedItem.GetComponent<DraggableItem>();
        DraggableItem oldDraggable;
        Transform oldParent = droppedDraggableItem.parentAfterDrag;

        if(isValidSlot(droppedItem)){
            if(transform.childCount == 0){
                droppedDraggableItem.parentAfterDrag = transform;
            }
            else if(transform.childCount == 1){
                oldDraggable = transform.GetComponentInChildren<DraggableItem>();
                oldDraggable.transform.SetParent(oldParent);
                droppedDraggableItem.parentAfterDrag = transform;
            }
            else{
                Debug.Log("Too many items in: " + transform.parent.name);
            }
        }
    }
        
    public bool isValidSlot(GameObject droppedItem){
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
    public void OnPointerEnter(PointerEventData eventData)
    {
        UIToolTip tooltip = Singleton.Instance.TooltipPrefab.GetComponent<UIToolTip>();
        GameObject droppedItem = eventData.pointerEnter;
        UIContainer itemContainer = droppedItem.GetComponentInChildren<UIContainer>();
        mouseover = true;
        if(itemContainer){
            Singleton.Instance.TooltipPrefab.SetActive(true);
            tooltip.SetTitle(itemContainer.item.data.name);
            tooltip.AddNewEntry("Entity Type: ",itemContainer.item.data.entityType);

            //TODO: Extend to included data for... everything
        }
        
    }
    public void OnPointerExit(PointerEventData eventdata){
        mouseover = false;
        Singleton.Instance.TooltipPrefab.SetActive(false);
    }
}
