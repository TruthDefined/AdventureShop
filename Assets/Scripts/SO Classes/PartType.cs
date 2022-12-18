using UnityEngine;
[CreateAssetMenu(fileName = "PartType", menuName = "ScriptableObjects/PartType")]
public class PartType : DataEntity
{
    public bool optional = false;
    public MaterialType[] acceptableMaterials;

    public void Init(string name, bool optional, MaterialType[] acceptableMaterials){
        base.Init(name);
        this.optional = optional;
        this.acceptableMaterials = acceptableMaterials;
    }
}
