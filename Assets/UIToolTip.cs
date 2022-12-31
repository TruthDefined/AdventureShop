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
    public int heightAdjustment = 25;

    private void Awake() {
        canvas = transform.root.GetComponent<Canvas>();
    }
    private void OnEnable() {
        ClearEntries();

        // SetTitle("Bron Johnson");

        // AddNewEntry("Species:", "Human");
        // AddNewEntry("Class:", "Fighter");

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
        obj.transform.SetParent(transform,false);

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

    void Update()
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform) canvas.transform, Input.mousePosition, canvas.worldCamera, out position);
        transform.position = canvas.transform.TransformPoint(position);
    }
}
