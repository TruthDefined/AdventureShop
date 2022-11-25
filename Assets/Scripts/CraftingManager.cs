using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class CraftingManager : MonoBehaviour
{
    [SerializeField] private List<Blueprint> _blueprints;
    [SerializeField] private List<MaterialType> _materialTypes;
    [SerializeField] private List<RawMaterial> _rawMaterials;
    [SerializeField] private List<PartType> _partTypes;

    private UIManager UI;
    private void Start() {
        UI = Singleton.Instance.UIManager;
    }

    public void Craft(){
        if(UI.activeBlueprint){
            Blueprint craftingBlueprint = blueprints.Find((x) => x.name == UI.GetDropdownOption(UI.blueprintDropdown).text);
            List<PartType> partsList = new List<PartType>();
            List<RawMaterial> matList = new List<RawMaterial>();
            
            for (int i = 0; i < craftingBlueprint.partsRequired.Count(); i++){                
                
                if(UI.GetDropdownValue(UI.partDropDowns[i]) != 0){
                    partsList.Add(craftingBlueprint.partsRequired[i]);
                    matList.Add(rawMaterials.Find((x) => x.name == UI.GetDropdownOption(UI.partDropDowns[i]).text));
                }
                else{
                    if(craftingBlueprint.partsRequired[i].optional == false){
                        Debug.Log("Incomplete Blueprint");
                        return;
                    }
                }
            }

            Equipment newEquipment = ScriptableObject.CreateInstance("Equipment") as Equipment;
            newEquipment.init(craftingBlueprint, partsList.ToArray(), matList.ToArray());
            newEquipment.name = matList[0].name + partsList[0].name; 
            Singleton.Instance.Player_Equipment_Inventory.Add(newEquipment);

        }
    }






#region GetLists
    public List<Blueprint> blueprints
    {
        get{
            return _blueprints;
        }
        set{
            print("Value cannot be set at Runtime");
        }
    }
    public List<MaterialType> materialTypes
    {
        get{
            return _materialTypes;
        }
        set{
            print("Value cannot be set at Runtime");
        }
    }
    public List<RawMaterial> rawMaterials
    {
        get{
            return _rawMaterials;
        }
        set{
            print("Value cannot be set at Runtime");
        }
    }
    public List<PartType> partTypes
    {
        get{
            return _partTypes;
        }
        set{
            print("Value cannot be set at Runtime");
        }
    }
#endregion    
    // Start is called before the first frame update
#region EDITOR
    #if UNITY_EDITOR
    public void UpdateBlueprints(List<Blueprint> newList){
        _blueprints = newList;
    }

    public void UpdateLists<T>(List<T> newList){
        switch (newList.GetType())
        {
            case System.Type blue when blue == typeof(List<Blueprint>):
                _blueprints = newList as List<Blueprint>;
            break;
            case System.Type mat when mat == typeof(List<MaterialType>):
                _materialTypes = newList as List<MaterialType>;
            break;
            case System.Type pt when pt == typeof(List<PartType>):
                _partTypes = newList as List<PartType>;
            break;
            case System.Type raw when raw == typeof(List<RawMaterial>):
                _rawMaterials = newList as List<RawMaterial>;
            break;

            default:
            break;
        }

    }
    public void Print(){
        foreach (Blueprint b in blueprints)
        {
            Debug.Log(b.name);
        }
        foreach (MaterialType m in materialTypes)
        {
            Debug.Log(m.name);
        }
        foreach (PartType p in partTypes)
        {
            Debug.Log(p.name);
        }
        foreach (RawMaterial r in rawMaterials)
        {
            Debug.Log(r.name);
        }
    }
    #endif
#endregion
}
