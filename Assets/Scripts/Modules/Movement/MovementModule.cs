/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

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
		MapTile newTile = map.RandomTileInRadius(enemy.StartingLocation, enemy.TerritoryRadius);
		newTile.PlaceUnit(enemy.GetAgent());
	}

}
