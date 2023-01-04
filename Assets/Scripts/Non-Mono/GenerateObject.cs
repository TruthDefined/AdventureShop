using UnityEngine;
using System.Collections.Generic;
public class Generate
{
    EntityManager manager = Singleton.Instance.EntityManager;
    public DataEntity RandomGeneric(){
        DataEntity gen = new DataEntity();
        gen.Init("Entity #" + Random.value);
        return gen;
    }
/// <summary>
/// If CREATENEW: Create new generic species
/// <br></br>
/// If !CREATENEW: Pull random species from list of all species
/// </summary>
/// <param name="createNew">""</param>
/// <returns>"Species"</returns>
    public Species RandomSpecies(bool createNew = false){
        if(createNew){
            Species gen = new Species();
            //TODO: Create rules for new species creation
            gen.Init("Species #" + Random.value);
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
    public Ability RandomAbility(bool createNew = false){
        if(createNew){
            Ability gen = new Ability();
            gen.Init("Ability #" + Random.value);
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
    public Location RandomLocation(bool createNew = false){
        if(createNew){
            Location gen = new Location();
            gen.Init("Location #" + Random.value);
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
    public AdventurerClass RandomClass(bool createNew = false){
        AdventurerClass gen = new AdventurerClass();
        if(createNew){
            gen.Init("Class #" + Random.value);
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
    public PartType RandomPartType(bool createNew = false, bool optional = true, List<MaterialType> acceptableMaterials = null){
        if(createNew){
            PartType gen = new PartType();
            bool initOptional = (optional == true)? true : false;
            List<MaterialType> initTypes;
            if(acceptableMaterials == null){
                int numMaterials = Random.Range(0,5);
                initTypes = new List<MaterialType>();
                for (int i = 0; i < numMaterials; i++)
                {
                    initTypes.Add(RandomMaterialType());
                }

            } else{
                initTypes = acceptableMaterials;
            }
            gen.Init("PartType #" + Random.value,initOptional,initTypes);
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
    public MaterialType RandomMaterialType(bool createNew = false){
        if(createNew){
            MaterialType gen = new MaterialType();
            gen.Init("MaterialType #" + Random.value);
            return gen;
        }
        else{
            return GetRandom(Singleton.Instance.EntityManager.materialTypes);
        }
    }

    // public HistoricalEntity RandomHistoric(Location historicLocation = null){
    //     HistoricalEntity gen = new HistoricalEntity();
    //     Location initLocation = (historicLocation == null) ? RandomLocation(): historicLocation;
    //     gen.Init("Historic #" + Random.value,initLocation);
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
    public Creature RandomCreature(bool createNew = false, Location historicLocation = null, Species creatureSpecies = null){
        //TODO: Define rules for new creature creation
        if(createNew){
            Creature gen = new Creature();
            Location initLocation = (historicLocation == null) ? RandomLocation() : historicLocation;
            Species initSpecies = (creatureSpecies == null) ? RandomSpecies() : creatureSpecies;
            gen.Init("Creature #" + Random.value, initLocation, initSpecies);
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
    public RawMaterial RandomMaterial(bool createNew = false, Location origin = null, Creature harvester = null, Creature originalOwner = null, MaterialType type = null, int price = 0, string notes = ""){
        if(createNew){
            RawMaterial gen = new RawMaterial();
            Location initOrigin = (origin == null) ? RandomLocation() : origin;
            Creature initHarvester = (harvester == null) ? RandomCreature() : harvester;
            Creature initOwner = (originalOwner == null) ? RandomCreature() : originalOwner;
            MaterialType initType = (type == null) ? RandomMaterialType() : type;
            int initPrice = (price == 0) ? -1 : price;
            string initNotes = (notes == "") ? "NoNotes" : notes;
            gen.Init("Material #" + Random.value,initOrigin,initHarvester,initOwner,initType,initPrice,initNotes);
            return gen;
        }
        else{
            return GetRandom(manager.rawMaterials);
        }  
    }

    public Blueprint RandomBlueprint(bool createNew = false, List<PartType> partsRequired = null){
        if(createNew){
            Blueprint gen = new Blueprint();
            List<PartType> initParts = new List<PartType>();
            if(initParts != partsRequired) initParts = partsRequired;
            else{
                int partCount = Random.Range(0,5);
                for (int i = 0; i < partCount; i++)
                {
                    initParts.Add(RandomPartType());
                }
            }
            gen.Init("Blueprint #" + Random.value,initParts);
            return gen;
        }
        else{
            return GetRandom(manager.blueprints);
        }
        
    }

    public Equipment RandomEquipment(bool createNew = false, Location origin = null, Creature crafter = null, Creature owner = null, Blueprint blueprint = null, List<PartType> parts = null, List<RawMaterial> mats = null, int price = 0, int durability = 0, string notes = ""){
        if(createNew){
            Equipment gen = new Equipment();
            Location initOrigin = (origin == null) ? RandomLocation() : origin;
            Creature initCrafter = (crafter == null) ? RandomCreature() : crafter;
            Creature initOwner = (owner == null) ? RandomCreature() : owner;
            Blueprint initBlueprint = (blueprint == null) ? RandomBlueprint() : blueprint;
            int initprice = (price == 0) ? 0 : price;
            int initDur = (durability == 0) ? 0 : durability;
            string initNotes = (notes == "") ? "" : notes;

            List<PartType> initParts = new List<PartType>();
            if(initParts != parts) initParts = parts;
            else{
                int partCount = Random.Range(0,5);
                for (int i = 0; i < partCount; i++)
                {
                    initParts.Add(RandomPartType());
                }
            }
            List<RawMaterial> initMats = new List<RawMaterial>();
            if(initMats != mats) initMats = mats;
            else{
                int partCount = Random.Range(0,5);
                for (int i = 0; i < partCount; i++)
                {
                    initMats.Add(RandomMaterial());
                }
            }
            gen.Init("Equipment #" + Random.value, initOrigin, initCrafter, initOwner, initBlueprint, initParts, initMats,initprice, initDur, initNotes);
            manager.AddEquipment(gen);
            return gen;
        }
        else{
            return GetRandom(manager.equipment);
        }
    }

    public Adventurer RandomAdventurer(bool createNew = false, Location origin = null, Species species = null, List<Equipment> startingGear = null, AdventurerClass adventuringClass = null){
        if(createNew){
            Adventurer gen = new Adventurer();
            Location initOrigin = (origin == null) ? RandomLocation() : origin;
            Species initSpecies = (species == null) ? RandomSpecies() : species;
            AdventurerClass initClass = (adventuringClass == null)? RandomClass() : adventuringClass;
            List<Equipment> initGear = new List<Equipment>();
            if(initGear == startingGear){
                //create new gear
                int numGear = Random.Range(0,5);
                initGear = new List<Equipment>();
                for (int i = 0; i < numGear; i++)
                {
                    initGear[i] = RandomEquipment();
                }
            }
            else{
                initGear = startingGear;
            }
            gen.Init("Adventurer #" + Random.value,initOrigin,initSpecies,initGear,initClass);
            manager.AddAdventurer(gen);
            return gen;
        }
        else{
            return GetRandom(manager.adventurers);
        }
    }

    public AdventurerParty RandomParty(bool createNew = false, Location location = null, List<Adventurer> adventurers = null, Sprite crest = null){
        if(createNew){
            AdventurerParty gen = new AdventurerParty();
            Location initLocation = (location == null)? RandomLocation():location;
            Sprite initSprite = (crest == null)? null: crest;
            List<Adventurer> initAdventurers = new List<Adventurer>();
            int numAdventurers = Random.Range(1,4);
            for (int i = 0; i < numAdventurers; i++)
            {
                initAdventurers.Add(RandomAdventurer(false));
            }
            gen.Init("Party #" + Random.value,initLocation,initAdventurers,initSprite);
            manager.AddParty(gen);
            return gen;
        }
        else{
            return GetRandom(manager.adventurerParties);
        }
    }
    
    //Quest +


    private T GetRandom<T>(List<T> param){
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
