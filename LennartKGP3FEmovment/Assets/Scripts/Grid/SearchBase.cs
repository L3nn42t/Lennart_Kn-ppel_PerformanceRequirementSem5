using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SearchBase : MonoBehaviour
{
	public  List<GridNode> _openList = new List<GridNode>();
	public Dictionary<GridNode, GridNode> _visited = new Dictionary<GridNode, GridNode>();
	public Unit _unit;
	protected GridNode _startNode;
	protected GridNode _goalNode;

	public Unit Unit { set => _unit = value; }
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
		
	}
	public void SlowSearch()
	{
		StartCoroutine((IEnumerator)SearchInternalCoroutine());
	}
	private IEnumerator SearchInternalCoroutine()
	{
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

	protected abstract void InitializeSearch();
	protected abstract bool StepToGoal();
}
