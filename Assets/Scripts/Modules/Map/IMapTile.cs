/*
 * Author: Isaiah Mann
 * Desc: A single cell on the map
 */

public interface IMapTile {

	bool IsOccupiedByUnit();

	int GetX();
	int GetY();
	MapLocation GetLocation();

	TileType GetTileType();

	void PlaceUnit(Agent unit);
	void RemoveUnit();

}
