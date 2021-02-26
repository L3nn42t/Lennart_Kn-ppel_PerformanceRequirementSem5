using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UEGP3PR
{
	public class AttackRangeSearch : SearchBase
	{
		protected override void InitializeSearch()
		{

			_openList = new List<GridNode>();
			_visited = new Dictionary<GridNode, GridNode>();
			List<GridNode> gridNodes = new List<GridNode>();
			gridNodes = FindObjectOfType<Grid>()._nodes; //searches all nodes if they are walkable
			foreach (GridNode node in gridNodes)
			{
				if (node._useState == GridNodeUseState.walkable)
				{
					foreach (GridNode neighbor in node._neighbours)
					{
						if (neighbor._useState != GridNodeUseState.walkable) //adds neighbors that are not walkable 
						{
							_openList.Add(neighbor);
							neighbor.CostSoFar = 1; //is 1 step away from walkable area
						}
					}
				}
			}

			////_visited = _unit._walkable;  //This solution used the dictionary walkable to get the nodes, but resulted in a row of nodes that arent in any dictionary, however this means we have to use search the whole grid for getting a movable point
			////Debug.Log("active");

			////foreach (GridNode gridNode in _visited.Keys)
			////{
			////	foreach(GridNode neighbor in gridNode.Neighbours)
			////	{
			////		if(neighbor._useState != GridNodeUseState.walkable)
			////		{
			////			Debug.Log(neighbor);

			////			_openList.Add(neighbor);					
			////			neighbor.CostSoFar = 0;

			////		}
			////	}
			////}




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

				if (!_visited.ContainsKey(next) && next._useState != GridNodeUseState.walkable)
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
}

