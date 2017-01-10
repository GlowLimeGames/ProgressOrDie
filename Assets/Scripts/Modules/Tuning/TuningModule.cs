/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

public class TuningModule : Module
{
	public int MaxRange {
		get {
			return data.MaxRange;
		}
	}

	public float BulkToHPRatio { 
		get {
			return data.BulkToHPRatio;
		}
	}

	public int MaxSpeed {
		get {
			return data.MaxSpeed;
		}
	}

	public int DamagePerMagicPoint {
		get {
			return data.DamagePerMagicPoint;
		}
	}

	public int DamagePerStrengthPoint {
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

	TuningData data;

	public void Init (TuningData data) {
		this.data = data;
	}

}

[System.Serializable]
public class TuningData : SerializableData
{
	public int MaxRange;
	public float BulkToHPRatio;
	public int MaxSpeed;
	public int DamagePerMagicPoint;
	public int DamagePerStrengthPoint;
	public int MaxSkill;
	public float CriticalHitRatePerSkillPoint;
	public int StartingHealthPotions;
	public int VisionRange;
	public float CriticalHitDamageMod;

}
