using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryController : MonoBehaviour
{

    public GameObject m_slotPrefab;
    public InventoryType type;
    public GameObject owner;
    private void Start() {
        InventoryManager.onInventoryChangedEvent += OnUpdateInventory;
    }

    private void OnUpdateInventory(){
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        DrawInventory();
    }

    private void DrawInventory(){
        switch (type){
            case InventoryType.Equipment:
                if(!owner){
                    foreach (InventoryItem item in Singleton.Instance.Player_Equipment_Inventory.inventory)
                    {
                        AddInventorySlot(item);
                    }
                }
                break;
            case InventoryType.RawMaterials:
                if(!owner){
                    foreach (InventoryItem item in Singleton.Instance.Player_Raw_Inventory.inventory)
                    {
                        AddInventorySlot(item);
                    }
                }
                break;
            default:
                break;
        }



        foreach (InventoryItem item in Singleton.Instance.Player_Equipment_Inventory.inventory)
        {
            AddInventorySlot(item);
        }
    }

    private void AddInventorySlot(InventoryItem item){
        GameObject obj = Instantiate(m_slotPrefab);
        obj.transform.SetParent(transform,false);

        UIInventoryItemSlot slot = obj.GetComponent<UIInventoryItemSlot>();
        slot.Set(item);
    }
}
