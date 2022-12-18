using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// This is the core class for anything that will be contained within an inventory and will be displayed with an icon
/// It contains an ID, an icon, and a list of abilities
/// </summary>

public class DataEntity: ScriptableObject 
{
    public string entityType = "";
    public Sprite icon;
    public List<Ability> abilities = new List<Ability>();

    private void OnValidate() {
        entityType = GetType().ToString();
    }

    public void Init(string name){
        this.name = name;
    }
}
