/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

using System;

[Serializable]
public class EnemyData : SerializableData
{
	public EnemyDescriptor[] Enemies;
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
	public int StatsOnKill;
	public int SphereOfInfluence;
	public int TerritoryRadius;
	public float ChanceOfMelee;

	public EnemyDescriptor GetInstance()
	{
		return Copy() as EnemyDescriptor;
	}
}

[Serializable]
public class StatPrefix
{
	public string Prefix;
	public string Stat;
}
