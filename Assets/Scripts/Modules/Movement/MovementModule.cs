/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

using System;
using System.Collections.Generic;

public class MovementModule : Module
{
	AgentAction onAgentMove;
	TurnModule turn;
	TuningModule tuning;
	MapModule map;

	bool isSetup = false;
	public float TimeToMove {
		get {
			return tuning.TimeToMove;
		}
	}

	public bool IsSetup {
		get {
			return isSetup;
		}
	}

	public void Init (TurnModule turn, TuningModule tuning, MapModule map) {
		this.turn = turn;
		this.tuning = tuning;
		this.map = map;
		this.isSetup = true;
	}
			
	void callOnAgentMove (Agent agent) {
		if (onAgentMove != null) {
			onAgentMove(agent);
		}
	}
		
	public void SubscribeToAgentMove (AgentAction action) {
		onAgentMove += action;
	}

	public void UnsubscribeFromAgentMove (AgentAction action) {
		onAgentMove -= action;
	}

	public bool CanMove (Agent agent) {
		return agent.GetAgentType() == turn.GetCurrentTurn();
	}

	public void Move (Agent agent) {
		callOnAgentMove(agent);
	}

	public void DetermineEnemyMovement(EnemyNPC enemy) {
		HashSet<MapTile> checkedTiles = new HashSet<MapTile>();
		bool foundSuitableTile = false;
		int tileTimeout = (int) Math.Pow(enemy.TerritoryRadius, 2) - 1;
		MapTile potentialDestination = null;
		while (!foundSuitableTile && checkedTiles.Count < tileTimeout) {
			potentialDestination = map.RandomTileInRadius(enemy.StartingLocation, enemy.TerritoryRadius);
			checkedTiles.Add(potentialDestination);
			foundSuitableTile = CanMoveToTile(enemy, potentialDestination);
		}
		if(foundSuitableTile) {
			enemy.LeaveCurrentTile();
			potentialDestination.PlaceUnit(enemy.GetAgent());
		}
	}

	public bool CanMoveToTile(Unit unit, MapTile tile)
	{
		if(tile.IsOccupiedByUnit())
		{
			return false;
		}
		else 
		{
			if(unit is PlayerCharacter) {
				return tile.PlayerPassable;
			} else if (unit is EnemyNPC) {
				return tile.EnemyPassable;
			} else {
				return true;
			}
		}
	}

}
