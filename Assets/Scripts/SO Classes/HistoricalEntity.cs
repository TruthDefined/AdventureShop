using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the core class for anything that has an origin and ahistory of owners.
/// </summary>
public class HistoricalEntity : DataEntity
{

    private DataEntity[] _origin = new DataEntity[3];
    private bool originLock = false;
    private List<DataEntity> _history = new List<DataEntity>();
    /// <summary>
    /// Origin of Entity
    /// </summary>
    /// <value>
    /// [0] = Location of Origin (Ancient Forest) <br></br>
    /// [1] = Harvester of Entity (Dwor the Dwarf) <br></br>
    /// [2] = Original Owner (Dagon the Dragon)
    /// </value>
    public DataEntity[] GetOrigin{
        get{
            return _origin;
        }
    }
    /// <summary>
    /// Origin of Entity
    /// </summary>
    /// <param name="location">Location of Origin (Ancient Forest)</param>
    /// <param name="harvester">[1] = Harvester of Entity (Dwor the Dwarf)</param>
    /// <param name="OriginalOwner">[2] = Original Owner (Dagon the Dragon)</param>
    /// <returns>True if new values are stored</returns>
    public bool SetOrigin(DataEntity location, DataEntity harvester, DataEntity OriginalOwner = null){
        if(!originLock){
            _origin[0] = location;
            _origin[1] = harvester;
            _origin[2] = OriginalOwner;
            originLock = true;
            return true;
        } 
        else{
            Debug.Log("Origin already set");
            return false;
        }
        
    }
    /// <summary>
    /// Returns a list of all previous holders of this object
    /// </summary>
    /// <value></value>
    public List<DataEntity> GetHistory{
        get{
            return _history;
        }
    }

    public bool AddToHistory(DataEntity newHistory){
        try
        {
            _history.Add(newHistory);
            return true;
        }
        catch (System.Exception)
        {
            return false;
        }
        
    }

}
