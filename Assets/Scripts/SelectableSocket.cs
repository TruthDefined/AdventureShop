using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class SelectableSocket : MonoBehaviour, ISelectHandler, IPointerClickHandler
{
    private UIDataDisplayController parentController;
    private UIContainer dataInSocket;
    void ISelectHandler.OnSelect(BaseEventData eventData)
    {
        if(dataInSocket.item.data.GetType() == typeof(Adventurer)){
            parentController.displayAdventurer(dataInSocket.item.data as Adventurer);
        }
        if(dataInSocket.item.data.GetType() == typeof(AdventurerParty)){
            parentController.displayParty(dataInSocket.item.data as AdventurerParty);
        }

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
