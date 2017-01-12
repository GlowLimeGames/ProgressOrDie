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

	public float CriticalHitDamageMod {
		get {
			return tuning.CriticalHitDamageMod;
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

	public int GetMeleeDamage (IUnit unit) {
		return (int) (unit.GetStrength() * DamagePerStrengthPoint);
	}

	public int GetMagicDamage (IUnit unit) {
		return (int) (unit.GetMagic() * DamagePerMagicPoint);
	}

	public bool CriticalHit (IUnit unit) {
		float critChance = CriticalHitRatePerSkillPoint * unit.GetSkill();
		return Random.Range(0.0f, 1.0f) < critChance;
	}

}
