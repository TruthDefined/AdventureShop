using UnityEngine;

[CreateAssetMenu(fileName = "RawMaterial", menuName = "ScriptableObjects/RawMaterial", order = 4)]
public class RawMaterial : HistoricalEntity
{
    public MaterialType type;
    public int price;
    public string notes;
    
}
