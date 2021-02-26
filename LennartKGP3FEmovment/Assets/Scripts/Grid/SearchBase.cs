using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UEGP3PR
{
	public abstract class SearchBase : MonoBehaviour
	{
		public List<GridNode> _openList = new List<GridNode>();
		public Dictionary<GridNode, GridNode> _visited = new Dictionary<GridNode, GridNode>();
		public Unit _unit;
		public GridNode _startNode;
		public GridNode _goalNode;
		public Manager _manager;

		private void BuildPath()
		{
			GridNode current = _goalNode;
			List<GridNode> path = new List<GridNode>();

			while (!current.Equals(_startNode))
			{
				path.Add(current);
				current = _visited[current];
			}

			path.Add(_startNode);
			path.Reverse();
			foreach (GridNode gridNode in path)
			{
				gridNode.SetGridNodeSearchState(GridNodeSearchState.PartOfPath);
			}

		}
		public void SlowSearch()
		{
			StartCoroutine((IEnumerator)SearchInternalCoroutine());
		}
		private IEnumerator SearchInternalCoroutine()
		{
			_unit = _manager.selectedUnit; //doesn't make use of _goalNode, this and search movmentPoint could be used the same, diversion is a relic of development
			InitializeSearch();
			yield return new WaitForSeconds(1);

			while (_openList.Count > 0)
			{
				if (StepToGoal())
				{
					break;
				}

				yield return new WaitForSeconds(1);
			}
		}

		public void Search()
		{
			_unit = _manager.selectedUnit;
			InitializeSearch();

			while (_openList.Count > 0)
			{
				if (StepToGoal())
				{
					break;
				}
			}

			//BuildPath();
		}
		public void SearchPath()
		{
			_unit = _manager.selectedUnit;
			InitializeSearch();

			while (_openList.Count > 0)
			{
				if (StepToGoal())
				{
					break;
				}
			}

			BuildPath();
		}

		public void SearchMovmentPoint()
		{
			_unit = _manager.selectedUnit;
			InitializeSearch();

			while (_openList.Count > 0)
			{
				if (StepToGoal())
				{
					break;
				}
			}
		}

		protected abstract void InitializeSearch();
		protected abstract bool StepToGoal();
	}

}
