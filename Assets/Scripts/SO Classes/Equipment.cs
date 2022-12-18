using UnityEngine;
[CreateAssetMenu(fileName = "Blueprint", menuName = "ScriptableObjects/Blueprint")]
/// <summary>
///  An Equipment is created by applying materials to the parts of a blueprint
/// </summary>
public class Equipment : HistoricalEntity
{
    /// <value>Blueprint used to generate Equipment</value>
    public Blueprint equipmentType {get; private set;}
    /// <value>The array of parts used to to generate Equipment</value>
    public PartType[] partsRequired {get; private set;}
    /// <value>The array of materials used to to generate Equipment</value>
    public RawMaterial[] usedMaterials {get; private set;}
    /// <value>Base price of Equipment</value>
    public int price;
    /// <value>Max Durability of Equipment</value>
    public int durability;
    public string notes;


    /// <summary>
    /// Sets up new Equipment ScriptableObject.
    /// </summary>
    /// <param name="_type"> Blueprint used to generate Equipment</param>
    /// <param name="_partsRequired">Parts used to to generate Equipment</param>
    /// <param name="_usedMaterials">Materials used to to generate Equipment</param>
    public void init(Blueprint _type, PartType[] _partsRequired, RawMaterial[] _usedMaterials){
        equipmentType = _type;
        partsRequired = _partsRequired;
        usedMaterials = _usedMaterials;
    }

}
