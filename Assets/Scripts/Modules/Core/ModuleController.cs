/*
 * Author: Isaiah Mann
 * Desc: Handles controller all the modules
 */

using UnityEngine;
using System.IO;

public class ModuleController : SingletonController<ModuleController> {
	const string UNITS = "Units";
	const string TILES = "Tiles";

	[SerializeField]
	bool createWorld = true;

	[SerializeField]
	string levelName = "Example";

	[SerializeField]
	bool levelOverride = false;

	[SerializeField]
	ParserModule parser;

	[SerializeField]
	AudioModule sound;

	[SerializeField]
	SpriteModule sprites;

	[SerializeField]
	MapModule map;

	[SerializeField]
	UnitModule unit;

	[SerializeField]
	UIModule ui;

	[SerializeField]
	CameraModule cam;

	[SerializeField]
	TurnModule turn;

	[SerializeField]
	AbilitiesModule abilities;

	[SerializeField]
	CombatModule combat;

	[SerializeField]
	GameEndModule gameEnd;

	[SerializeField]
	MovementModule movement;

	[SerializeField]
	SaveModule save;

	[SerializeField]
	StatModule stats;

	[SerializeField]
	TuningModule tuning;

	[SerializeField]
	LegendModule legends;

	[SerializeField]
	NotificationModule notifications;

	[SerializeField]
	PrefabModule prefabs;

	protected override void SetReferences ()
	{
		base.SetReferences ();
		if(!levelOverride) {
			levelName = PlayerPrefs.GetString(LEVEL);
		}
		TuningData tuningData = parser.ParseJSONFromResources<TuningData>("Tuning");
		tuning.Init(tuningData);
		stats.Init(tuning);

		TileData tileData = parser.ParseJSONFromResources<TileData>(TILES);
		string[,] tiles = parser.ParseCSVFromResources(getTilesCSVPath(levelName));
		if (createWorld) {
			map.Init (tiles, tileData.Tiles, sprites, movement);
		}

		EnemyData enemyData = parser.ParseJSONFromResources<EnemyData>("Enemies");
		string[,] units = parser.ParseCSVFromResources(getUnitsCSVPath(levelName));
		unit.Init (map, units, enemyData, turn, movement, combat, stats, abilities, tuning, prefabs, createWorld);
		if (createWorld) {
			cam.StartFollowing (unit.GetMainPlayer ());
			movement.Init (turn, tuning, map);
			combat.Init (unit, map, abilities, stats, gameEnd);
		}
		ui.Init (levelName, turn, unit, tuning, createWorld);
		LegendData legendData = parser.ParseJSONFromResources<LegendData>("Legends");
		legends.Init(stats, unit, legendData);

		AbilityData abilityData = parser.ParseJSONFromResources<AbilityData>("Abilities");
		abilities.Init(abilityData);
		if(createWorld) {
			EventModule.Event("Ambience");
			EventModule.Event("PlayInGameMusic");
		}
	}

	protected override void CleanupReferences ()
	{
		base.CleanupReferences ();
		if(createWorld) {
			EventModule.Event("StopAmbience");
			EventModule.Event("PlayMenuMusic");
		}
	}

	string getUnitsCSVPath(string levelName)
	{
		return Path.Combine(levelName, UNITS);
	}

	string getTilesCSVPath(string levelName)
	{
		return Path.Combine(levelName, TILES);
	}
		
}
