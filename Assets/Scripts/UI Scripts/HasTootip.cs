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
            showTooltip(true);
        }
        if(!showTooltipAction.IsPressed()){
            showTooltip(false);
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
        
        //StartCoroutine(UpdateTooltip(lastHovered));       
    }
    
    public void OnPointerExit(PointerEventData eventdata){
        _hovered = false;
        showTooltip(false);
        //StopCoroutine(UpdateTooltip(lastHovered));
    }

    private IEnumerator UpdateTooltip(GameObject currentObject){
        _currentData = currentObject.GetComponentInChildren<UIContainer>().item.data;
        if(_currentData){
            Singleton.Instance.TooltipPrefab.SetActive(true);
            tooltip.SetTitle(_currentData.name);
            tooltip.AddNewEntry("Entity Type: ",_currentData.entityType);
        }
        
        yield break;
        //TODO: Extend to included data for... everything
    }
    public void showTooltip(bool active){
        if(active){
            tooltipPrefab.SetActive(true);
        }
        else{
            tooltipPrefab.SetActive(false);
        }
        
    }
}
