/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

using UnityEngine;

public class MapTileBehaviour : WorldObjectBehaviour 
{	
	public bool HasAgent {
		get {
			return containedAgent != null;
		}
	}

	Agent containedAgent;
	SpriteRenderer spriteR;
	Map map;
	public MapTile Tile{get; private set;}

	protected override void SetReferences () {
		base.SetReferences ();
		this.spriteR = GetComponent<SpriteRenderer>();
	}

	public void SetTile (Map map, MapTile tile, Sprite sprite) {
		this.map = map;
		this.Tile = tile;
		tile.Link(this);
		transform.position = getPosition();
		spriteR.sprite = sprite;
	}

	public void PlaceUnit(Agent agent) {
		this.containedAgent = agent;
		agent.SetLocation(this);
	}

	public Vector3 WorldPos () {
		return transform.position;
	}

	Vector3 getPosition() {
		return new Vector3((float) (Tile.GetX() - map.Width / 2) / 4f, (float) (Tile.GetY() - map.Height / 2) / 4f);
	}
}
