using UnityEngine;
[CreateAssetMenu(fileName = "PartType", menuName = "ScriptableObjects/PartType", order = 2)]
public class PartType : DataEntity
{
    public bool optional = false;
    public MaterialType[] acceptableMaterials;
}
