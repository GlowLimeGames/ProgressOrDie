/*
 * Author(s): Isaiah Mann
 * Description: Loads sprites for the game from resources
 * Usage: [no notes]
 */

using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class SpriteModule : Module 
{	
	const string TILE_DIR = "Tiles";
	const string ENEMY_DIR = "Enemies";

	const string TILES_SUFFIX = "Tile";

	Dictionary<string, Sprite> spriteLookup = new Dictionary<string, Sprite>();

	public Sprite GetEnemy(EnemyDescriptor descriptor) {
		string fileName = getEnemyFileName(descriptor);
		return fetchSprite(fileName);
	}

	public Sprite GetTile(TileType type)
	{
		string fileName = getTileFileName(type);
		return fetchSprite(fileName);
	}

	Sprite fetchSprite(string fileName) {
		Sprite sprite;
		if(spriteLookup.TryGetValue(fileName, out sprite))
		{
			return sprite;
		}
		else
		{
			sprite = loadSpriteFromResources(fileName);
			return addSpriteToLookup(fileName, sprite);
		}
	}

	Sprite addSpriteToLookup(string fileName, Sprite sprite)
	{
		spriteLookup.Add(fileName, sprite);
		return sprite;
	}

	Sprite loadSpriteFromResources(string fileName)
	{
		Sprite sprite = Resources.Load<Sprite>(spritePath(fileName));
		return sprite;
	}

	string getTileFileName(TileType type)
	{
		return Path.Combine(TILE_DIR, string.Format("{0}{1}", type.TileName.Trim(), TILES_SUFFIX));
	}

	string getEnemyFileName(EnemyDescriptor descriptor) {
		return Path.Combine(ENEMY_DIR, descriptor.Key.Trim());
	}
}
