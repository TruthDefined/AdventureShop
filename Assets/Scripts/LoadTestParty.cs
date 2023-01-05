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
    public List<GameObject> ActiveEquipmentSlots;
    public List<GameObject> ActiveAdventurerSlots;

    private AdventurerParty activeParty;
    private Adventurer activeAdventurer;

    private void Awake() {
        //displayController = PartyContainer.GetComponent<UIDataDisplayController>();
    }
    private void Start() {
        GameObject slot = PartyContainer.CreateInventorySlot();
        Party = Generate.RandomParty(true);
        //SlotItem item = new SlotItem( Party);
        SlotItem item = new SlotItem(Party);
        PartyContainer.AddItemToSlot(slot, item);
    }

    public void showEquipment(Adventurer active){
        activeAdventurer = active;
        foreach(GameObject slot in ActiveEquipmentSlots){
            Destroy(slot);
        }
        foreach(InventoryItem equipment in active.equipment.inventory){
                GameObject invSlot = EquipmentContainer.CreateInventorySlot();
                ActiveEquipmentSlots.Add(invSlot);
                SlotItem inItem = new SlotItem(equipment.data);
                EquipmentContainer.AddItemToSlot(invSlot, inItem);
            }
    }
    public void showParty(AdventurerParty active){
        activeParty = active;
        foreach(GameObject slot in ActiveAdventurerSlots){
            Destroy(slot);
        }
        foreach(Adventurer adventurer in active.adventurers){
            GameObject adSlot = AdventurerContainer.CreateInventorySlot();
            ActiveAdventurerSlots.Add(adSlot);
            SlotItem adItem = new SlotItem( adventurer);
            PartyContainer.AddItemToSlot(adSlot, adItem);
            
            //Create new inventory of Equipment if needed
            if(adventurer.equipment.inventory.Count== 0){
                int starterEquipmentCount = Random.Range(0,10);
                for (int i = 0; i < starterEquipmentCount; i++)
                {
                    InventoryItem newItem = new InventoryItem(Generate.RandomEquipment(true));
                    adventurer.equipment.inventory.Add(newItem);
                }

            }        
        }
    }

    public void refreshParty(){
        showParty(activeParty);
    }
    public void refreshAdventurer(){
        showEquipment(activeAdventurer);
    }
}
