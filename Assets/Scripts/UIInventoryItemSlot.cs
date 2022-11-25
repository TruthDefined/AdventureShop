using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIInventoryItemSlot : MonoBehaviour
{
    private Sprite m_icon;
    private TMP_Text m_label;
    public GameObject m_stackObject;
    private TMP_Text m_stackLabel;

    private void Awake() {
        //m_icon = GetComponent<Sprite>();
        m_label = GetComponent<TMP_Text>();
        m_stackLabel = m_stackObject.GetComponent<TMP_Text>();
    }


    public void Set(InventoryItem item){
        //m_icon = item.data.icon;
        m_label.text = item.data.name;
        if(item.stackSize <= 1){
            m_stackObject.SetActive(false);
            return;
        }
        m_stackLabel.text = item.stackSize.ToString();
    }

    public void Add(){
        var temp = Singleton.Instance.Player_Raw_Inventory.inventory.Find((x) => x.data.name == m_label.text);
        Singleton.Instance.Player_Raw_Inventory.Add(temp.data as RawMaterial);
    }
    public void Remove(){
        var temp = Singleton.Instance.Player_Raw_Inventory.inventory.Find((x) => x.data.name == m_label.text);
        Singleton.Instance.Player_Raw_Inventory.Remove(temp.data as RawMaterial);
    }
}
