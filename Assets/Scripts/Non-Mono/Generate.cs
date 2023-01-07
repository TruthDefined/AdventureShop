using UnityEngine;
using System.Collections.Generic;
public static class Generate
{
    static EntityManager manager = Singleton.Instance.EntityManager;
    static public DataEntity RandomGeneric(){
        DataEntity gen = ScriptableObject.CreateInstance<DataEntity>();
        gen.Init("Entity #" + Random.Range(0,1000));
        return gen;
    }
/// <summary>
/// If CREATENEW: Create new generic species
/// <br></br>
/// If !CREATENEW: Pull random species from list of all species
/// </summary>
/// <param name="createNew">""</param>
/// <returns>"Species"</returns>
    static public Species RandomSpecies(bool createNew = false){
        if(createNew){
            Species gen = ScriptableObject.CreateInstance<Species>();
            //TODO: Create rules for new species creation
            gen.Init("Species #" + Random.Range(0,1000));
            return gen;
        }
        else{
            return GetRandom(manager.species);
        }
    }
    /// <summary>
    /// If CREATENEW: Create new generic ability
    /// <br></br>
    /// If !CREATENEW: return random ability from list of all ability
    /// </summary>
    /// <param name="createNew">""</param>
    /// <returns>"Ability"</returns>
    static public Ability RandomAbility(bool createNew = false){
        if(createNew){
            Ability gen = ScriptableObject.CreateInstance<Ability>();
            gen.Init("Ability #" + Random.Range(0,1000));
            return gen;
        }
        else{
            return GetRandom(manager.abilitys);
        }  
    }
    /// <summary>
    /// If CREATENEW: Create new generic location
    /// <br></br>
    /// If !CREATENEW: Pull random location from list of all location
    /// </summary>
    /// <param name="createNew">""</param>
    /// <returns>"Location"</returns>
    static public Location RandomLocation(bool createNew = false){
        if(createNew){
            Location gen = ScriptableObject.CreateInstance<Location>();
            gen.Init("Location #" + Random.Range(0,1000));
            return gen;
        }
        else{
            return GetRandom(manager.locations);
        } 
    }
    /// <summary>
    /// If CREATENEW: Create new generic adventuring class
    /// <br></br>
    /// If !CREATENEW: Pull random class from list of all classes
    /// </summary>
    /// <param name="createNew">""</param>
    /// <returns>"Adventurer Class"</returns>
    static public AdventurerClass RandomClass(bool createNew = false){
        AdventurerClass gen = ScriptableObject.CreateInstance<AdventurerClass>();
        if(createNew){
            gen.Init("Class #" + Random.Range(0,1000));
            return gen;
        }
        else{
            return GetRandom(manager.adventurerClasses);
        }   
    }
    /// <summary>
    /// If CREATENEW: Create new generic Part Tyle
    /// <br></br>
    /// If !CREATENEW: Pull random type from list of all part types
    /// </summary>
    /// <param name="createNew"></param>
    /// <param name="optional"></param>
    /// <param name="acceptableMaterials"></param>
    /// <returns></returns>
    static public PartType RandomPartType(bool createNew = false, bool optional = true, List<MaterialType> acceptableMaterials = null){
        if(createNew){
            PartType gen = ScriptableObject.CreateInstance<PartType>();
            bool initOptional = (optional == true)? true : false;
            List<MaterialType> initTypes = new List<MaterialType>();
            if(acceptableMaterials == null){
                int numMaterials = Random.Range(1,5);
                initTypes = new List<MaterialType>();
                for (int i = 0; i < numMaterials; i++)
                {
                    initTypes.Add(RandomMaterialType(createNew));
                }

            } else{
                initTypes = acceptableMaterials;
            }
            gen.Init("PartType #" + Random.Range(0,1000),initOptional,initTypes);
            return gen;
        }
        else{
            return GetRandom(manager.partTypes);
        }
    }
    /// <summary>
    /// If CREATENEW: Create new generic material type
    /// <br></br>
    /// If !CREATENEW: return random type from list of all material types
    /// </summary>
    /// <param name="createNew">""</param>
    /// <returns>"Material Type"</returns>
    static public MaterialType RandomMaterialType(bool createNew = false){
        if(createNew){
            MaterialType gen = ScriptableObject.CreateInstance<MaterialType>();
            gen.Init("MaterialType #" + Random.Range(0,1000));
            return gen;
        }
        else{
            return GetRandom(Singleton.Instance.EntityManager.materialTypes);
        }
    }

    // public HistoricalEntity RandomHistoric(Location historicLocation = null){
    //     HistoricalEntity gen = new HistoricalEntity();
    //     Location initLocation = (historicLocation == null) ? RandomLocation(): historicLocation;
    //     gen.Init("Historic #" + Random.Range(0,1000),initLocation);
    //     return gen;
    // }
    /// <summary>
    /// If CREATENEW: Create new generic creature
    /// <br></br>
    /// If !CREATENEW: return random creature from list of all creatures
    /// </summary>
    /// <param name="createNew">""</param>
    /// <param name="historicLocation"></param>
    /// <param name="creatureSpecies"></param>
    /// <returns>Creature</returns>
    static public Creature RandomCreature(bool createNew = false, Location historicLocation = null, Species creatureSpecies = null){
        //TODO: Define rules for new creature creation
        if(createNew){
            Creature gen = ScriptableObject.CreateInstance<Creature>();
            Location initLocation = (historicLocation == null) ? RandomLocation(createNew) : historicLocation;
            Species initSpecies = (creatureSpecies == null) ? RandomSpecies(createNew) : creatureSpecies;
            gen.Init("Creature #" + Random.Range(0,1000), initLocation, initSpecies);
            manager.AddCreature(gen);
            return gen;
        }
        else{
            return GetRandom(manager.creatures);
        }
        
    }
    /// <summary>
    /// If CREATENEW: Create new generic material
    /// <br></br>
    /// If !CREATENEW: return random material from list of all materials
    /// </summary>
    /// <param name="createNew"></param>
    /// <param name="origin"></param>
    /// <param name="harvester"></param>
    /// <param name="originalOwner"></param>
    /// <param name="type"></param>
    /// <param name="price"></param>
    /// <param name="notes"></param>
    /// <returns>Material</returns>
    static public RawMaterial RandomMaterial(bool createNew = false, Location origin = null, Creature harvester = null, Creature originalOwner = null, MaterialType type = null, int price = 0, string notes = ""){
        if(createNew){
            RawMaterial gen = ScriptableObject.CreateInstance<RawMaterial>();
            Location initOrigin = (origin == null) ? RandomLocation(createNew) : origin;
            Creature initHarvester = (harvester == null) ? RandomCreature(createNew) : harvester;
            Creature initOwner = (originalOwner == null) ? RandomCreature(createNew) : originalOwner;
            MaterialType initType = (type == null) ? RandomMaterialType(createNew) : type;
            int initPrice = (price == 0) ? -1 : price;
            string initNotes = (notes == "") ? "NoNotes" : notes;
            gen.Init("Material #" + Random.Range(0,1000),initOrigin,initHarvester,initOwner,initType,initPrice,initNotes);
            return gen;
        }
        else{
            return GetRandom(manager.rawMaterials);
        }  
    }

    static public RawMaterial MaterialOfType(RawMaterial material){
        RawMaterial newRaw = ScriptableObject.CreateInstance<RawMaterial>();
        newRaw.Init(material.name,RandomLocation(true),RandomCreature(true),RandomCreature(true),material.type,material.price,material.notes);
        return newRaw;
    }

    static public Blueprint RandomBlueprint(bool createNew = false, List<PartType> partsRequired = null){
        if(createNew){
            Blueprint gen = ScriptableObject.CreateInstance<Blueprint>();
            List<PartType> initParts = new List<PartType>();
            if(initParts != partsRequired) initParts = partsRequired;
            else{
                int partCount = Random.Range(0,5);
                for (int i = 0; i < partCount; i++)
                {
                    initParts.Add(RandomPartType(createNew));
                }
            }
            gen.Init("Blueprint #" + Random.Range(0,1000),initParts);
            return gen;
        }
        else{
            return GetRandom(manager.blueprints);
        }
        
    }

    static public Equipment RandomEquipment(bool createNew = false, Location origin = null, Creature crafter = null, Creature owner = null, Blueprint blueprint = null, List<PartType> parts = null, List<RawMaterial> mats = null, int price = 0, int durability = 0, string notes = ""){
        if(createNew){
            Equipment gen = ScriptableObject.CreateInstance<Equipment>();
            Location initOrigin = (origin == null) ? RandomLocation(createNew) : origin;
            Creature initCrafter = (crafter == null) ? RandomCreature(createNew) : crafter;
            Creature initOwner = (owner == null) ? RandomCreature(createNew) : owner;
            Blueprint initBlueprint = (blueprint == null) ? RandomBlueprint(createNew) : blueprint;
            int initprice = (price == 0) ? 0 : price;
            int initDur = (durability == 0) ? 0 : durability;
            string initNotes = (notes == "") ? "" : notes;
            int partCount = Random.Range(1,5);
            List<PartType> initParts = new List<PartType>();
            if(parts != null) initParts = parts;
            else{
                
                for (int i = 0; i < partCount; i++)
                {
                    initParts.Add(RandomPartType(createNew));
                }
            }
            List<RawMaterial> initMats = new List<RawMaterial>();
            if(mats != null) initMats = mats;
            else{
                for (int i = 0; i < partCount; i++)
                {
                    initMats.Add(RandomMaterial(createNew));
                }
            }
            gen.Init("Equipment #" + Random.Range(0,1000), initOrigin, initCrafter, initOwner, initBlueprint, initParts, initMats,initprice, initDur, initNotes);
            manager.AddEquipment(gen);
            return gen;
        }
        else{
            return GetRandom(manager.equipment);
        }
    }

    static public Adventurer RandomAdventurer(bool createNew = false, Location origin = null, Species species = null, List<Equipment> startingGear = null, AdventurerClass adventuringClass = null){
        if(createNew){
            Adventurer gen = ScriptableObject.CreateInstance<Adventurer>();
            Location initOrigin = (origin == null) ? RandomLocation(createNew) : origin;
            Species initSpecies = (species == null) ? RandomSpecies(createNew) : species;
            AdventurerClass initClass = (adventuringClass == null)? RandomClass(createNew) : adventuringClass;
            List<Equipment> initGear = new List<Equipment>();
            if(initGear == startingGear){
                //create new gear
                int numGear = Random.Range(0,5);
                initGear = new List<Equipment>();
                for (int i = 0; i < numGear; i++)
                {
                    initGear[i] = RandomEquipment(createNew);
                    //gen.equipment.Add(initGear[i]);
                    initGear[i].AddToHistory(gen);
                    Debug.Log("History count: " + initGear[i].GetHistory.Count);
                }
            }
            else{
                initGear = startingGear;
            }
            gen.Init("Adventurer #" + Random.Range(0,1000),initOrigin,initSpecies,initGear,initClass);
            manager.AddAdventurer(gen);
            return gen;
        }
        else{
            return GetRandom(manager.adventurers);
        }
    }

    static public AdventurerParty RandomParty(bool createNew = false, Location location = null, List<Adventurer> adventurers = null, Sprite crest = null){
        if(createNew){
            AdventurerParty gen = ScriptableObject.CreateInstance<AdventurerParty>();
            Location initLocation = (location == null)? RandomLocation(createNew):location;
            Sprite initSprite = (crest == null)? null: crest;
            List<Adventurer> initAdventurers = new List<Adventurer>();
            int numAdventurers = Random.Range(2,4);
            for (int i = 0; i < numAdventurers; i++)
            {
                initAdventurers.Add(RandomAdventurer(createNew));
            }
            gen.Init("Party #" + Random.Range(0,1000),initLocation,initAdventurers,initSprite);
            manager.AddParty(gen);
            return gen;
        }
        else{
            return GetRandom(manager.adventurerParties);
        }
    }
    
    //Quest +


    static private T GetRandom<T>(List<T> param){
        int randomIndex = Random.Range(0,param.Count);
        return param[randomIndex];
    }

//Not needed, but works!
    // private T AssignIfNull<T>( T param) where T : class{

    //     if(param != null){
    //         return param;
    //     }

    //     if(typeof(T) == typeof(Species)){
    //         Species s = RandomSpecies();
    //         return s as T;
    //     }
    //     if(typeof(T) == typeof(Ability)){
    //         Ability s = RandomAbility();
    //         return s as T;
    //     }
    //     if(typeof(T) == typeof(Location)){
    //         Location s = RandomLocation();
    //         return s as T;
    //     }
    //     if(typeof(T) == typeof(MaterialType)){
    //         MaterialType s = RandomMaterialType();
    //         return s as T;
    //     }
    //     if(typeof(T) == typeof(HistoricalEntity)){
    //         HistoricalEntity s = RandomHistoric();
    //         return s as T;
    //     }
    //     if(typeof(T) == typeof(Creature)){
    //         Creature s = RandomCreature();
    //         return s as T;
    //     }
    //     if(typeof(T) == typeof(RawMaterial)){
    //         RawMaterial s = RandomMaterial();
    //         return s as T;
    //     }
    //     if(typeof(T) == typeof(DataEntity)){
    //         DataEntity s = RandomGeneric();
    //         return s as T;
    //     }
    //     return null;
    // }
}
