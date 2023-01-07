using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class SelectableSocket : MonoBehaviour, ISelectHandler, IPointerClickHandler, IDeselectHandler
{
    private UIDataDisplayController parentController;
    private UIContainer dataInSocket;
    private Color originalColor = Color.clear;
    void ISelectHandler.OnSelect(BaseEventData eventData)
    {
        originalColor = GetComponent<Image>().color;
        GetComponent<Image>().color = Color.yellow;
        if(dataInSocket.item.data[0].GetType() == typeof(Adventurer)){
            parentController.displayAdventurer(dataInSocket.item.data[0] as Adventurer);
        }
        if(dataInSocket.item.data[0].GetType() == typeof(AdventurerParty)){
            parentController.displayParty(dataInSocket.item.data[0] as AdventurerParty);
        }

    }

    void IDeselectHandler.OnDeselect(BaseEventData eventData)
    {
        GetComponent<Image>().color = originalColor;
    }

    // Start is called before the first frame update
    private void Start() {
        parentController = GetComponentInParent<UIDataDisplayController>();
        dataInSocket = GetComponentInChildren<UIContainer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;
        // Selection tracking
        EventSystem.current.SetSelectedGameObject(gameObject, eventData);
    }
}
