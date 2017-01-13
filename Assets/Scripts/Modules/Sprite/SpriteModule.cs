/*
 * Author(s): Isaiah Mann
 * Description: Loads sprites for the game from resources
 * Usage: [no notes]
 */

using UnityEngine;
using System.Collections.Generic;

public class SpriteModule : Module 
{	
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
		return Resources.Load<Sprite>(spritePath(fileName));
	}

	string getTileFileName(TileType type)
	{
		return string.Format("{0}{1}", type.TileName, TILES_SUFFIX);
	}

	string getEnemyFileName(EnemyDescriptor descriptor) {
		return descriptor.Key;
	}
}
