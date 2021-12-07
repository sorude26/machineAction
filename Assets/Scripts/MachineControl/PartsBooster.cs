using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsBooster : UnitPartsMaster<BoosterData>
{   
    /// <summary> 推進力 </summary>
    public int Propulsion { get => _partsData.Propulsion[_partsID]; }    
}
