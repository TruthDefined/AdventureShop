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
        InventoryItem item = new InventoryItem(Party);
        PartyContainer.AddItemToSlot(slot, item);
    }

    public void showEquipment(Adventurer active){
        activeAdventurer = active;
        foreach(GameObject slot in ActiveEquipmentSlots){
            Destroy(slot);
        }
        foreach(KeyValuePair<string,InventoryItem> equipment in active.equipment.inventory){
                GameObject invSlot = EquipmentContainer.CreateInventorySlot();
                ActiveEquipmentSlots.Add(invSlot);
                InventoryItem inItem = new InventoryItem(equipment.Value.data);
                EquipmentContainer.AddItemToSlot(invSlot, inItem);
            }
    }
    public void showParty(AdventurerParty active){
        activeParty = active;
        foreach(GameObject slot in ActiveAdventurerSlots){
            Destroy(slot);
        }
        foreach(Adventurer adventurer in active.adventurers){
            //Debug.Log("Add Adventurer");
            GameObject adSlot = AdventurerContainer.CreateInventorySlot();
            ActiveAdventurerSlots.Add(adSlot);
            InventoryItem adItem = new InventoryItem( adventurer);
            PartyContainer.AddItemToSlot(adSlot, adItem);
            
            //Create new inventory of Equipment if needed
            if(adventurer.equipment.inventory.Count== 0){
                int starterEquipmentCount = Random.Range(2,4);
                for (int i = 0; i < starterEquipmentCount; i++)
                {
                    InventoryItem newItem = new InventoryItem(Generate.RandomEquipment(true));
                    Equipment equipment = newItem.data[0] as Equipment;
                    //equipment.AddToHistory(adventurer);
                    adventurer.equipment.Add(equipment, adventurer);
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
