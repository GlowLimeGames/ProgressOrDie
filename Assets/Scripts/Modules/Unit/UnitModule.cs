/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

using System.Collections.Generic;
using UnityEngine;

public class UnitModule : Module 
{	
	PlayerCharacterBehaviour mainPlayer;

	[SerializeField]
	PlayerCharacterBehaviour playerPrefab;
	[SerializeField]
	EnemyNPCBehaviour enemyPrefab;

	CombatModule combat;
	StatModule stats;

	const string PLAYER_KEY = "P";
	List<Unit> units = new List<Unit>();
	EnemyNPC[] highlightedEnemyTargets = new EnemyNPC[0];

	public float BulkToHPRatio {
		get {
			return stats.BulkToHPRatio;
		}
	}

	PlayerCharacter player() {
		return GetMainPlayer().GetCharacter();
	}

	public void Init(MapModule map, 
		SpriteModule sprites, 
		string[,] units,
		EnemyData enemyInfo,
		TurnModule turns,
		MovementModule movement,
		CombatModule combat, 
		StatModule stats,
		AbilitiesModule abilities
	){
		this.combat = combat;
		this.stats = stats;
		movement.SubscribeToAgentMove(handleAgentMove);
		turns.SubscribeToTurnSwitch(handleTurnSwitch);
		createUnits(map.Map, units, enemyInfo);
		placeUnits(map, sprites, this.units.ToArray(), turns, movement, combat, stats, abilities);
	}
		
	public void HandleUnitDestroyed(Unit unit) {
		// TODO: Implement real functionality

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
		}
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

	void createUnits(Map map, string[,] units, EnemyData enemyInfo) {
		Dictionary<string, EnemyDescriptor> lookup = getEnemyLookup(enemyInfo);
		for (int x = 0; x < map.Width; x++) {	
			for (int y = 0; y < map.Width; y++) {
				string tileUnit = units[x, y];
				if (isUnit(tileUnit)) {
					Unit unit = null;
					MapLocation startLocation = new MapLocation(x, y);
					if(isPlayer(tileUnit)) {
						unit = new PlayerCharacter(this, startLocation, map);
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
		SpriteModule sprites, 
		Unit[] units, 
		TurnModule turns,
		MovementModule movement, 
		CombatModule combat,
		StatModule stats,
		AbilitiesModule abilities
	) {
		
		for (int i = 0; i < units.Length; i++) {
			Agent agent;
			Unit unit = units[i];
			if (unit is PlayerCharacter) {
				agent = getPlayer(unit as PlayerCharacter);
				mainPlayer = agent as PlayerCharacterBehaviour;
			} else if (unit is EnemyNPC) {
				agent = getEnemy(unit as EnemyNPC, sprites);
			} else {
				// Skip this unit: it's not supported
				continue;
			}
			agent.Init(turns, movement, combat, stats, abilities);
			map.PlaceUnit(agent);
		}

	}
		
	EnemyNPCBehaviour getEnemy (EnemyNPC data, SpriteModule sprites) {
		EnemyNPCBehaviour enemy = Instantiate(enemyPrefab, transform);	
		enemy.SetEnemy(data);
		enemy.SetSprite(sprites.GetEnemy(data.Descriptor));
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
		return unitKey.Equals(PLAYER_KEY);
	}

	Dictionary<string, EnemyDescriptor> getEnemyLookup (EnemyData enemyInfo) {
		Dictionary<string, EnemyDescriptor> lookup = new Dictionary<string, EnemyDescriptor>();
		foreach (EnemyDescriptor descriptor in enemyInfo.Enemies) {
			lookup.Add(descriptor.Key, descriptor);
		}
		return lookup;
	}

	public void ChangePlayerStrength(int delta) {
		player().ModStrength(delta);	
	}

	public void ChangePlayerSpeed(int delta) {
		player().ModSpeed(delta);	
	}

	public void ChangePlayerConstitution(int delta) {
		player().ModConstitution(delta);	
	}

	public void ChangePlayerMagic(int delta) {
		player().ModMagic(delta);
	}

	public void ChangePlayerSkill(int delta) {
		player().ModSkill(delta);
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

}
