using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UEGP3PR
{
    public class PlayerUnit : Unit
    {

        public GameObject highlight; //was in unit but is only relevant with player units, therefore was added here
        ////private void OnMouseDown()
        ////{
        ////    if (_isselected)
        ////    {
        ////        highlight.SetActive(false);
        ////    }
        ////}

        public void Update() // messy, but only Playerunit is intended to use the highlight
        {
            if (_targetNode == null)
            {
                highlight.SetActive(false);
            }
            else
            {
                highlight.SetActive(true);
                highlight.transform.position = _targetNode.transform.position;
                if (manager.selectedUnit = this) //i hate using so many if statements, but they don't accept multible parameters
                {
                    if (!pathfound && _targetNode._useState == GridNodeUseState.walkable)
                    {
                        UnitMovePath();
                    }




                }
            }
            if (!movedthisRound && pathfound)
            {
                if (canmove == true)
                {
                    MovetoTile();
                    canmove = false;
                }
            }

        }
    }

}
