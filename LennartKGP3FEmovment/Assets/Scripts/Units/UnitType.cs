using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UEGP3PR
{
    [CreateAssetMenu(fileName = "FEMovment", menuName = "Units/New UnitType", order = 200)]
    public class UnitType : ScriptableObject
    {// just a container for costof tile, implemented in Gridnode and Unit.
        public int Groundcost;
        public int Wallcost;
        public int Forestcost;
        public int RoughCost;
    }
}

