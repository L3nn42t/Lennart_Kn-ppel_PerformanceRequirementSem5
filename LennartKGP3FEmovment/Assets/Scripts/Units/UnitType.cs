using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "FEMovment", menuName = "Units/New UnitType", order = 200)]
public class UnitType : ScriptableObject
{
    public int Groundcost;
    public int Wallcost;
    public int Forestcost;
    public int RoughCost;
}
