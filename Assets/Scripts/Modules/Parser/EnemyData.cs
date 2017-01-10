/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

[System.Serializable]
public class EnemyData : SerializableData
{
	public EnemyDescriptor[] Enemies;
	public StatPrefix[] Prefxies;
}

[System.Serializable]
public class EnemyDescriptor : SerializableData
{
	public string Key;
	public string[] Types;
	public int Speed;
	public int Strength;
	public int Skill;
	public int Constitution;
	public int Magic;
	public int[] Size;
	public int TurnPriority;

	public EnemyDescriptor Copy() {
		EnemyDescriptor desc = new EnemyDescriptor();
		desc.Key = this.Key;
		desc.Types = this.Types;
		desc.Speed = this.Speed;
		desc.Strength = this.Strength;
		desc.Skill = this.Skill;
		desc.Constitution = this.Constitution;
		desc.Magic = this.Magic;
		desc.Size = this.Size;
		desc.TurnPriority = this.TurnPriority;
		return desc;
	}
}

[System.Serializable]
public class StatPrefix
{
	public string Prefix;
	public string Stat;
}
