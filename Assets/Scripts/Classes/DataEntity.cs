using UnityEngine;

public class DataEntity: ScriptableObject 
{
    public string id;
    public Sprite icon;
    public Ability[] abilities;

    private void OnValidate() {
        id = GetType().ToString();
    }
}
