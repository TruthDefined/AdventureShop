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
    private InventoryItem tooltipEntity;
    public bool showHistory = false;

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
        //TODO: Could make this more efficient by enable.disable and setting values instead of instantiating and deleting entries.
        //Create a new entry, and set it as my child
        GameObject obj = Instantiate(EntryTemplate);
        obj.transform.SetParent(tooltipPanel.transform,false);

        //Set new text values and add them to the list of children
        TMP_Text[] texts = obj.GetComponentsInChildren<TMP_Text>();
        texts[0].text = title;
        texts[1].text = value;
        Entries.Add(obj);
        
        //Resize window to account for new entry
        RectTransform t = tooltipPanel.GetComponent<RectTransform>();
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
    public void showTooltip(InventoryItem item, bool active){
        tooltipEntity = item;
        if(active){
            if(!tooltipPanel.activeSelf){
                StartCoroutine(UpdateTooltip(tooltipEntity)); 
                tooltipPanel.SetActive(true);
            }
        }
        else{
            if(tooltipPanel.activeSelf){
                tooltipPanel.SetActive(false);
                ClearEntries();
            }
        }
        
    }

    public IEnumerator UpdateTooltip(InventoryItem currentObject){
        
        Singleton.Instance.TooltipPrefab.SetActive(true);
        SetTitle(currentObject.data[0].name);
        string type = currentObject.data[0].entityType;
        AddNewEntry("Entity Type: ", type);
    
        switch(type){
            case"AdventurerParty":
                AdventurerParty aP = currentObject.data[0] as AdventurerParty;
                foreach (Adventurer a in aP.adventurers){
                    AddNewEntry( a.name, a.species.name + " " + a.adventurerClass.name);
                }
                break;
            case "RawMaterial":
                Debug.Log("Recognized Raw Material");
                    foreach(DataEntity d in currentObject.data){
                        RawMaterial mat = d as RawMaterial;
                        AddNewEntry("Material: ", mat.name);
                        AddNewEntry("Original Owner: ", mat.GetOrigin[2].name);
                    }
                break;
            case "Adventurer":
                Adventurer adv = currentObject.data[0] as Adventurer;
                AddNewEntry("Species: ", adv.species.name);
                AddNewEntry("Class: ", adv.adventurerClass.name);

                break;
            default:
            break;
        }

        if(showHistory && currentObject.data[0] as HistoricalEntity){
            HistoricalEntity historicalObject = currentObject.data[0] as HistoricalEntity;
            foreach(DataEntity historicalEntry in historicalObject.GetHistory){
                AddNewEntry("Owner: ", historicalEntry.name);
            }
        }




        
        yield break;
        //TODO: Extend to included data for... everything
    }
}
