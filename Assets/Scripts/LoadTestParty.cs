using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadTestParty : MonoBehaviour
{

    public AdventurerParty Party;
    //private UIDataDisplayController displayController;

    public UIDataDisplayController PartyContainer;
    public UIDataDisplayController AdventurerContainer;
    public UIDataDisplayController EquipmentContainer;


    private void Awake() {
        //displayController = PartyContainer.GetComponent<UIDataDisplayController>();
    }
    private void Start() {
        GameObject slot = PartyContainer.CreateInventorySlot();
        SlotItem item = new SlotItem( Party);
        PartyContainer.AddItemToSlot(slot, item);

        foreach(Adventurer adventurer in Party.adventurers){
            GameObject adSlot = AdventurerContainer.CreateInventorySlot();
            SlotItem adItem = new SlotItem( adventurer);
            PartyContainer.AddItemToSlot(adSlot, adItem);
            
            if(adventurer.equipment == null){
                Debug.Log("Equipment Inventory is not instatiated");
                adventurer.equipment = new InventoryManager(InventoryType.Equipment);
            }
            if(adventurer.equipment.inventory.Count>0){
                foreach(InventoryItem equipment in adventurer.equipment.inventory){
                    GameObject invSlot = EquipmentContainer.CreateInventorySlot();
                    SlotItem inItem = new SlotItem(equipment.data);
                    EquipmentContainer.AddItemToSlot(invSlot, inItem);
                }
            }
            else{
                Debug.Log("Equipment Inventory is Empty");
            }
            
        }

        
    }
}
