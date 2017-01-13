/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

[System.Serializable]
public class MapTile : WorldData, IMapTile
{
	public static MapTile Default {
		get {
			return new MapTile(0, 0, new TileType());
		}
	}

	public bool HasGOLink {
		get {
			return this.goLink != null;
		}
	}

	public TileType TileType {
		get {
			return this.type;
		}
	}

	[System.NonSerialized]
	MapTileBehaviour goLink;

	MapLocation location;
	Unit occupyingUnit;
	TileType type;

	public void Link (MapTileBehaviour goLink) {
		this.goLink = goLink;
	}

	public MapTile(int x, int y, TileType type)
	{
		this.location = new MapLocation(x, y);
		this.type = type;
	}

	public bool IsOccupiedByUnit()
	{
		return occupyingUnit != null;
	}

	public int GetX()
	{
		return this.location.X;
	}

	public int GetY()
	{
		return this.location.Y;
	}

	public MapLocation GetLocation()
	{
		return this.location;
	}

	public TileType GetTileType()
	{
		return this.type;
	}

	public void PlaceUnit(Agent agent)
	{
		this.occupyingUnit = agent.GetUnit();
		if (HasGOLink) {
			goLink.PlaceUnit(agent);
		}
	}

	public void RemoveUnit()
	{
		this.occupyingUnit = null;
	}

}
