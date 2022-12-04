using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private UIInventoryItemSlot _mouseItem;

    // Start is called before the first frame update
    private void Awake() {
        canvas = transform.root.GetComponent<Canvas>();
        _mouseItem = GetComponentInChildren<UIInventoryItemSlot>(); 
    }

    public void SetData(InventoryItem inventoryItem){
        _mouseItem.item = inventoryItem;
    }

    public void Toggle (bool val){
        gameObject.SetActive(val);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform) canvas.transform, Input.mousePosition, canvas.worldCamera, out position);
        transform.position = canvas.transform.TransformPoint(position);
    }
}
