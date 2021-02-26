using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UEGP3PR
{
	public class Grid : MonoBehaviour
	{
		[SerializeField] private GridNode _prefab;
		//[SerializeField] private Marker _startMarkerPrefab;
		//[SerializeField] private Marker _endMarkerPrefab;
		[SerializeField] private float _width;
		[SerializeField] private float _height;
		[SerializeField] private Manager _manager;
		public List<GridNode> _nodes = new List<GridNode>();

		public Unit unit;
		private void Awake()
		{
			unit = _manager.selectedUnit;
			CreateGrid();
			//RandomlyScatterStartAndEnd();
			InitNodes();
		}

		//private void RandomlyScatterStartAndEnd()
		//{
		//	_searchAlgorithm.StartMarker = PlaceMarker(_startMarkerPrefab);
		//	_searchAlgorithm.EndMarker = PlaceMarker(_endMarkerPrefab);
		//}

		//private Marker PlaceMarker(Marker prefab)
		//{
		//	float ranX = Random.Range(0, _width);
		//	float ranY = Random.Range(0, _height);

		//	Marker placeMarker = Instantiate(prefab, transform.position + new Vector3(ranX, ranY, 0), Quaternion.identity, null);
		//	placeMarker.SetToClosestGridNode();

		//	return placeMarker;
		//}

		private void InitNodes()
		{
			foreach (GridNode gridNode in _nodes)
			{
				gridNode.Init();
			}
		}

		private void CreateGrid()
		{
			for (int x = 0; x < _width; x++)
			{
				for (int y = 0; y < _height; y++)
				{
					GridNode gridNode = Instantiate(_prefab, transform.position + new Vector3(x, y, 0), Quaternion.identity, transform);
					_manager.GiveNodeCost();
					gridNode.manager = _manager;
					_nodes.Add(gridNode);
				}
			}
		}
	}

}
