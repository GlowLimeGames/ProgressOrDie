/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

[System.Serializable]
public class TileData : SerializableData
{
	public TileType[] Tiles;
}

[System.Serializable]
public class TileType : SerializableData
{
	public string TileName;
	public string Key;
	public int Speed;
	public int Skill;
	public int Constitution;
	public int Strength;
	public int Magic;
	public string PlaceholderLabel;
	public string Passive;
}
