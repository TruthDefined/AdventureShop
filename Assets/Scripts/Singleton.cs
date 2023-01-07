using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// Points to all Player manager and controller scripts for easy reference.
/// </summary>
public class Singleton : MonoBehaviour
{
    public static Singleton Instance { get; private set; }
    public PlayerInventoryManager Player_Raw_Inventory { get; private set; }
    public PlayerInventoryManager Player_Equipment_Inventory {get; private set;}
    public UICraftingController UICraftingController { get; private set; }
    public CraftingManager CraftingManager {get; private set;}
    public EntityManager EntityManager {get; private set;}
    public TimeManager TimeManager {get; private set;}
    public GameObject TooltipPrefab;
    public Creature Player;
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
        Player_Raw_Inventory = new PlayerInventoryManager();
        Player_Equipment_Inventory = new PlayerInventoryManager();
    }
    private void Start() {
        //Add all raw materials currently registered to the players inventory
        if(debug){
            foreach(RawMaterial mat in EntityManager.rawMaterials){
                Player_Raw_Inventory.Add(Generate.MaterialOfType(mat));                
            }
            //Debug.Log(TimeManager.GetCurrentDate());
        }
        
    }
    
}
