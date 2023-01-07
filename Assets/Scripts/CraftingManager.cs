using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class CraftingManager : MonoBehaviour
{
    private UICraftingController UI;
    private EntityManager eManager;
    private void Start() {
        UI = Singleton.Instance.UICraftingController;
        eManager = Singleton.Instance.EntityManager;
    }

    /// <summary>
    /// Craft a new Equipment based on selected Blueprint and current Dropdown options
    /// </summary>
    public void Craft(){
        Debug.Log("Craft");
        if(UI.activeBlueprint){
            Blueprint craftingBlueprint = eManager.blueprints.Find((x) => x.name == UI.GetDropdownOption(UI.blueprintDropdown).text);
            List<PartType> partsList = new List<PartType>();
            List<RawMaterial> matList = new List<RawMaterial>();
            List<RawMaterial> materialsUsed = new List<RawMaterial>();
            
            for (int i = 0; i < craftingBlueprint.partsRequired.Count(); i++){                
                
                if(UI.GetDropdownValue(UI.partDropDowns[i]) != 0){
                    partsList.Add(craftingBlueprint.partsRequired[i]);
                    matList.Add(eManager.rawMaterials.Find((x) => x.name == UI.GetDropdownOption(UI.partDropDowns[i]).text));
                }
                else{
                    if(craftingBlueprint.partsRequired[i].optional == false){
                        Debug.Log("Incomplete Blueprint");
                        return;
                    }
                }
            }
            //Check to make sure there are enough of each material to craft
            //TODO: This is why crafting isn't working
            bool craftable = true;
            var inventory = Singleton.Instance.Player_Raw_Inventory;
            foreach(RawMaterial mat in matList){
                if(inventory.TypeIsInInventory(mat, out InventoryItem item)){
                    Debug.Log($"{item.data.Count} {mat} in inventory");
                    if(item.data.Count>=1){
                        var use = item.Get() as RawMaterial;
                        materialsUsed.Add(use);
                        //inventory.Remove(use);
                    } else{
                        craftable = false;
                        Debug.Log($"Not enough {item.containedItem} to craft");
                    }
                }
                else{
                    Debug.Log("Material not in inventory");
                }

                // if(Singleton.Instance.Player_Raw_Inventory.inventory.Count>0){
                //     materialsUsed.Add(mat);
                //     Singleton.Instance.Player_Raw_Inventory.Remove(mat);
                // } else{
                //     Debug.Log("Not enough " + mat.name + " to craft!");
                //     foreach (RawMaterial refundMaterial in materialsUsed){
                //         Singleton.Instance.Player_Raw_Inventory.Add(refundMaterial);
                //     }
                //     return;
                // }
            }
            if(!craftable){
                Debug.Log("Refunding Materials");
                foreach (RawMaterial refundMaterial in materialsUsed){
                    inventory.Add(refundMaterial);
                }
                return;
            }

            Equipment newEquipment = ScriptableObject.CreateInstance("Equipment") as Equipment;

            string importantMatNames = "";

            for (int i = 0; i < matList.Count; i++)
            {
                if(!partsList[i].optional){
                    if(importantMatNames != ""){
                        importantMatNames += ", ";
                    }
                    importantMatNames += matList[i].name;
                }
            }
            //TODO: Player should be designed as a CREATURE for ease of crafting Init (Or equipment needs special Init just for player-crafted status)
            newEquipment.Init(importantMatNames + " " + craftingBlueprint.name, Generate.RandomLocation(true), Singleton.Instance.Player, Generate.RandomCreature(true), craftingBlueprint, partsList, materialsUsed, 0, 0, "");
            newEquipment.name = importantMatNames + " " + craftingBlueprint.name;
            Singleton.Instance.EntityManager.AddEquipment(newEquipment);
            Singleton.Instance.Player_Equipment_Inventory.Add(newEquipment,Singleton.Instance.Player);

        }
    }
}
