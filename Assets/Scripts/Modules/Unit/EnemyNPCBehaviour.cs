/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

public class EnemyNPCBehaviour : AIAgent 
{	
	HealthBarBehaviour healthBar;

	EnemyNPC enemy;

	protected override void SetReferences ()
	{
		base.SetReferences ();
		healthBar = GetComponentInChildren<HealthBarBehaviour>();
	}

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

	public void UpdateHealthDisplay(float fraction) {
		healthBar.SetHealthDisplay(fraction);
	}

	public void SetEnemy (EnemyNPC enemy) {
		this.enemy = enemy;
		this.SetUnit(enemy);
		ReplenishAtTurnStart(AgentType.Enemy);
	}
}
