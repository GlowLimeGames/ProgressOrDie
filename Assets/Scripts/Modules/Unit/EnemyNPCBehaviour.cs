/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

public class EnemyNPCBehaviour : AIAgent 
{	
	EnemyNPC enemy;

	public override AgentType GetAgentType()
	{
		return AgentType.Enemy;
	}

	public override Unit GetUnit() {
		return GetEnemy();
	}

	public EnemyNPC GetEnemy () {
		return enemy;
	}

	public void SetEnemy (EnemyNPC enemy) {
		this.enemy = enemy;
		this.enemy.LinkToAgent(this);
		ReplenishAtTurnStart(AgentType.Enemy);
	}
}
