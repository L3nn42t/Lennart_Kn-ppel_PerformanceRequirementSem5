using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovmentRangeSearch : SearchBase
{
	// Breath first search
	protected override void InitializeSearch()
	{

		_startNode = _unit.ClosestGridNode; //add unit position here
		

		foreach (GridNode gridNode in _visited.Keys)
		{
			gridNode.Reset();
		}

		_openList = new List<GridNode>();
		_visited = new Dictionary<GridNode, GridNode>();

		_openList.Add(_startNode);
		_startNode.CostSoFar = 0;
	}

	protected override bool StepToGoal()
	{
		Debug.Log("Started");
		GridNode current = _openList[0];
		Debug.Log(current);

		// end reached
	

		foreach (GridNode next in current.Neighbours)
		{
 		 float newCost = current.CostSoFar + next.Cost;
			
			if (!_visited.ContainsKey(next))
			{
				// only use nextnode if cost less or equal to movmentpoints
				if (newCost <= _unit.MovmentPoints)
				{
					Debug.Log(newCost);
					_openList.Add(next);
					_visited.Add(next, current);
					next.CostSoFar = newCost;
					next.SetGridNodeSearchState(GridNodeSearchState.Queue);
				}
				else
				{
					Debug.Log("to costly");
					continue;
				}
				
			}		
		}
		_openList.Remove(current);
		current.SetGridNodeUseState(GridNodeUseState.walkable);
		current.SetGridNodeSearchState(GridNodeSearchState.Processed);
		if (_openList.Count == 0)
		{
			_unit._walkable = this._visited;
			Debug.Log("IsDone");
			return true;
		}
		else
		{
			// not yet finished
			Debug.Log("not finished");
			return false;
		}
	

	}
	

}
