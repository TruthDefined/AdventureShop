using UnityEngine;
[CreateAssetMenu(fileName = "PartType", menuName = "ScriptableObjects/PartType")]
public class PartType : DataEntity
{
    public bool optional = false;
    public MaterialType[] acceptableMaterials;
}
