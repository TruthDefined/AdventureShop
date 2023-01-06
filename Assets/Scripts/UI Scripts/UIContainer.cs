using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class UIContainer : MonoBehaviour
{
    public Image iconImage;
    protected TMP_Text m_label;

    [HideInInspector]
    public Transform parentAfterDrag;
    protected InventoryItem _item;
    public InventoryItem item{
        get{
            return _item;
        }
        set{
            _item = value;
            iconImage.sprite = _item.icon;
            if(m_label){
                m_label.text = _item.containedItem;
            }
        }
    }
}
