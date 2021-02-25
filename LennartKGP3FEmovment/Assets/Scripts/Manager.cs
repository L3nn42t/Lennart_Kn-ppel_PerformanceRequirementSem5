﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public Unit selectedUnit;
    public bool _isunitselected;
    public Grid _grid;

    public void ResetNodes()
    {
        ResetNodeSearchState();
        ResetNodeUseState();
    }

    public void ResetNodeUseState()
    {
        foreach(GridNode node in _grid._nodes)
        {
            node.SetGridNodeUseState(GridNodeUseState.none);
        }
    }
    public void ResetNodeSearchState()
    {
        foreach (GridNode node in _grid._nodes)
        {
            node.SetGridNodeSearchState(GridNodeSearchState.None);
        }
    }
    public void SetNodetoDanger()// it is easier to implement this here instead on the enemy unit
    {
        foreach(GridNode node in _grid._nodes)
        {
            if(node._useState == GridNodeUseState.walkable)
            {
                node.SetGridNodeUseState(GridNodeUseState.danger);
            }
            else if(node._useState == GridNodeUseState.attackable)
            {
                node.SetGridNodeUseState(GridNodeUseState.danger);
            }
        }
    }
    public void GiveNodeCost()
    {
        foreach(GridNode gridNode in _grid._nodes)
        {
            if(selectedUnit != null)
            {
                gridNode.forestcost = selectedUnit._type.Forestcost;
                gridNode.wallcost = selectedUnit._type.Wallcost;
                gridNode.groundcost = selectedUnit._type.Groundcost;
                gridNode.roughcost = selectedUnit._type.RoughCost;
            }
            else //use this if no unit is selected, 
            {
                gridNode.forestcost = 1;
                gridNode.wallcost = 1;
                gridNode.groundcost = 1;
                gridNode.roughcost = 1;
            }
            
        }
    }
}
