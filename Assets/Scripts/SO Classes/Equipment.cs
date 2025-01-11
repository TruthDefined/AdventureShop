using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "Blueprint", menuName = "ScriptableObjects/Blueprint")]
/// <summary>
///  An Equipment is created by applying materials to the parts of a blueprint
/// </summary>
public class Equipment : HistoricalEntity
{
    /// <value>Blueprint used to generate Equipment</value>
    public Blueprint equipmentType {get; private set;}
    /// <value>The array of parts used to to generate Equipment</value>
    public List<PartType> partsRequired {get; private set;}
    /// <value>The array of materials used to to generate Equipment</value>
    public List<RawMaterial> usedMaterials {get; private set;}
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
    public void Init(string name, Location location, Creature crafter, Creature originalOwner, Blueprint _type, List<PartType> _partsRequired, List<RawMaterial> _usedMaterials, int _price, int _durability, string _notes){
        base.Init(name,location,crafter,originalOwner);
        this.equipmentType = _type;
        this.partsRequired = _partsRequired;
        this.usedMaterials = _usedMaterials;
        this.price = _price;
        this.durability = _durability;
        this.notes = _notes;
    }
    public void Init(string name, Location location, Player crafter, Creature originalOwner, Blueprint _type, List<PartType> _partsRequired, List<RawMaterial> _usedMaterials, int _price, int _durability, string _notes){
        base.Init(name,location,crafter,originalOwner);
        this.equipmentType = _type;
        this.partsRequired = _partsRequired;
        this.usedMaterials = _usedMaterials;
        this.price = _price;
        this.durability = _durability;
        this.notes = _notes;
    }
    public void Init(string name, Location location, Player crafter, Player originalOwner, Blueprint _type, List<PartType> _partsRequired, List<RawMaterial> _usedMaterials, int _price, int _durability, string _notes){
        base.Init(name,location,crafter,originalOwner);
        this.equipmentType = _type;
        this.partsRequired = _partsRequired;
        this.usedMaterials = _usedMaterials;
        this.price = _price;
        this.durability = _durability;
        this.notes = _notes;
    }
    public void Init(string name, Location location, Creature crafter, Player originalOwner, Blueprint _type, List<PartType> _partsRequired, List<RawMaterial> _usedMaterials, int _price, int _durability, string _notes){
        base.Init(name,location,crafter,originalOwner);
        this.equipmentType = _type;
        this.partsRequired = _partsRequired;
        this.usedMaterials = _usedMaterials;
        this.price = _price;
        this.durability = _durability;
        this.notes = _notes;
    }
}
