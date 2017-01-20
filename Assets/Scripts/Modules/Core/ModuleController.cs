/*
 * Author: Isaiah Mann
 * Desc: Handles controller all the modules
 */

using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class ModuleController : SingletonController<ModuleController> {
	const string UNITS = "Units";
	const string TILES = "Tiles";
	const string NEW_CHARACTER = "NewCharacter";
	bool newCharacter {
		get {
			return PlayerPrefsUtil.GetBool(NEW_CHARACTER, true);
		}

		set {
			PlayerPrefsUtil.SetBool(NEW_CHARACTER, value);
		}
	}

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
		unit.Init (map, units, enemyData, turn, movement, combat, stats, abilities, 
			tuning, prefabs, createWorld, newCharacter);
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

	public void HandleGameOver() 
	{
		newCharacter = true;
	}
		
	public void HandleLevelCleared()
	{
		newCharacter = false;
		if(hasNextLevel()) {
			PlayerPrefs.SetString(LEVEL, getNextLevelName());
			SceneManager.LoadScene(GAME_INDEX);
		} 
		else 
		{
			SceneManager.LoadScene(MAIN_MENU_INDEX);
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
		
	protected override void SubscribeEvents ()
	{
		base.SubscribeEvents ();
		EventModule.Subscribe(handlePODEvent);
	}

	protected override void UnsubscribeEvents ()
	{
		base.UnsubscribeEvents ();
		EventModule.Unsubscribe(handlePODEvent);
	}

	void handlePODEvent(PODEvent gameEvent) {
		if(gameEvent == PODEvent.PlayerKilled) {
			HandleGameOver();
		} else if (gameEvent == PODEvent.BossKilled) {
			HandleLevelCleared();
		}
	}

	#if UNITY_EDITOR

	// DEBUGGING ONLY
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Q)) {
			EventModule.Event(PODEvent.BossKilled);
		}
	}

	#endif

	string getNextLevelName () {
		switch(levelName) 
		{
			case "Level1":
				return "Level2";
			case "Level2":
				return "Level3";
			default: return string.Empty;
		}
	}

	bool hasNextLevel() 
	{
		return !string.IsNullOrEmpty(getNextLevelName());
	}

}
