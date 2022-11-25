using UnityEngine;

[CreateAssetMenu(fileName = "RawMaterial", menuName = "ScriptableObjects/RawMaterial", order = 4)]
public class RawMaterial : DataEntity
{
    public MaterialType type;
    public int price;
    public string notes;
}
