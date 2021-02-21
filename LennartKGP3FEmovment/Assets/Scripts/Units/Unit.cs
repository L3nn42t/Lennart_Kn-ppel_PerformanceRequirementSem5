using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
    //public GridNode _targetNode;
    public GameObject highlight;
    //points for Movment

    public int MovmentPoints;
    public int AttackRange;

    //Wip Stats
    public int Health;
    public int Damage;


    public Dictionary<GridNode, GridNode> _walkable = new Dictionary<GridNode, GridNode>();// Tiles are stored sepreatly in case
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
            _isselected = true;
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

            foreach(GridNode node in _walkable.Keys)
            {
                node.Reset();
            }
            foreach (GridNode node in _attackable.Keys)
            {
                node.Reset();
            }
            _isselected = false;
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
   
    public void UnitMove()
    {

    }
    public void UnitAttack()
    {

    }
}
