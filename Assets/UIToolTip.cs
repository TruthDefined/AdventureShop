using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIToolTip : MonoBehaviour
{
    [SerializeField]
    private TMP_Text Title;
    [SerializeField]
    private GameObject EntryTemplate;
    private List<GameObject> Entries = new List<GameObject>(); 
    private Canvas canvas;
    public GameObject tooltipPanel;
    public int heightAdjustment = 25;
    //Refactor below
    private GameObject lastHovered;
    //

    private void Awake() {
        canvas = GetComponent<Canvas>();
    }
    private void OnEnable() {
        ClearEntries();
    }

    public void ClearEntries(){
        foreach (GameObject g in Entries){
            Destroy(g);
        }
        Entries.Clear();
    }

    public void SetTitle(string title){
        Title.text = title;
    }


    public void AddNewEntry(string title, string value){
        //Create a new entry, and set it as my child
        GameObject obj = Instantiate(EntryTemplate);
        obj.transform.SetParent(transform.GetComponentInChildren<Transform>(),false);

        //Set new text values and add them to the list of children
        TMP_Text[] texts = obj.GetComponentsInChildren<TMP_Text>();
        texts[0].text = title;
        texts[1].text = value;
        Entries.Add(obj);
        
        //Resize window to account for new entry
        RectTransform t = GetComponent<RectTransform>();
        t.sizeDelta = new Vector2(t.rect.width, 25 + (heightAdjustment * Entries.Count));
    }
    public List<GameObject> GetEntries(){
        return Entries;
    }

    private void Update()
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform) canvas.transform, Input.mousePosition, canvas.worldCamera, out position);
        tooltipPanel.transform.position = canvas.transform.TransformPoint(position);
    }
    public void showTooltip(bool active){
        if(active){
            if(!tooltipPanel.activeSelf){
                StartCoroutine(UpdateTooltip(lastHovered)); 
                tooltipPanel.SetActive(true);
            }
        }
        else{
            if(tooltipPanel.activeSelf){
                tooltipPanel.SetActive(false);
            }
        }
        
    }

    public IEnumerator UpdateTooltip(GameObject currentObject){
        // _currentData = currentObject.GetComponentInChildren<UIContainer>().item.data;
        // if(_currentData){
        //     Singleton.Instance.TooltipPrefab.SetActive(true);
        //     tooltip.SetTitle(_currentData.name);
        //     tooltip.AddNewEntry("Entity Type: ",_currentData.entityType);
        // }
        
        yield break;
        //TODO: Extend to included data for... everything
    }
}
