using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : Unit
{
    public PlayerUnit player;
    public void Start()
    {
        SetToClosestGridNode();
        player = FindObjectOfType<PlayerUnit>(); //in an actual game one could use an array
    }

    private void OnMouseDown()
    {
        SetToClosestGridNode();

        
        if (manager.selectedUnit == player && manager._isunitselected)
        {
            player.targetUnit = this;
            // attack here
            if(!player.movedthisRound && !player.attackedthisRound)
            {
                if(ClosestGridNode._useState == GridNodeUseState.attackable)
                {
                    player.UnitAttack();
                }
                else if(ClosestGridNode._useState == GridNodeUseState.walkable)
                {
                    player.UnitAttack();
                }
                
            }
            
        }
        else if (!_isselected)
        {
            manager.ResetNodes();
            _isselected = true;

            manager.selectedUnit = this;
            manager._isunitselected = true;
            selctionIndicator.SetActive(true);
            manager.GiveNodeCost();

            MovmentRangeSearch rangeSearch = FindObjectOfType<MovmentRangeSearch>();
            rangeSearch.Search();
            AttackRangeSearch attackSearch = FindObjectOfType<AttackRangeSearch>();
            attackSearch.Search();
            manager.SetNodetoDanger();
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

            selctionIndicator.SetActive(false);
            movedthisRound = false;// wip instedad of a real roundSystem
        }
    }

}
