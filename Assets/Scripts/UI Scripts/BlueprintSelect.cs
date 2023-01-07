using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;
using System.Collections.Generic;
using TMPro;

public class BlueprintSelect : MonoBehaviour, IDropHandler
{
    public TMP_Text Title;
    public TMP_Dropdown Dropdown;
    public Image icon;

    public List<MaterialType> type;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag.name);
        RawMaterial droppedItem = eventData.pointerDrag.GetComponentInChildren<UIContainer>().item.data[0] as RawMaterial;
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
