using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;

public class UICraftingController : MonoBehaviour
{
    private EntityManager _entityManager;
    private List<GameObject> _partDropdowns = new List<GameObject>();
    private TMP_Dropdown _blueprintDropdown;
    

    public List<GameObject> partDropDowns {get{return _partDropdowns;}} 
    public GameObject blueprintDropdown;
    public GameObject dropdownTemplate;
    public Blueprint activeBlueprint;

    void Start()
    {
        _blueprintDropdown = blueprintDropdown.GetComponent<BlueprintSelect>().Dropdown;
        TMP_Text title = blueprintDropdown.GetComponent<BlueprintSelect>().Title;
        title.text = "Blueprint";

        _entityManager = Singleton.Instance.EntityManager;
        _blueprintDropdown.ClearOptions();

        //Add Empty option to blueprint dropdown
        _blueprintDropdown.options.Add(new TMP_Dropdown.OptionData());
        
        //Populate dropdown with all possible blueprints
        foreach (Blueprint bprint in _entityManager.blueprints){
            _blueprintDropdown.options.Add(new TMP_Dropdown.OptionData(bprint.name,bprint.icon,Color.white));
        }

    }


    /// <returns>The Gameobject represented by the selection of the dropdown. </returns>
    public TMP_Dropdown.OptionData GetDropdownOption(GameObject dropdown){
        
        TMP_Dropdown temp = dropdown.GetComponent<BlueprintSelect>().Dropdown;
        return temp.options[temp.value];
    } 

    /// <returns> The int value of the option selected on the dropdown. </returns>
    public int GetDropdownValue(GameObject dropdown){
        return dropdown.GetComponent<BlueprintSelect>().Dropdown.value;
    } 

    private void HandleBeginDrag(InventoryItem obj){
       
    }

    private void HandleEndDrag(InventoryItem obj){
        
    }


    /// <summary>
    /// Populate blueprint creation dropdowns.
    /// </summary>
   public void OnBlueprintChoice(){ 
        activeBlueprint = _entityManager.blueprints.Where(obj => obj.name == _blueprintDropdown.options[_blueprintDropdown.value].text).SingleOrDefault();     
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
                TMP_Text title = tempDropdown.GetComponent<BlueprintSelect>().Title;
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

                tempDropdown.GetComponent<BlueprintSelect>().Dropdown.options.Add(new TMP_Dropdown.OptionData());
                tempDropdown.GetComponent<BlueprintSelect>().type = activeBlueprint.partsRequired[i].acceptableMaterials;
                //Populate the dropdown with acceptable items
                foreach(KeyValuePair<string,InventoryItem> raw in Singleton.Instance.Player_Raw_Inventory.inventory){
                    RawMaterial mat = null;
                    try
                    {
                       mat = raw.Value.data[0] as RawMaterial;
                    }
                    catch (System.Exception)
                    {
                        Debug.Log($"No {raw.Value.containedItem} in Player inventory");
                        throw;
                    }

                    if( activeBlueprint.partsRequired[i].acceptableMaterials.Contains(mat.type)){
                        if(Singleton.Instance.Player_Raw_Inventory.inventory.Count>0){
                            tempDropdown.GetComponent<BlueprintSelect>().Dropdown.options.Add(new TMP_Dropdown.OptionData(mat.name,mat.icon,Color.white));
                        }
                    }
                }
                    
                    //tempDropdown.GetComponent<TMP_Dropdown>().options.Add(new TMP_Dropdown.OptionData(type.name,type.sprite));
                _partDropdowns.Add(tempDropdown);
            }
        }
    
   }
}
