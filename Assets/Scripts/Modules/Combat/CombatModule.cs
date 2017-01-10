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
		PlayerCharacter player = units.GetMainPlayer().GetCharacter();
		if(ableToPerformMeleeAttack(player, unit)) {
			player.MeleeAttack(unit);
		} else {
			player.MagicAttack(unit);
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
		target.Damage(stats.GetMeleeDamage(attacker));
	}

	public void MagicAttack (IUnit attacker, IUnit target) {
		target.Damage(stats.GetMagicDamage(attacker));
	}

	public void FleeAttempt (IStatModule playerstats, IUnit unit) {

	}

	public void KillUnit (IUnit unit) {

	}

}
