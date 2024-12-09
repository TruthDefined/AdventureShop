using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// Points to all Player manager and controller scripts for easy reference.
/// </summary>
public class Singleton : MonoBehaviour
{
    public static Singleton Instance { get; private set; }

    public PlayerGuild PlayerGuild { get; private set; }
    /// <summary>
    /// Raw Materials owned by the player.
    /// </summary>
    public PlayerInventoryManager Player_Raw_Inventory { get; private set; }
    /// <summary>
    /// Unequipped Equipment owned by the player.
    /// </summary>
    public PlayerInventoryManager Player_Equipment_Inventory {get; private set;}
    public UICraftingController UICraftingController { get; private set; }
    public CraftingManager CraftingManager {get; private set;}
    public EntityManager EntityManager {get; private set;}
    public TimeManager TimeManager {get; private set;}
    public GameObject TooltipPrefab;
    private Creature Player;
    public Sprite[] RandomAdventurerSprites;
    public bool debug = true;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        TimeManager = gameObject.AddComponent<TimeManager>();
        //If UI Manager is setup on Gameobject, grab it. Else, make a new one
        if(GetComponent<UICraftingController>()){
            UICraftingController = GetComponent<UICraftingController>();
        }
        else{
            UICraftingController = gameObject.AddComponent<UICraftingController>();
        }
        //If Crafting Manager is setup on Gameobject, grab it. Else, make a new one
        if(GetComponent<CraftingManager>()){
            CraftingManager = GetComponent<CraftingManager>();
        }
        else{
            CraftingManager = gameObject.AddComponent<CraftingManager>();
        }
        if(GetComponent<EntityManager>()){
            EntityManager = GetComponent<EntityManager>();
        }
        else{
            EntityManager = gameObject.AddComponent<EntityManager>();
        }
        //Create player inventories
        PlayerGuild = new PlayerGuild();
        Player_Raw_Inventory = PlayerGuild.inventory;
        Player_Equipment_Inventory = PlayerGuild.equipment;
        PlayerGuild.guildMaster = Player;
       
    }
    private void Start() {
        
        //Add all raw materials currently registered to the players inventory
        if(debug){
            foreach(RawMaterial mat in EntityManager.rawMaterials){
                Player_Raw_Inventory.Add(Generate.MaterialOfType(mat));
                Player_Raw_Inventory.Add(Generate.MaterialOfType(mat));                
            }
            //Debug.Log(TimeManager.GetCurrentDate());
        }
        
    }
    
}
