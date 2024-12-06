using UnityEngine;

[CreateAssetMenu(fileName = "RawMaterial", menuName = "ScriptableObjects/RawMaterial")]
public class RawMaterial : HistoricalEntity
{
    public MaterialType type;
    public int price;
    public string notes;
    public Color color;
    
    public void Init(string name, Location location, Creature harvester, Creature originalOwner, MaterialType type, int price, string notes){
        base.Init(name,location,harvester,originalOwner);
        this.type = type;
        this.price = price;
        this.notes = notes;
        this.icon = type.icon;
        this.color = Color.white;
    }
    public void Init(string name, Location location, Creature harvester, Creature originalOwner, MaterialType type, int price, string notes, Color color){
        base.Init(name,location,harvester,originalOwner);
        this.type = type;
        this.price = price;
        this.notes = notes;
        this.icon = type.icon;
        this.color = color;
    }
}
