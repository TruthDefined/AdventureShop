using UnityEngine;
[CreateAssetMenu(fileName = "Blueprint", menuName = "ScriptableObjects/Blueprint", order = 1)]
public class Blueprint : DataEntity
{
    public PartType[] partsRequired;
}
