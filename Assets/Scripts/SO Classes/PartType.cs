using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "PartType", menuName = "ScriptableObjects/PartType")]
public class PartType : DataEntity
{
    public bool optional = false;
    public List<MaterialType> acceptableMaterials;

    public void Init(string name, bool optional, List<MaterialType> acceptableMaterials){
        base.Init(name);
        this.optional = optional;
        this.acceptableMaterials = acceptableMaterials;
    }
}
