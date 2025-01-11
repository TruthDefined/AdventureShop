using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : HistoricalEntity
{
   public List<string> historyNotes = new List<string>();
   public string Instructions;
   public List<DataEntity> keywords;


   public void Init(string name, Location location, Creature issuer, List<DataEntity> keywords, string Instructions){
    base.Init(name, location, issuer);
    this.Instructions = Instructions;
    this.keywords = keywords;
   }
   public void Init(string name, Location location, Player issuer, List<DataEntity> keywords, string Instructions){
    base.Init(name, location, issuer);
    this.Instructions = Instructions;
    this.keywords = keywords;
   }
}
