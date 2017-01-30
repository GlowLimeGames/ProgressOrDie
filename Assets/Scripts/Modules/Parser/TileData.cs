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
	public static TileType Default 
	{
		get 
		{
			TileType tile = new TileType();
			tile.TileName = "Pit";
			tile.Key = "P";
			return tile;
		}
	}

	public string TileName;
	public string Key;
	public int Speed;
	public int Skill;
	public int Constitution;
	public int Strength;
	public int Magic;
	public bool PlayerPassable;
	public bool MonsterPassable;

}
