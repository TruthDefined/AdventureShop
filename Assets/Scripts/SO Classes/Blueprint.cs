using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "Blueprint", menuName = "ScriptableObjects/Blueprint")]
/// <summary>
/// A Blueprint contains an array of Parts that are required to make a piece of Equipment
/// </summary>
public class Blueprint : DataEntity
{
    /// <value>
    /// The partsRequired property represents all part types that can be used in creating equipment from this blueprint
    /// </value>
    public List<PartType> partsRequired;
    
    public void Init(string name, List<PartType> partsRequired){
        base.Init(name);
        this.partsRequired = partsRequired;
    }
}
