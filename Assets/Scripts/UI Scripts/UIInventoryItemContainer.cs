using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UIInventoryItemContainer : UIContainer
{
    public GameObject stackLabel;  
    public GameObject nameLabel;  


    private void Awake() {
        m_label = nameLabel.GetComponent<TMP_Text>();
    }

    //TODO: FIX ADD/REMOVE
    // public void Add(RawMaterial raw){
    //     if(Singleton.Instance.Player_Raw_Inventory.inventory.TryGetValue(m_label.text, out InventoryItem temp)){
    //         Singleton.Instance.Player_Raw_Inventory.Add(temp.data as RawMaterial);
    //     }
    //     //.Find((x) => x.data.name == m_label.text);
        
    // }
    // public void Remove(){
    //     //var temp = Singleton.Instance.Player_Raw_Inventory.inventory.Find((x) => x.data.name == m_label.text);
    //     //Singleton.Instance.Player_Raw_Inventory.Remove(temp.data as RawMaterial);
    // }

    new public InventoryItem item{
        get{
            return _item;
        }
        set{
            _item = value;
            iconImage.sprite = _item.icon;
            if(m_label){
                m_label.text = _item.containedItem;
            }
            stackLabel.GetComponent<TMP_Text>().text = _item.data.Count.ToString();
        }
    }

    // public void OnBeginDrag(PointerEventData eventData)
    // {
    //     parentAfterDrag = iconImage.transform.parent;
    //     iconImage.transform.SetParent(transform.root);
    //     iconImage.transform.SetAsLastSibling();
    // }

    // public void OnDrag(PointerEventData eventData)
    // {
    //     iconImage.transform.position = Input.mousePosition;
    // }

    // public void OnEndDrag(PointerEventData eventData)
    // {
    //     print("EndDrag");
    //     iconImage.transform.SetParent(parentAfterDrag);
    //     // iconImage.transform.SetPositionAndRotation(parentAfterDrag.position, parentAfterDrag.rotation);
    // }

    // public void OnDrop(PointerEventData eventData)
    // {
    //     GameObject dropped = eventData.pointerDrag;
    // }
}
