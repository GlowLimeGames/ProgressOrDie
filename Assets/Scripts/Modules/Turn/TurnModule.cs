/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

public class TurnModule : Module, ITurnModule
{	
	const string TURN = "Turn";
	int turnCount = 1;

	AgentType currentTurn;
	AgentTypeAction onTurnChange;
	MonoActionStr onTurnChangeStr;

	public AgentType GetCurrentTurn () {
		return currentTurn;
	}

	public string CurrentTurnStr () {
		return string.Format("{0} {1}:\n{2}", TURN, turnCount, currentTurn.ToString());
	}

	public void SubscribeToTurnSwitch (AgentTypeAction action) {
		this.onTurnChange += action;
	}

	public void UnsubscribeFromTurnSwitch (AgentTypeAction action) {
		this.onTurnChange -= action;
	}

	public void SubscribeToTurnSwitchStr (MonoActionStr action) {
		this.onTurnChangeStr += action;
	}
		
	public void UnsubscribeFromTurnSwitchStr (MonoActionStr action) {
		this.onTurnChangeStr -= action;
	}

	void callOnTurnChange (AgentType turn) {
		if (onTurnChange != null) {
			onTurnChange(turn);
		}
	}

	void callOnTurnChangeStr (string turnName) {
		if (onTurnChangeStr != null) {
			onTurnChangeStr(turnName);
		}
	}

	public void NextTurn () {
		turnCount++;
		int currentTurn = (int) this.currentTurn;
		currentTurn += 1;
		currentTurn %= Agent.AgentTypeCount();
		this.currentTurn = (AgentType) currentTurn;
		callOnTurnChange(this.currentTurn);
		callOnTurnChangeStr(CurrentTurnStr());
	}

}
