/*
 * Author(s): Isaiah Mann
 * Description: Handles combat and targeting logic
 * Usage: [no notes]
 */

using UnityEngine;

public class CombatModule : Module, ICombatModule
{
	UnitModule units;
	MapModule map;
	AbilitiesModule abilities;
	StatModule stats;
	GameEndModule gameEnd;

	PlayerCharacter player
	{
		get 
		{
			return units.Player();
		}
	}

	public void Init (
		UnitModule units, 
		MapModule map, 
		AbilitiesModule abilities,
		StatModule stats,
		GameEndModule gameEnd
	)
	{
		this.units = units;
		this.map = map;
		this.abilities = abilities;
		this.stats = stats;
		this.gameEnd = gameEnd;
	}

	public void HandleAttackByPlayer (IUnit unit) {
		PlayerCharacterBehaviour playerAgent = units.GetMainPlayer();
		PlayerCharacter player = playerAgent.GetCharacter();
		if (!playerAgent.HasAttackedDuringTurn) {
			playerAgent.Attack();
			if(ableToPerformMeleeAttack(player, unit)) {
				player.MeleeAttack(unit);
				EventModule.Event(PODEvent.PlayerMeleeAttack);
			} else {
				player.MagicAttack(unit);
				EventModule.Event(PODEvent.PlayerMagicAttack);
			}
		}
	}

	public bool IsTargetInRange (IUnit attacker, IUnit target, AttackType attackType) {
		switch (attackType) {
			case AttackType.Melee:
				return ableToPerformMeleeAttack(attacker, target);
			case AttackType.Magic:
				return ableToPerformRangedAttack(attacker, target);
			default:
				return false;
		}
	}

	public bool HasTargetToAttack(Unit unit, out Unit validTarget)
	{
		validTarget = null;
		if(unit is EnemyNPC)
		{
			if(IsTargetInRange(unit, player, unit.GetPrimaryAttack()))
			{
				validTarget = player;
				return true;
			}
			else
			{
				return false;
			}
		}
		else
		{
			return false;
		}
	}

	bool ableToPerformMeleeAttack (IUnit attacker, IUnit target) {
		return isTargetAdjacent(attacker, target, countDiagonal:false);
	}

	bool ableToPerformRangedAttack (IUnit attacker, IUnit target) {
		return getRangeRequired(attacker, target) <= stats.MaxRange;
	}

	int getRangeRequired (IUnit attacker, IUnit target) {
		return Mathf.Abs(attacker.X - target.X) + Mathf.Abs(attacker.Y - target.Y);
	}

	public bool IsTargetAdjacent (IUnit attacker, IUnit target) {
		return isTargetAdjacent(attacker, target, countDiagonal:false);
	}

	bool isTargetAdjacent (IUnit attacker, IUnit target, bool countDiagonal) {
		if (countDiagonal) {
			return isTargetDiagonallyAdjacent(attacker, target) || 
				isTargetAdjacent(attacker, target, countDiagonal:false);
		} else {
			return Mathf.Abs(attacker.X - target.X) + Mathf.Abs(attacker.Y - target.Y) < 2;
		}
	}

	bool isTargetDiagonallyAdjacent (IUnit attacker, IUnit target) {
		return Mathf.Abs(attacker.X - target.X) == 1 && Mathf.Abs(attacker.Y - target.Y) == 1;
	}

	public void MeleeAttack (IUnit attacker, IUnit target) {
		if(attacker is PlayerCharacter) 
		{
			EventModule.Event("PlayerMeleeAttack");
		}
		else if (attacker is EnemyNPC)
		{
			EventModule.Event("EnemyMeleeAttack");
		}
		int damage = stats.GetMeleeDamage(attacker);
		target.Damage(damage);
		handleAttack(attacker, target, damage);
	}

	public void MagicAttack (IUnit attacker, IUnit target) {
		EventModule.Event("MagicAttack");
		int damage = stats.GetMagicDamage(attacker);
		target.Damage(damage);
		handleAttack(attacker, target, damage);
	}

	void handleAttack(IUnit attacker, IUnit target, int damage) {
		EventModule.Event(PODEvent.Notification, 
			string.Format("{0} dealt {1} damage to {2}", attacker, damage, target));
	}

	public void FleeAttempt (IStatModule playerstats, IUnit unit) {

	}

	public void KillUnit (IUnit unit) {

	}
}
