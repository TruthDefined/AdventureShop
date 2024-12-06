using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    public delegate void OnAdventurerAdd();
    public static event OnAdventurerAdd onAdventurerAdd;
    public delegate void OnPartyAdd();
    public static event OnPartyAdd onPartyAdd;
    public delegate void OnCreatureAdd();
    public static event OnCreatureAdd onCreatureAdd;
    public delegate void OnEquipmentAdd();
    public static event OnEquipmentAdd onEquipmentAdd;
    public delegate void OnQuestAdd();
    public static event OnQuestAdd onQuestAdd;

    [SerializeField] private List<Blueprint> _blueprints;
    [SerializeField] private List<MaterialType> _materialTypes;
    [SerializeField] private List<RawMaterial> _rawMaterials;
    [SerializeField] private List<PartType> _partTypes;
    [SerializeField] private List<Ability> _abilitys;
    [SerializeField] private List<Adventurer> _adventurers;
    [SerializeField] private List<AdventurerClass> _adventurerClasses;
    [SerializeField] private List<AdventurerParty> _adventurerParties;
    [SerializeField] private List<AdventurerGuild> _adventurerGuilds;
    [SerializeField] private List<Creature> _creatures;
    [SerializeField] private List<Equipment> _equipment;
    [SerializeField] private List<Location> _locations;
    [SerializeField] private List<Quest> _quests;
    [SerializeField] private List<Species> _species;

    public List<List<DataEntity>> lists;
#region AddToLists
    public void AddAdventurer(Adventurer add){
        _adventurers.Add(add);
        //onAdventurerAdd();
    }
    public void AddGuild(AdventurerGuild add){
        _adventurerGuilds.Add(add);
    }
    public void AddParty(AdventurerParty add){
        _adventurerParties.Add(add);
        //onPartyAdd();
    }
    public void AddCreature(Creature add){
        _creatures.Add(add);
        //onCreatureAdd();
    }
    public void AddEquipment(Equipment add){
        _equipment.Add(add);
        //onEquipmentAdd();
    }
    public void AddQuest(Quest add){
        _quests.Add(add);
        //onQuestAdd();
    }

#endregion
#region GetLists
    //Cannot be set at runtime, but CAN be set at launch by mods
    //TODO: Make these editable at launch by mods
    public List<Blueprint> blueprints {get { return _blueprints;} set{print("Value cannot be set at Runtime");}}
    public List<MaterialType> materialTypes { get{ return _materialTypes;} set{ print("Value cannot be set at Runtime");}}
    public List<RawMaterial> rawMaterials{get{return _rawMaterials;}set{print("Value cannot be set at Runtime");}}
    public List<PartType> partTypes{get{return _partTypes;}set{print("Value cannot be set at Runtime");}}
    public List<Ability> abilitys{get{return _abilitys;}set{print("Value cannot be set at Runtime");}}
    public List<AdventurerClass> adventurerClasses{get{return _adventurerClasses;}set{print("Value cannot be set at Runtime");}}
    public List<Species> species{get{return _species;}set{print("Value cannot be set at Runtime");}}
    public List<Location> locations{get{return _locations;}set{print("Value cannot be set at Runtime");}}


    //Make addable via other functions
    public List<Adventurer> adventurers{get{return _adventurers;}set{print("Use AddAdventurer instead");}}
    public List<AdventurerParty> adventurerParties{get{return _adventurerParties;}set{print("Value cannot be set at Runtime");}}
    public List<AdventurerGuild> adventurerGuilds{get{return _adventurerGuilds;}set{print("Value cannot be set at Runtime");}}
    public List<Creature> creatures{get{return _creatures;}set{print("Value cannot be set at Runtime");}}
    public List<Equipment> equipment{get{return _equipment;}set{print("Value cannot be set at Runtime");}}
    public List<Quest> quests{get{return _quests;}set{print("Value cannot be set at Runtime");}}
#endregion    
    // Start is called before the first frame update
#region EDITOR
    #if UNITY_EDITOR
    public void UpdateBlueprints(List<Blueprint> newList){
        _blueprints = newList;
    }

    /// <summary>
    /// Sets List as Entity Manager Managed List
    /// </summary>
    /// <param name="newList"></param>
    /// <typeparam name="T"></typeparam>
    public void CreateLists<T>(List<T> newList){
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
            case System.Type ab when ab == typeof(List<Ability>):
                _abilitys = newList as List<Ability>;
            break;
            case System.Type cls when cls == typeof(List<AdventurerClass>):
                _adventurerClasses = newList as List<AdventurerClass>;
            break;
            case System.Type spec when spec == typeof(List<Species>):
                _species = newList as List<Species>;
            break;
            case System.Type loc when loc == typeof(List<Location>):
                _locations = newList as List<Location>;
            break;
            case System.Type adv when adv == typeof(List<Adventurer>):
                _adventurers = newList as List<Adventurer>;
            break;
            case System.Type pt when pt == typeof(List<AdventurerParty>):
                _adventurerParties = newList as List<AdventurerParty>;
            break;
            case System.Type pt when pt == typeof(List<AdventurerGuild>):
                _adventurerGuilds = newList as List<AdventurerGuild>;
            break;
            case System.Type cr when cr == typeof(List<Creature>):
                _creatures = newList as List<Creature>;
            break;
            case System.Type eq when eq == typeof(List<Equipment>):
                _equipment = newList as List<Equipment>;
            break;
            case System.Type quest when quest == typeof(List<Quest>):
                _quests = newList as List<Quest>;
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
