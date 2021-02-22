﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRangeSearch : SearchBase
{
	protected override void InitializeSearch()
	{

		_openList = new List<GridNode>();
		_visited = new Dictionary<GridNode, GridNode>();
		_visited = _unit._walkable;
		Debug.Log("active");

		foreach (GridNode gridNode in _visited.Keys)
		{
			foreach(GridNode neighbor in gridNode.Neighbours)
			{
				if(neighbor._useState != GridNodeUseState.walkable)
				{
					Debug.Log(neighbor);
					
					_openList.Add(neighbor);					
					neighbor.CostSoFar = 0;
					
				}
			}
		}

		
		

		//_openList.Add(_startNode);
		//_startNode.CostSoFar = 0;
	}

	protected override bool StepToGoal()
	{
		Debug.Log("AttackRangeStarted");
		GridNode current = _openList[0];
		Debug.Log(current);

		// end reached


		foreach (GridNode next in current.Neighbours)
		{
			float newCost = current.CostSoFar + 1;

			if (!_visited.ContainsKey(next))
			{
				// only use nextnode if cost less or equal to movmentpoints
				if (newCost <= _unit.AttackRange)
				{					
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
		current.SetGridNodeUseState(GridNodeUseState.attackable);
		current.SetGridNodeSearchState(GridNodeSearchState.Processed);
		if (_openList.Count == 0)
		{
			Debug.Log("AttackrangeIsDone");
			_unit._attackable = this._visited;
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
