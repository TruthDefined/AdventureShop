using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;
using TMPro;

public class BlueprintSelect : MonoBehaviour, IDropHandler
{
    public TMP_Text Title;
    public TMP_Dropdown Dropdown;
    public Image icon;

    public MaterialType[] type;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag.name);
        RawMaterial droppedItem = eventData.pointerDrag.GetComponentInChildren<UIInventoryItemContainer>().item.data as RawMaterial;
        var materialTypeOfDroppedItem = droppedItem.type;
        if(type.Contains(materialTypeOfDroppedItem)){
            int data = Dropdown.options.FindIndex((x) => x.text == droppedItem.name);
            Dropdown.value = data;
        }
        else{
            Debug.Log("Invalid Material");
        }
    }
}
