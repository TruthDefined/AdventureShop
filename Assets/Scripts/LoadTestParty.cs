using System;
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
    public List<GameObject> ActivePartySlots;

    private AdventurerParty activeParty;
    private Adventurer activeAdventurer;
    private AdventurerGuild activeGuild;

    private void Awake() {
        // displayController = PartyContainer.GetComponent<UIDataDisplayController>();
    }
    private void Start() {
        //GameObject slot = PartyContainer.CreateInventorySlot();
        //GenerateRandomParty();
        // SlotItem item = new SlotItem( Party);
        //InventoryItem item = new InventoryItem(Party);
        //PartyContainer.AddItemToSlot(slot, item);
    }
    public void showEquipmentOnAdventurer(Adventurer active){
        activeAdventurer = active;
        EquipmentContainer.Refresh(active.equipment);
    }
    public void showAdventurersInParty(AdventurerParty active){
        activeParty = active;
        AdventurerContainer.Refresh(active.adventurers);
        foreach(Adventurer adventurer in active.adventurers){
            AddEquipmentToAdventurer(adventurer);
        }
        showEquipmentOnAdventurer(activeParty.adventurers[0]);
    }
    public void showPartiesInGuild(AdventurerGuild active){
        activeGuild = active;
        PartyContainer.Refresh(Singleton.Instance.PlayerGuild.parties);
    }

    public void refreshAdventurer(){
        showEquipmentOnAdventurer(activeAdventurer);
    }
    public void refreshParty(){
        showAdventurersInParty(activeParty);
    }
    public void refreshGuild(){
        showPartiesInGuild(activeGuild);
    }   

    public void GenerateRandomParty(){
        AdventurerParty newParty = Generate.RandomParty(true);
        Singleton.Instance.PlayerGuild.parties.Add(newParty);
        refreshGuild();
        showAdventurersInParty(newParty);
        
        //showAdventurersInParty(newParty);
        //showEquipmentOnAdventurer(newParty.adventurers[0]);
    }

    public void AddEquipmentToAdventurer(Adventurer adventurer){
        if(adventurer.equipment.inventory.Count== 0){
                int starterEquipmentCount = UnityEngine.Random.Range(2,4);
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
