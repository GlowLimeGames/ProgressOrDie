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

	public float BaseCriticalHitRate {
		get {
			return data.BaseCriticalHitRate;
		}
	}

	public float BaseCriticalHitDamageMultiplier {
		get {
			return data.BaseCriticalHitDamageMultiplier;
		}
	}

	public int StartingStatPoints {
		get {
			return data.StartingStatPoints;
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

	public float TimeToMove {
		get {
			return data.TimeToMove;
		}
	}

	public int TurnsBeforeBossMonsterHeals {
		get {
			return data.TurnsBeforeBossMonsterHeals;
		}
	}

	public int BossMonsterHealingAmount {
		get {
			return data.BossMonsterHealingAmount;
		}
	}

	public float DamageAddedToCriticalHitMultiplierPerSkillPoint {
		get {
			return data.DamageAddedToCriticalHitMultiplierPerSkillPoint;
		}
	}

	public string PlayerKey {
		get {
			return data.PlayerSymbolOnMap;
		}
	}

	public int MaxSpeedInCharacterCreation {
		get {
			return data.MaxSpeedInCharacterCreation;
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
	public int MaxSpeedInCharacterCreation;
	public float DamagePerMagicPoint;
	public float DamagePerStrengthPoint;
	public int MaxSkill;
	public float CriticalHitRatePerSkillPoint;
	public float BaseCriticalHitRate;
	public float BaseCriticalHitDamageMultiplier;
	public float DamageAddedToCriticalHitMultiplierPerSkillPoint;
	public int StartingStatPoints;
	public int StartingHealthPotions;
	public int VisionRange;
	public float SpeedToMovementPoints;
	public float HealthPercentGainFromPotion;
	public int MeleeAttackMaxRange;
	public int RangeAttackMaxRange;
	public float TimeToMove;
	public int TurnsBeforeBossMonsterHeals;
	public int BossMonsterHealingAmount;
	public string PlayerSymbolOnMap;
	public int NumberOfPotions;
}
