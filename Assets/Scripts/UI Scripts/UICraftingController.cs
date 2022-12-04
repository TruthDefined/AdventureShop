using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class UICraftingController : MonoBehaviour
{
    private CraftingManager _craftingManager;
    private List<GameObject> _partDropdowns = new List<GameObject>();
    private TMP_Dropdown _blueprintDropdown;
    

    public List<GameObject> partDropDowns {get{return _partDropdowns;}} 
    public GameObject blueprintDropdown;
    public GameObject dropdownTemplate;
    public Blueprint activeBlueprint;
    void Start()
    {
        _blueprintDropdown = blueprintDropdown.GetComponent<TMP_Dropdown>();
        TMP_Text title = blueprintDropdown.GetComponent<UITitle>().title;
        title.text = "Blueprint";

        _craftingManager = Singleton.Instance.CraftingManager;
        _blueprintDropdown.ClearOptions();

        //Add Empty option to blueprint dropdown
        _blueprintDropdown.options.Add(new TMP_Dropdown.OptionData());
        
        //Populate dropdown with all possible blueprints
        foreach (Blueprint bprint in _craftingManager.blueprints){
            _blueprintDropdown.options.Add(new TMP_Dropdown.OptionData(bprint.name,bprint.icon));
        }
    }


    /// <returns>The Gameobject represented by the selection of the dropdown. </returns>
    public TMP_Dropdown.OptionData GetDropdownOption(GameObject dropdown){
        TMP_Dropdown temp = dropdown.GetComponent<TMP_Dropdown>();
        return temp.options[temp.value];
    } 

    /// <returns> The int value of the option selected on the dropdown. </returns>
    public int GetDropdownValue(GameObject dropdown){
        return dropdown.GetComponent<TMP_Dropdown>().value;
    } 
    /// <summary>
    /// Populate blueprint creation dropdowns.
    /// </summary>
   public void OnBlueprintChoice(){
        activeBlueprint = _craftingManager.blueprints.Where(obj => obj.name == _blueprintDropdown.options[_blueprintDropdown.value].text).SingleOrDefault();     
        float yOffset = -60f;
        
        foreach (GameObject drop in _partDropdowns){
            Destroy(drop);
        }

        _partDropdowns.Clear();

        if(activeBlueprint){
            Debug.Log(activeBlueprint.name);
            //Create new dropdown for each possible part for selected blueprint
            for (int i = 0; i < activeBlueprint.partsRequired.Count(); i++){  

                var tempDropdown = Instantiate(dropdownTemplate,blueprintDropdown.transform,false);
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
                //Populate the dropdown with acceptable items
                foreach(RawMaterial raw in _craftingManager.rawMaterials){
                    if( activeBlueprint.partsRequired[i].acceptableMaterials.Contains(raw.type)){
                        if(Singleton.Instance.Player_Raw_Inventory.Get(raw).stackSize >0){
                            tempDropdown.GetComponent<TMP_Dropdown>().options.Add(new TMP_Dropdown.OptionData(raw.name,raw.icon));
                        }
                    }
                }
                    
                    //tempDropdown.GetComponent<TMP_Dropdown>().options.Add(new TMP_Dropdown.OptionData(type.name,type.sprite));
                _partDropdowns.Add(tempDropdown);
            }
        }
    
   }
}