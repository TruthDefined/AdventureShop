using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UIInventoryItemSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image iconImage;
    public GameObject stackLabel;  
    public GameObject nameLabel;  
    private TMP_Text m_label;
    private Transform parentAfterDrag;
    private InventoryItem _item;

    public InventoryItem item{
        get{
            return _item;
        }
        set{
            _item = value;
            iconImage.sprite = _item.data.icon;
            if(m_label){
                m_label.text = _item.data.name;
            }
            stackLabel.GetComponent<TMP_Text>().text = _item.stackSize.ToString();
        }

    }

    private void Awake() {
        m_label = nameLabel.GetComponent<TMP_Text>();
    }

    public void Add(){
        var temp = Singleton.Instance.Player_Raw_Inventory.inventory.Find((x) => x.data.name == m_label.text);
        Singleton.Instance.Player_Raw_Inventory.Add(temp.data as RawMaterial);
    }
    public void Remove(){
        var temp = Singleton.Instance.Player_Raw_Inventory.inventory.Find((x) => x.data.name == m_label.text);
        Singleton.Instance.Player_Raw_Inventory.Remove(temp.data as RawMaterial);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = iconImage.transform.parent;
        iconImage.transform.SetParent(transform.root);
        iconImage.transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        iconImage.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        print("EndDrag");
        iconImage.transform.SetParent(parentAfterDrag);
        // iconImage.transform.SetPositionAndRotation(parentAfterDrag.position, parentAfterDrag.rotation);
    }
}
