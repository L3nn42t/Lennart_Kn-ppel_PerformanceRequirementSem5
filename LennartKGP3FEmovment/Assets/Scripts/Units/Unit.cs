using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GP3._04_SearchAlgorithms.Dijkstra;

public class Unit : MonoBehaviour
{
    private bool _isselected;
    [SerializeField] private GameObject selctionIndicator;
    public UnitType _type;

    private GridNode _closestGridNode;
    public LayerMask gridMask;
    public GridNode ClosestGridNode => _closestGridNode;
    private Vector3 _screenPoint;
    private Vector3 _offset;
    //MovmentStuff
    public Manager manager;
    public GridNode _targetNode;
    public GridNode _moveNode; // to be used for attacking using the dictionary to retrace the path fom a attackable node to a walkable one
    public GameObject highlight;
    //points for Movment

    public int MovmentPoints;
    public int AttackRange;

    //Wip Stats
    public int Health;
    public int Damage;
    public bool pathfound = false;

    //ActionsWip
    [SerializeField]
    private bool movedthisRound;
    private bool attackedthisRound;
    public bool canmove = false;

    public Dictionary<GridNode, GridNode> _walkable = new Dictionary<GridNode, GridNode>();// Tiles are stored sepreatly in case i implement attacking
    public Dictionary<GridNode, GridNode> _attackable = new Dictionary<GridNode, GridNode>();
    public void Start()
    {
        SetToClosestGridNode();
    }

    private void OnMouseDown()
    {
        SetToClosestGridNode();
        
        if (!_isselected)
        {
            manager.ResetNodes();
            _isselected = true;
            
            manager.selectedUnit = this;
            manager._isunitselected = true;
            selctionIndicator.SetActive(true);

            MovmentRangeSearch rangeSearch = FindObjectOfType<MovmentRangeSearch>();
            rangeSearch.Search();
            AttackRangeSearch attackSearch = FindObjectOfType<AttackRangeSearch>();
            attackSearch.Search();
        }
        else
        {
            
            //rangeSearch.SlowSearch();
            //insert movment here // rewrite so a different gridnode is selected, get search algorithm
            _screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            _offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z));

            //foreach(GridNode node in _walkable.Keys)
            //{
            //    node.Reset();
            //}
            //foreach (GridNode node in _attackable.Keys)
            //{
            //    node.Reset();
            //}
            manager.ResetNodes();
            manager.selectedUnit = null;
            manager._isunitselected = false;
            _isselected = false;
            highlight.SetActive(false);
            selctionIndicator.SetActive(false);
        }
    }
    public void SetToClosestGridNode()
    {
        Collider2D overlapCircle = Physics2D.OverlapCircleAll(transform.position, transform.localScale.x, gridMask).OrderBy(c => (transform.position - c.transform.position).sqrMagnitude).FirstOrDefault();
        transform.position = overlapCircle.transform.position;
        Debug.Log(overlapCircle.transform.position+ "at object" + overlapCircle.gameObject.name);
        _closestGridNode = overlapCircle.transform.GetComponent<GridNode>();
    }
   
    public void UnitMovePath()
    {
        _moveNode = _targetNode;
        DijkstraSearch dijkstraSearch = FindObjectOfType<DijkstraSearch>();
        dijkstraSearch.SearchPath();
        pathfound = true;
    }
    public void MovetoTile()
    {
        transform.position = _moveNode.transform.position;
        SetToClosestGridNode();
        movedthisRound = true;
        manager.ResetNodes();
        pathfound = false;
    }
    public void UnitAttack()
    {
        
    }
    public void Update()
    {
        if(_targetNode == null)
        {
            highlight.SetActive(false);
        }
        else
        {
            highlight.SetActive(true);
            highlight.transform.position = _targetNode.transform.position;
            if (manager.selectedUnit = this) //i hate using so many if statements, but they don't accept multible parameters
            {
                if (!pathfound && _targetNode._useState == GridNodeUseState.walkable)
                {
                    UnitMovePath();
                }
                else if(!pathfound && _targetNode._useState == GridNodeUseState.attackable)
                {
                    //UnitAttack()
                }

            }
        }
        if(!movedthisRound && pathfound)
        {
            if(canmove == true)
            {
                MovetoTile();
                canmove = false;
            }
        }
        
    }
}
