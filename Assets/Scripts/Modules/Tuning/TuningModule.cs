/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

public class TuningModule : Module
{
	public int MaxMagicRange {
		get {
			return data.RangeAttackMaxRange;
		}
	}

	public int MaxMeleeRange {
		get {
			return data.MeleeAttackMaxRange;
		}
	}

	public float BulkToHPRatio { 
		get {
			return data.ConstitutionToHPRatio;
		}
	}

	public int MaxSpeed {
		get {
			return data.MaxSpeed;
		}
	}

	public float DamagePerMagicPoint {
		get {
			return data.DamagePerMagicPoint;
		}
	}

	public float DamagePerStrengthPoint {
		get {
			return data.DamagePerStrengthPoint;
		}
	}

	public int MaxSkill {
		get {
			return data.MaxSkill;
		}
	}

	public float CriticalHitRatePerSkillPoint {
		get {
			return data.CriticalHitRatePerSkillPoint;
		}
	}

	public int StartingHealthPotions {
		get {
			return data.StartingHealthPotions;
		}
	}

	public int VisionRange {
		get {
			return data.VisionRange;
		}
	}

	public float CriticalHitDamageMod {
		get {
			return data.CriticalHitDamageMod;
		}
	}

	public float SpeedToMovementRatio {
		get {
			return data.SpeedToMovementPoints;
		}
	}

	public float HealthPerecentGainFromPotion {
		get {
			return data.HealthPercentGainFromPotion;
		}
	}

	TuningData data;

	public void Init (TuningData data) {
		this.data = data;
	}

}

[System.Serializable]
public class TuningData : SerializableData
{
	public float ConstitutionToHPRatio;
	public int MaxSpeed;
	public float DamagePerMagicPoint;
	public float DamagePerStrengthPoint;
	public int MaxSkill;
	public float CriticalHitRatePerSkillPoint;
	public int StartingHealthPotions;
	public int VisionRange;
	public float CriticalHitDamageMod;
	public float SpeedToMovementPoints;
	public float HealthPercentGainFromPotion;
	public int MeleeAttackMaxRange;
	public int RangeAttackMaxRange;
}
