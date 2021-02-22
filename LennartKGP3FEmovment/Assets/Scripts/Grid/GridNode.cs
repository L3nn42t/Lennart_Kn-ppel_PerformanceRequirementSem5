using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GridNode : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _image = default;
    [SerializeField] private SpriteRenderer _searchUseImage = default;
    [SerializeField] private SpriteRenderer _searchStateImage = default;

    public float CostSoFar { get; set; }
    public IEnumerable<GridNode> Neighbours => _neighbours;
    public bool IsWall => _type == GridNodeType.Wall;
    public GridNodeType _type;
    public GridNodeUseState _useState;
    public GridNodeSearchState _searchState;
    public List<GridNode> _neighbours = new List<GridNode>();
    public LayerMask gridmask;
    public Manager manager;

    //Placeholder, to be set by unit object
    public int groundcost;
    public int wallcost;
    public int forestcost;
    public int roughcost;

    public float cost;

    public float Cost
    {
        get
        {
            switch (_type)
            {
                case GridNodeType.Ground:
                    return groundcost;
                case GridNodeType.Wall:
                    return wallcost;
                case GridNodeType.Forest:
                    return forestcost;
                case GridNodeType.Rought:
                    return roughcost;
                default:
                    throw new ArgumentOutOfRangeException();

            }
        }
    }

    public void Init()
    {
        Reset();
        FindNeighbours();
        SetGridNodeType(GridNodeType.Ground);
        SetGridNodeUseState(GridNodeUseState.none);
        cost = Cost;
        
        _searchUseImage.enabled = false;
        _searchStateImage.enabled = false;
    }
    private void OnMouseDown()
    {
        if (!manager._isunitselected)
        {
            switch (_type)
            {
                case GridNodeType.Ground:
                    SetGridNodeType(GridNodeType.Wall);
                    break;
                case GridNodeType.Wall:
                    SetGridNodeType(GridNodeType.Forest);
                    break;
                case GridNodeType.Forest:
                    SetGridNodeType(GridNodeType.Rought);
                    break;
                case GridNodeType.Rought:
                    SetGridNodeType(GridNodeType.Ground);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        else
        {
            Unit unit = manager.selectedUnit;
            
            unit._targetNode = this;
            Debug.Log("targetnode is selected");
        }
       
    }

    private void SetGridNodeType(GridNodeType type)
    {
        _type = type;
        switch (type)
        {
            case GridNodeType.Ground:
                _image.color = SearchSettings.Instance.GroundNodeColor;
                break;
            case GridNodeType.Wall:
                _image.color = SearchSettings.Instance.WallNodeColor;
                break;
            case GridNodeType.Forest:
                _image.color = SearchSettings.Instance.ForestNodeColor;
                break;
            case GridNodeType.Rought:
                _image.color = SearchSettings.Instance.RoughtNodeColor;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
    public void Reset()
    {
      
        SetGridNodeUseState(GridNodeUseState.none);
        CostSoFar = -1000;
    }
   

    public void SetGridNodeUseState(GridNodeUseState state)
    {
        _useState = state;
        _searchUseImage.enabled = true;

        switch (state)
        {
            case GridNodeUseState.none:
                _searchUseImage.color = SearchSettings.Instance.NoneNodeColor;
                break;
            case GridNodeUseState.walkable:
                _searchUseImage.color = SearchSettings.Instance.WalkableNodeColor;
                break;
            case GridNodeUseState.attackable:
                _searchUseImage.color = SearchSettings.Instance.AttackableNodeColor;
                break;
            case GridNodeUseState.danger:
                _searchUseImage.color = SearchSettings.Instance.DangerNodeColor;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }
    public void SetGridNodeSearchState(GridNodeSearchState state)
    {
        _searchState = state;
        _searchStateImage.enabled = true;
        switch (state)
        {
            
            case GridNodeSearchState.Queue:
                _searchStateImage.color = SearchSettings.Instance.QueueNodeColor;
                break;
            case GridNodeSearchState.Processed:
                _searchStateImage.color = SearchSettings.Instance.ProcessedNodeColor;
                break;
            case GridNodeSearchState.PartOfPath:
                _searchStateImage.color = SearchSettings.Instance.PathNodeColor;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }
    private void FindNeighbours()
    {
        RaycastHit2D raycastHit2D = Physics2D.Linecast(transform.position, transform.position + Vector3.up, gridmask);
        if ((raycastHit2D.collider != null) && raycastHit2D.collider.TryGetComponent(out GridNode node))
        {
            _neighbours.Add(node);
        }
        raycastHit2D = Physics2D.Linecast(transform.position, transform.position + Vector3.right, gridmask);
        if ((raycastHit2D.collider != null) && raycastHit2D.collider.TryGetComponent(out node))
        {
            _neighbours.Add(node);
        }
        raycastHit2D = Physics2D.Linecast(transform.position, transform.position + Vector3.down, gridmask);
        if ((raycastHit2D.collider != null) && raycastHit2D.collider.TryGetComponent(out node))
        {
            _neighbours.Add(node);
        }
        raycastHit2D = Physics2D.Linecast(transform.position, transform.position + Vector3.left,gridmask);
        if ((raycastHit2D.collider != null) && raycastHit2D.collider.TryGetComponent(out node))
        {
            _neighbours.Add(node);
        }
    }
}
