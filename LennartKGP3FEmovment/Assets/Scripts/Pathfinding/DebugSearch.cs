using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSearch : SearchBase
{
	protected override void InitializeSearch()
	{
		_startNode = _unit.ClosestGridNode;
		

		foreach (GridNode gridNode in _visited.Keys)
		{
			gridNode.Reset();
		}

		_openList = new List<GridNode>();
		_visited = new Dictionary<GridNode, GridNode>();

		_openList.Add(_startNode);
	}

	protected override bool StepToGoal()
	{
		GridNode current = _openList[0];

		// goal found
		if (current == _goalNode)
		{
			return true;
		}

		foreach (GridNode next in current.Neighbours)
		{
			if (next.IsWall)
			{
				continue;
			}

			if (!_visited.ContainsKey(next))
			{
				_openList.Add(next);
				_visited.Add(next, current);
				next.SetGridNodeSearchState(GridNodeSearchState.Queue);
			}
		}

		_openList.Remove(current);
		current.SetGridNodeSearchState(GridNodeSearchState.Processed);
		// not yet finished
		return false;
	}
}
