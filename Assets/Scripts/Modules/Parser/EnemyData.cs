/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

using System;

[Serializable]
public class EnemyData : SerializableData
{
	public EnemyDescriptor[] EnemiesLV1;
	public EnemyDescriptor[] EnemiesLV2;
	public EnemyDescriptor[] EnemiesLV3;
	public EnemyDescriptor[] Enemies {
		get {
			return ArrayUtil.Concat(ArrayUtil.Concat(EnemiesLV1, EnemiesLV2), EnemiesLV3);
		}
	}
	public BossDescriptor[] BossMonsters;

	public StatPrefix[] Prefxies;
}

[Serializable]
public class EnemyDescriptor : SerializableData
{
	public string Key;
	public string[] Types;
	public int Speed;
	public int Strength;
	public int Skill;
	public int Constitution;
	public int Magic;
	public int TurnPriority;
	public int StatPointsOnKill;
	public int SphereOfInfluence;
	public int TerritoryRadius;
	public float ChanceOfMelee;
	public int Level;

	public EnemyDescriptor GetInstance()
	{
		return Copy() as EnemyDescriptor;
	}
}

[Serializable]
public class BossDescriptor : EnemyDescriptor
{

	public BossDescriptor GetBoss()
	{
		return GetInstance() as BossDescriptor;
	}
}


[Serializable]
public class StatPrefix
{
	public string Prefix;
	public string Stat;
}
