/*
 * Authors: Riva Lam, Isaiah Mann
 * Description: An interface to handle the map logic
 */

public interface IMapModule {

	bool CoordinateIsInBounds (int x, int y);

	MapTile GetTile (int x, int y);

}
