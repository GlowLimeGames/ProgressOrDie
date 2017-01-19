/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

using UnityEngine;

public class StatModule : Module
{
	TuningModule tuning;

	public void Init (TuningModule tuning) {
		this.tuning = tuning;
	}

	public float BulkToHPRatio {
		get {
			return tuning.BulkToHPRatio;
		}
	}

	public int MaxRange {
		get {
			return tuning.MaxMagicRange;
		}
	}

	public int MaxMeleeRange {
		get {
			return tuning.MaxMeleeRange;
		}
	}

	public int MaxSpeed {
		get {
			return tuning.MaxSpeed;
		}
	}

	public float DamagePerMagicPoint {
		get {
			return tuning.DamagePerMagicPoint;
		}
	}

	public float DamagePerStrengthPoint {
		get {
			return tuning.DamagePerStrengthPoint;
		}
	}

	public int MaxSkill {
		get {
			return tuning.MaxSkill;
		}
	}

	public float CriticalHitRatePerSkillPoint {
		get {
			return tuning.CriticalHitRatePerSkillPoint;
		}
	}

	public int StartingStatPoints {
		get {
			return tuning.StartingStatPoints;
		}
	}

	public int StartingHealthPotions {
		get {
			return tuning.StartingHealthPotions;
		}
	}

	public int VisionRange {
		get {
			return tuning.VisionRange;
		}
	}

	public float SpeedToMovementRatio {
		get {
			return tuning.SpeedToMovementRatio;
		}
	}

	public float HealthPerecentGainFromPotion {
		get {
			return tuning.HealthPerecentGainFromPotion;
		}
	}

	public string GetPlayerCritChanceAsPercentStr(PlayerCharacter player) {
		return string.Format("{0}%", getChanceOfCrit(player) * 100);
	}

	public float GetPlayerCritChanceAsPercentf(PlayerCharacter player) {
		return getChanceOfCrit(player);
	}

	public int GetMeleeDamage (IUnit unit) {
		int damage = (int) (unit.GetStrength() * DamagePerStrengthPoint);
		return applyDamageMods(unit, damage);
	}

	public int GetMagicDamage (IUnit unit) {
		int damage = (int) (unit.GetMagic() * DamagePerMagicPoint);
		return applyDamageMods(unit, damage);
	}

	int applyDamageMods(IUnit unit, int baseDamage)
	{
		int totalDamage;
		if(CriticalHit(unit)) {
			totalDamage = (int) ((float) baseDamage * getCritDamageModifier(unit));
			EventModule.Event(PODEvent.Notification, getCritNotification(unit));
		} else {
			totalDamage = baseDamage;
		}
		return totalDamage;
	}

	float getChanceOfCrit(IUnit unit) {
		return CriticalHitRatePerSkillPoint * unit.GetSkill() + tuning.BaseCriticalHitRate;
	}

	float getCritDamageModifier(IUnit unit) {
		return (float) unit.GetSkill() * tuning.DamageAddedToCriticalHitMultiplierPerSkillPoint + tuning.BaseCriticalHitDamageMultiplier;
	}

	string getCritNotification(IUnit unit) 
	{
		return string.Format("{0} dealt a critical hit for {1}% damage", unit, getCritDamageModifier(unit) * 100);
	}


	public bool CriticalHit (IUnit unit) {
		float critChance = getChanceOfCrit(unit);
		return Random.Range(0.0f, 1.0f) < critChance;
	}

}
