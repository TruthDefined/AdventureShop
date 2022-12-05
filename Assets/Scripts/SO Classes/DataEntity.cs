using UnityEngine;

/// <summary>
/// This is the core class for anything that will be contained within an inventory and will be displayed with an icon
/// It contains an ID, an icon, and a list of abilities
/// </summary>

public class DataEntity: ScriptableObject 
{
    public string id;
    public Sprite icon;
    public Ability[] abilities;

    private void OnValidate() {
        id = GetType().ToString();
    }
}
