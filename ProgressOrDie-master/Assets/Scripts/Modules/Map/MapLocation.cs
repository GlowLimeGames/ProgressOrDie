/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

[System.Serializable]
public class MapLocation : WorldData
{
	public int X;
	public int Y;

	public MapLocation(int x, int y)
	{
		this.X = x;
		this.Y = y;
	}

	public MapLocation Translate(int x, int y) {
		return new MapLocation(this.X + x, this.Y + y);
	}

}
