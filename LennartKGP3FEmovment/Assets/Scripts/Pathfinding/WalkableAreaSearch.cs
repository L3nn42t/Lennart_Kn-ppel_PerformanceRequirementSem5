using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UEGP3PR
{
	public class WalkableAreaSearch : SearchBase
	{

		protected override void InitializeSearch()
		{
			_startNode = _unit._targetNode;


			foreach (GridNode gridNode in _visited.Keys)
			{
				gridNode.Reset();
			}

			_openList = new List<GridNode>();
			_visited = new Dictionary<GridNode, GridNode>();

			foreach (GridNode node in _startNode._neighbours)
			{
				_openList.Add(node);
			}
			//_openList.Add(_startNode);
		}

		protected override bool StepToGoal()
		{
			GridNode current = _openList[0];

			// goal found
			if (current._useState == GridNodeUseState.walkable)
			{
				_unit._moveNode = current; //this is the only difference towards some of the other algorithms, but I only want to find the nearest walkable
				return true;
			}

			foreach (GridNode next in current.Neighbours)
			{
				if (next == _startNode)
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

}
