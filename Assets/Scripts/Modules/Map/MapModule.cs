﻿/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

using UnityEngine;
using System.Collections.Generic;

public class MapModule : Module, IMapModule 
{	
	[SerializeField]
	GameObject mapTilePrefab;
	public Map Map{get; private set;}
	SpriteModule sprites;
	MovementModule movement;

	public void Init(string[,] tiles, TileType[] tileTypes, SpriteModule sprites, MovementModule movement) {
		this.Map = new Map(parseTilesToMap(tiles, tileTypes), this);
		this.sprites = sprites;
		this.movement = movement;
		createMap(this.Map);
	}

	protected override void CleanupReferences ()
	{
		base.CleanupReferences ();
	}

	void createMap(Map map) {
		for(int x = 0; x < map.Width; x++) {
			for (int y = 0; y < map.Height; y++) {
				MapTile tileInfo = map.GetTile(x, y);
				MapTileBehaviour tile = Instantiate(mapTilePrefab, transform).GetComponent<MapTileBehaviour>();
				tile.SetTile(map, tileInfo, sprites.GetTile(tileInfo.TileType));
				tile.name = tileInfo.TileType.TileName;
			}
		}
	}

	MapTile[,] parseTilesToMap(string[,] tileKeys, TileType[] tileTypes) {
		int width = tileKeys.GetLength(0);
		int height = tileKeys.GetLength(1);
		Dictionary<string, TileType> tileLookup = getTileLookup(tileTypes);
		MapTile[,] tiles = new MapTile[width, height];
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				try {
					tiles[x, y] = new MapTile(x, y, tileLookup[tileKeys[x, y].ToLower()]);
				} catch {
					Debug.LogErrorFormat("{0} at ({1}, {2}) is an illegal key", tileKeys[x, y].ToLower(), x, y);
					tiles[x, y] = MapTile.Default;
				}
			}
		}
		return tiles;
	}

	MapTile getTileFromLoc(MapLocation location) {
		return Map.GetTile(location);
	}

	public void PlaceUnit (Agent agent) {
		MapLocation location = agent.GetStartLocation();
		MapTile tile = getTileFromLoc(location);
		tile.PlaceUnit(agent);
	}

	Dictionary<string, TileType> getTileLookup(TileType[] tileTypes) {
		Dictionary<string, TileType> tileLookup = new Dictionary<string, TileType>();
		foreach (TileType tile in tileTypes) {
			tileLookup.Add(tile.Key.ToLower(), tile);
		}
		return tileLookup;
	}
		
	public bool CoordinateIsInBounds (int x, int y) {
		return Map.CoordinateIsInBounds(x, y);
	}

	public bool CoordinateIsInBounds(MapLocation loc) {
		return Map.CoordinateIsInBounds(loc);
	}

	public MapTile GetTile (int x, int y) {
		return Map.GetTile(x, y);
	}

	public bool CanTravelTo(Agent agent, MapLocation loc) {
		if(CoordinateIsInBounds(loc)) {
			MapTile tile = getTileFromLoc(loc);
			return movement.CanMoveToTile(agent.GetUnit(), tile);
		} else {
			return false;
		}
	}

	public int TravelTo (Agent agent, MapLocation loc) {
		MapTile tile = getTileFromLoc(loc);
		tile.PlaceUnit(agent);
		return tile.TileType.Speed;
	}

	public MapTile RandomTileInRadius(MapLocation center, int radius) {
		return Map.RandomTileInRadius(center, radius);
	}

}
