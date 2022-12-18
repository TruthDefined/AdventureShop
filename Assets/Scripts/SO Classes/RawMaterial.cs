using UnityEngine;

[CreateAssetMenu(fileName = "RawMaterial", menuName = "ScriptableObjects/RawMaterial")]
public class RawMaterial : HistoricalEntity
{
    public MaterialType type;
    public int price;
    public string notes;
    
}
