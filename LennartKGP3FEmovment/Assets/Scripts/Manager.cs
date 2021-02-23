using System.Collections;
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
}
