using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public class HasTootip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private InputAction showTooltipAction;
    private DataEntity _currentData;
    public bool _hovered;
    private UIToolTip tooltip;
    private GameObject lastHovered;
    private GameObject tooltipPrefab;
    private bool _rendering = false;
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
            tooltip.showTooltip(true);
        }
        if(!showTooltipAction.IsPressed()){
            tooltip.showTooltip(false);
            StopCoroutine(tooltip.UpdateTooltip(lastHovered));
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _hovered = true;
        lastHovered = eventData.pointerEnter;
        try{
            _currentData = lastHovered.GetComponentInChildren<UIContainer>().item.data;
        }
        catch{
            Debug.Log("No Data contained on" +lastHovered.name);
        }
        
              
    }
    
    public void OnPointerExit(PointerEventData eventdata){
        _hovered = false;
        tooltip.showTooltip(false);
        
    }

    
    // public void showTooltip(bool active){
    //     if(active){
    //         if(!tooltipPrefab.activeSelf){
    //             StartCoroutine(UpdateTooltip(lastHovered)); 
    //             tooltipPrefab.SetActive(true);
    //         }
    //     }
    //     else{
    //         if(tooltipPrefab.activeSelf){
    //             tooltipPrefab.SetActive(false);
    //         }
    //     }
        
    // }
}
