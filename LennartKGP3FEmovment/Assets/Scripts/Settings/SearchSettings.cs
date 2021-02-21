using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FEMovment", menuName = "SearchAlgorithms/New SearchSettings", order = 200)]
public class SearchSettings : ScriptableSingleton<SearchSettings>
{
	// Start is called before the first frame update
	[Header("Colors for Use States")]
	[SerializeField] private Color _noneNodeColor;
	[SerializeField] private Color _walkableNodeColor;
	[SerializeField] private Color _attackableNodeColor;
	[SerializeField] private Color _dangerNodeColor;
	[Header("Colors for Node Types")]
	[SerializeField] private Color _groundNodeColor;
	[SerializeField] private Color _wallNodeColor;
	[SerializeField] private Color _roughtNodeColor;
	[SerializeField] private Color _forestNodeColor;
	[Header("Colors for Search States")]
	[SerializeField] private Color _queueNodeColor;
	[SerializeField] private Color _processedNodeColor;
	[SerializeField] private Color _pathNodeColor;

	public Color NoneNodeColor => _noneNodeColor;
	public Color WallNodeColor => _wallNodeColor;
	public Color ForestNodeColor => _forestNodeColor;
	public Color RoughtNodeColor => _roughtNodeColor;
	public Color GroundNodeColor => _groundNodeColor;
	public Color WalkableNodeColor => _walkableNodeColor;
	public Color AttackableNodeColor => _attackableNodeColor;
	public Color DangerNodeColor => _dangerNodeColor;

	public Color QueueNodeColor => _queueNodeColor;
	public Color ProcessedNodeColor => _processedNodeColor;
	public Color PathNodeColor => _pathNodeColor;
}
