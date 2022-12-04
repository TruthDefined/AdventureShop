using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIInventoryItemSlot : MonoBehaviour
{
    public Image iconImage;
    public GameObject stackLabel;    
    private TMP_Text m_label;

    private void Awake() {
        m_label = GetComponent<TMP_Text>();
    }
    public void Set(InventoryItem item){
        iconImage.sprite = item.data.icon;
        m_label.text = item.data.name;
        // if(item.stackSize <= 0){
        //     m_stackObject.SetActive(false);
        //     return;
        // }
        stackLabel.GetComponent<TMP_Text>().text = item.stackSize.ToString();
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
