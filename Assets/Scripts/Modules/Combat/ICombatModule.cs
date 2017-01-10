/*
 * Authors: Ian Macpherson, Isaiah Mann
 * Description: An interface to handle the combat logic
 */

public interface ICombatModule {

	bool IsTargetInRange (IUnit attacker, IUnit target, AttackType attackType);

	bool IsTargetAdjacent (IUnit attacker, IUnit target);

	void MeleeAttack (IUnit attacker, IUnit target);

	void MagicAttack (IUnit attacker, IUnit target);

	void FleeAttempt (IStatModule playerstats, IUnit unit);

	void KillUnit (IUnit unit);

}
