using UnityEngine;
[CreateAssetMenu(fileName = "Blueprint", menuName = "ScriptableObjects/Blueprint", order = 6)]
public class Equipment : DataEntity
{
    public Blueprint equipmentType {get; private set;}
    public PartType[] partsRequired {get; private set;}
    public RawMaterial[] usedMaterials {get; private set;}
    public int price;
    public int durability;
    public string notes;

    public void init(Blueprint _type, PartType[] _partsRequired, RawMaterial[] _usedMaterials){
        equipmentType = _type;
        partsRequired = _partsRequired;
        usedMaterials = _usedMaterials;
    }

}
