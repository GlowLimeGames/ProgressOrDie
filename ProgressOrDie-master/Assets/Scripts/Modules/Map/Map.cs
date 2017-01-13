/*
 * Author(s): Isaiah Mann
 * Description: Map of the game world
 * Usage: [no notes]
 */

public class Map 
{
	MapModule module;

	public Map(MapTile[,] tiles, MapModule mod) {
		this.tiles = tiles;
		this.module = mod;
	}

	public int Width {
		get {
			return tiles.GetLength(0);
		}
	}

	public int Height {
		get {
			return tiles.GetLength(1);
		}
	}

	MapTile[,] tiles;

	public bool CoordinateIsInBounds (MapLocation loc) {
		return CoordinateIsInBounds(loc.X, loc.Y);
	}

	public bool CoordinateIsInBounds (int x, int y) {
		return IntUtil.InRange(x, Width) && IntUtil.InRange(y, Height);
	}

	public MapTile GetTile (int x, int y) {
		if (CoordinateIsInBounds(x, y)) {
			return tiles[x, y];
		} else {
			return MapTile.Default;
		}
	}

	public MapTile GetTile (MapLocation loc) {
		return GetTile(loc.X, loc.Y);
	}

	public int TravelTo (Agent agent, MapLocation loc) {
		return module.TravelTo(agent, loc);
	}
}
