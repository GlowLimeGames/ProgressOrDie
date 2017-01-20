/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitModule : Module 
{	
	PlayerCharacterBehaviour mainPlayer;

	[SerializeField]
	PlayerCharacterBehaviour playerPrefab;
	[SerializeField]
	EnemyNPCBehaviour enemyPrefab;
	int level = 1;
	CombatModule combat;
	StatModule stats;
	TuningModule tuning;
	TurnModule turns;

	MovementModule movement;
	List<Unit> units = new List<Unit>();
	EnemyNPC[] highlightedEnemyTargets = new EnemyNPC[0];

	public float BulkToHPRatio {
		get {
			return stats.BulkToHPRatio;
		}
	}
		
	public PlayerCharacter Player() {
		return GetMainPlayer().GetCharacter();
	}

	public void SetLevel(int level) {
		this.level = level;
	}

	public void EarnStatPoints(int statPointsCollected)
	{
		Player().EarnStatPoints(statPointsCollected);
	}

	public void UsePotionOnPlayer() {
		Player().Heal(tuning.HealthPerecentGainFromPotion);
	}

	public void Init(MapModule map, 
		string[,] units,
		EnemyData enemyInfo,
		TurnModule turns,
		MovementModule movement,
		CombatModule combat, 
		StatModule stats,
		AbilitiesModule abilities,
		TuningModule tuning,
		PrefabModule prefabs,
		bool createWorld,
		bool newCharacter
	){
		this.combat = combat;
		this.stats = stats;
		this.tuning = tuning;
		this.turns = turns;
		this.movement = movement;
		if (createWorld) {
			movement.SubscribeToAgentMove (handleAgentMove);
			turns.SubscribeToTurnSwitch (handleTurnSwitch);
			createUnits(map.Map, units, enemyInfo, newCharacter);
			placeUnits (map, this.units.ToArray (), turns, movement, combat, stats, abilities, prefabs);
		} else {
			GameObject go = new GameObject ();
			mainPlayer = go.AddComponent<PlayerCharacterBehaviour> ();
			mainPlayer.SetCharacter (new PlayerCharacter (this, new MapLocation(0, 0), 
				map.Map, tuning.StartingStatPoints, newCharacter:true));
		}
	}
		
	public string GetPlayerCritChanceAsPercentStr(PlayerCharacter player){
		return stats.GetPlayerCritChanceAsPercentStr(player);
	}

	public float GetPlayerCritChanceAsPercentf(PlayerCharacter player){
		return stats.GetPlayerCritChanceAsPercentf(player);
	}

	public void HandleUnitDestroyed(Unit unit) {
		units.Remove(unit);
		if(unit is EnemyNPC) 
		{
			EnemyNPC enemy = unit as EnemyNPC;
			Player().EarnStatPoints(enemy.StatPointsOnKill);
			EventModule.Event("EnemyDeath");
		}
	}

	void handleAgentMove (Agent agent) {
		if (agent is PlayerCharacterBehaviour) {
			handlePlayerMove();
		}
	}
		
	void handlePlayerMove () {
		unhighlightEnemies();
		highlightEnemiesRange();
	}

	void highlightEnemiesRange () {
		highlightedEnemyTargets = GetEnemiesInRange(GetMainPlayer().GetCharacter());
		foreach (EnemyNPC enemy in highlightedEnemyTargets) {
			enemy.HighlightToAttack();
		}
	}

	void unhighlightEnemies () {
		foreach  (EnemyNPC enemy in highlightedEnemyTargets) {
			enemy.Unhighlight();
		}
		highlightedEnemyTargets = new EnemyNPC[0];
	}

	void handleTurnSwitch (AgentType turn) {
		if (turn == AgentType.Player) {
			highlightEnemiesRange();
		} else if (turn == AgentType.Enemy) {
			unhighlightEnemies();
			handleEnemyTurn();
		}
	}
		
	public bool PlayerHasUnspentSkillPoints() {
		return Player().HasUnspentStatPoints();
	}

	public int GetAvailablePlayerSkillPoints() {
		return Player().GetUnspentStatPoints();
	}

	public void MeleeAttack (IUnit attacker, IUnit target) {
		combat.MeleeAttack(attacker, target);
	}

	public void MagicAttack (IUnit attacker, IUnit target) {
		combat.MagicAttack(attacker, target);
	}

	public PlayerCharacterBehaviour GetMainPlayer () {
		return this.mainPlayer;
	}

	public void HandlePlayerKilled()
	{
		loadGameOver();
	}

	public EnemyNPC[] GetEnemiesInRange (PlayerCharacter player) {
		List<EnemyNPC> inRange = new List<EnemyNPC>();
		foreach (Unit unit in units) {
			if (unit is EnemyNPC) {
				EnemyNPC enemy = unit as EnemyNPC;
				if (combat.IsTargetInRange(player, enemy, AttackType.Magic)) {
					inRange.Add(enemy as EnemyNPC);
				}
			}
		}
		return inRange.ToArray();
	}

	void createUnits(Map map, string[,] units, EnemyData enemyInfo, bool newCharacter) {
		Dictionary<string, EnemyDescriptor> lookup = getEnemyLookup(enemyInfo);
		for (int x = 0; x < map.Width; x++) {	
			for (int y = 0; y < map.Width; y++) {
				string tileUnit = string.Concat(units[x, y], level);
				if (isUnit(tileUnit)) {
					Unit unit = null;
					MapLocation startLocation = new MapLocation(x, y);
					if(isPlayer(tileUnit)) {
						unit = new PlayerCharacter(this, startLocation, map, tuning.StartingStatPoints, newCharacter);
					} else {
						EnemyDescriptor descr;
						if(lookup.TryGetValue(tileUnit, out descr)) {
							unit = new EnemyNPC(this, descr, startLocation, map);
						}
					}
					if (unit != null) {
						unit.ResetStats();
						this.units.Add(unit);
					}
				}
			}
		}
	}

	void placeUnits(MapModule map, 
		Unit[] units, 
		TurnModule turns,
		MovementModule movement, 
		CombatModule combat,
		StatModule stats,
		AbilitiesModule abilities,
		PrefabModule prefabs
	) {
		
		for (int i = 0; i < units.Length; i++) {
			Agent agent;
			Unit unit = units[i];
			if (unit is PlayerCharacter) {
				agent = getPlayer(unit as PlayerCharacter);
				mainPlayer = agent as PlayerCharacterBehaviour;
			} else if (unit is EnemyNPC) {
				agent = getEnemy(unit as EnemyNPC, prefabs);
			} else {
				// Skip this unit: it's not supported
				continue;
			}
			agent.Init(map, turns, movement, combat, stats, abilities);
			map.PlaceUnit(agent);
		}
	}
		
	EnemyNPCBehaviour getEnemy (EnemyNPC data, PrefabModule prefabs) {
		EnemyNPCBehaviour enemy = prefabs.GetEnemy(data.Descriptor.Key);
		enemy.SetEnemy(data);
		enemy.transform.SetParent(transform);
		return enemy;
	}

	PlayerCharacterBehaviour getPlayer (PlayerCharacter data) {
		PlayerCharacterBehaviour player = Instantiate(playerPrefab, transform);	
		player.SetCharacter(data);
		return player;
	}

	bool isUnit(string unitKey) {
		return !string.IsNullOrEmpty(unitKey);
	}

	bool isPlayer(string unitKey) {
		return unitKey.Equals(string.Concat(tuning.PlayerKey, level));
	}

	Dictionary<string, EnemyDescriptor> getEnemyLookup (EnemyData enemyInfo) {
		Dictionary<string, EnemyDescriptor> lookup = new Dictionary<string, EnemyDescriptor>();
		foreach (EnemyDescriptor descriptor in enemyInfo.Enemies) {
			lookup.Add(string.Concat(descriptor.Key, descriptor.Level), descriptor);
		}
		return lookup;
	}

	public void ChangePlayerUnspentStatPoints(int delta) {
		if(delta > 0) {
			Player().EarnStatPoints(delta);
		} else if (delta < 0) {
			Player().TrySpendStatPoints(Mathf.Abs(delta));
		}
	}

	public void ChangePlayerStrength(int delta) {
		Player().ModStrength(delta);	
	}

	public void ChangePlayerSpeed(int delta) {
		Player().ModSpeed(delta);	
	}

	public void ChangePlayerConstitution(int delta) {
		Player().ModConstitution(delta);	
	}

	public void ChangePlayerMagic(int delta) {
		Player().ModMagic(delta);
	}

	public void ChangePlayerSkill(int delta) {
		Player().ModSkill(delta);
	}
		
	void modStrength(Unit unit, int delta) {
		unit.ModStrength(delta);
	}

	void modSpeed(Unit unit, int delta) {
		unit.ModSpeed(delta);
	}

	void modConstitution(Unit unit, int delta) {
		unit.ModConstitution(delta);
	}

	void modMagic(Unit unit, int delta) {
		unit.ModMagic(delta);
	}

	void modSkill(Unit unit, int delta) {
		unit.ModSkill(delta);
	}

	void handleEndEnemyTurn()
	{
		EventModule.Event(PODEvent.EnemyTurnEnd);
		turns.NextTurn();
	}

	void handleEnemyTurn()
	{
		EventModule.Event(PODEvent.EnemyTurnStart);
		EnemyNPC[] enemiesSorted = sortEnemiesByTurnPriority(this.units);
		float turnTimerPerEnemy = getEnemyTurnTimePerEnemy(enemiesSorted);
		StartCoroutine(takeEnemiesTurnInOrder(enemiesSorted, turnTimerPerEnemy, handleEndEnemyTurn));
	}

	float getEnemyTurnTimePerEnemy(EnemyNPC[] enemies)
	{
		return tuning.TimeToMove / (float) enemies.Length;
	}

	EnemyNPC[] sortEnemiesByTurnPriority(List<Unit> units) {
		Sort<EnemyNPC> sorter = new SelectionSort<EnemyNPC>();
		EnemyNPC[] enemies = getAllEnemies();
		return sorter.run(enemies);
	}

	EnemyNPC[] getAllEnemies()
	{
		return units.OfType<EnemyNPC>().ToArray();
	}

	IEnumerator takeEnemiesTurnInOrder(EnemyNPC[] enemyOrder, float timerPerTurn, MonoAction callback = null)
	{
		EventModule.Event("EnemyFootsteps");
		foreach(EnemyNPC enemy in enemyOrder)
		{
			handleIndividualEnemyTurn(enemy);
			yield return new WaitForSeconds(timerPerTurn);
		}
		if(callback != null) {
			callback();
		}
		EventModule.Event("StopEnemyFootsteps");
	}

	void handleIndividualEnemyTurn(EnemyNPC enemy)
	{
		Unit target;
		if(hasTargetToAttack(enemy, out target)) {
			AttackType[] attacks = enemy.GetAvailableAttacks();
			if(attacks[0] == AttackType.Magic) {
				combat.MagicAttack(enemy, target as IUnit);
			} else if (attacks[0] == AttackType.Melee) {
				combat.MeleeAttack(enemy, target as IUnit);
			}
		}
		handleEnemyMovement(enemy);
	}

	void handleEnemyMovement(EnemyNPC enemy) {
		movement.DetermineEnemyMovement(enemy);
	}

	bool hasTargetToAttack(EnemyNPC enemy, out Unit validTarget)
	{
		return combat.HasTargetToAttack(enemy, out validTarget);
	}
		
}
