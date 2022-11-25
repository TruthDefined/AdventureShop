using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class UIManager : MonoBehaviour
{
    private CraftingManager _craftingManager;
    private InventoryManager _playerEquipmentInventory;
    private InventoryManager _playerMaterialInventory;
    private List<GameObject> _partDropdowns = new List<GameObject>();
    public List<GameObject> partDropDowns {get{return _partDropdowns;}} 
    private TMP_Dropdown _bluDropdown;
    public GameObject blueprintDropdown;
    public GameObject templateDropdown;
    public GameObject inventoryPanel;
    public GameObject inventoryLabelPrefab;
    public Blueprint activeBlueprint;
    // Start is called before the first frame update
    void Start()
    {
        _bluDropdown = blueprintDropdown.GetComponent<TMP_Dropdown>();
        TMP_Text title = blueprintDropdown.GetComponent<UITitle>().title;
        title.text = "Blueprint";

        _playerEquipmentInventory = Singleton.Instance.Player_Equipment_Inventory;
        _playerMaterialInventory = Singleton.Instance.Player_Raw_Inventory;

        _craftingManager = Component.FindObjectOfType<CraftingManager>();
        _bluDropdown.ClearOptions();


        _bluDropdown.options.Add(new TMP_Dropdown.OptionData());
        foreach (Blueprint bprint in _craftingManager.blueprints){
            _bluDropdown.options.Add(new TMP_Dropdown.OptionData(bprint.name,bprint.icon));
        }
    }


    public TMP_Dropdown.OptionData GetDropdownOption(GameObject dropdown){
        TMP_Dropdown temp = dropdown.GetComponent<TMP_Dropdown>();
        return temp.options[temp.value];
    } 
    public int GetDropdownValue(GameObject dropdown){
        TMP_Dropdown temp = dropdown.GetComponent<TMP_Dropdown>();
        return temp.value;
    } 

   public void OnBlueprintChoice(){
        activeBlueprint = _craftingManager.blueprints.Where(obj => obj.name == _bluDropdown.options[_bluDropdown.value].text).SingleOrDefault();     
        float yOffset = -30f;
        
        foreach (GameObject drop in _partDropdowns){
            Destroy(drop);
        }

        _partDropdowns.Clear();

        if(activeBlueprint){
            for (int i = 0; i < activeBlueprint.partsRequired.Count(); i++){  

                var tempDropdown = Instantiate(templateDropdown,blueprintDropdown.transform,false);
                TMP_Text title = tempDropdown.GetComponent<UITitle>().title;

                tempDropdown.transform.position = blueprintDropdown.transform.position;
                tempDropdown.transform.Translate( new Vector3 (0f,yOffset *(i+1),0f));
                tempDropdown.gameObject.name = activeBlueprint.partsRequired[i].name + " Dropdown";
                title.text = activeBlueprint.partsRequired[i].name;
                
                if (activeBlueprint.partsRequired[i].optional){
                    title.color = Color.yellow;
                }
                else{
                    title.color = Color.green;
                }

                tempDropdown.GetComponent<TMP_Dropdown>().options.Add(new TMP_Dropdown.OptionData());

                foreach(RawMaterial raw in _craftingManager.rawMaterials){
                    if( activeBlueprint.partsRequired[i].acceptableMaterials.Contains(raw.type)){
                        tempDropdown.GetComponent<TMP_Dropdown>().options.Add(new TMP_Dropdown.OptionData(raw.name,raw.icon));
                    }
                }
                    
                    //tempDropdown.GetComponent<TMP_Dropdown>().options.Add(new TMP_Dropdown.OptionData(type.name,type.sprite));
                _partDropdowns.Add(tempDropdown);
            }
        }
    
   }
}
