using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public class HasTootip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private InputAction showTooltipAction;
    private InventoryItem _currentData;
    public bool _hovered;
    private UIToolTip tooltip;
    private GameObject lastHovered;
    private GameObject tooltipPrefab;
    private Keyboard keyboard = Keyboard.current;
    private void Awake() {
        tooltipPrefab = Singleton.Instance.TooltipPrefab;
        tooltip = tooltipPrefab.GetComponent<UIToolTip>();
        //if(keyboard.altKey
        showTooltipAction = new InputAction("showTooltip", binding:"<Keyboard>/alt");
        showTooltipAction.Enable();
    }

    private void Update() {
        if(_hovered && showTooltipAction.IsPressed()){
            tooltip.showTooltip(_currentData, true);
        }
        if(!showTooltipAction.IsPressed()){
            tooltip.showTooltip(_currentData, false);
            StopCoroutine(tooltip.UpdateTooltip(_currentData));
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _hovered = true;
        lastHovered = eventData.pointerEnter;
        try{
            //TODO: Metadate should be InventoryItem dependant, not Dictionary[0] dependant
            _currentData = lastHovered.GetComponentInChildren<UIContainer>().item;
        }
        catch{
            Debug.Log("No Data contained on" +lastHovered.name);
        }
        
              
    }
    
    public void OnPointerExit(PointerEventData eventdata){
        _hovered = false;
        tooltip.showTooltip(_currentData,false);
        
    }
}
