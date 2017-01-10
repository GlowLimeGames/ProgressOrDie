/*
 * Author: Isaiah Mann
 * Desc: Handles controller all the modules
 */

using UnityEngine;

public class ModuleController : SingletonController<ModuleController> {
	[SerializeField]
	ParserModule parser;

	[SerializeField]
	AudioModule sound;

	[SerializeField]
	EventModule events;

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

	protected override void SetReferences ()
	{
		base.SetReferences ();

		TuningData tuningData = parser.ParseJSONFromResources<TuningData>("Tuning");
		tuning.Init(tuningData);
		stats.Init(tuning);

		TileData tileData = parser.ParseJSONFromResources<TileData>("Tiles");
		string[,] tiles = parser.ParseCSVFromResources("Example/Tiles");
		map.Init(tiles, tileData.Tiles, sprites);

		EnemyData enemyData = parser.ParseJSONFromResources<EnemyData>("Enemies");
		string[,] units = parser.ParseCSVFromResources("Example/Units");
		unit.Init(map, sprites, units, enemyData, turn, movement, combat, stats, abilities);
		cam.StartFollowing(unit.GetMainPlayer());
		ui.Init(turn, unit);
		movement.Init(turn);
		combat.Init(unit, map, abilities, stats, gameEnd);

		LegendData legendData = parser.ParseJSONFromResources<LegendData>("Legends");
		legends.Init(stats, unit, legendData);

		AbilityData abilityData = parser.ParseJSONFromResources<AbilityData>("Abilities");
		abilities.Init(abilityData);
	}
		
}
