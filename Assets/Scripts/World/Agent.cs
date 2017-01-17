/*
 * Author(s): Isaiah Mann
 * Description: Super class for all agents operating in the game world
 */

using System;
using UnityEngine;
using System.Collections;

public abstract class Agent : MobileObjectBehaviour {
	protected const string FRONT = "Front";
	protected const string BACK = "Back";
	protected const string LEFT = "Left";
	protected const string RIGHT = "Right";
	protected const string IS_MOVING = "IsMoving";
	protected const string MAGIC = "Magic";
	protected const string MELEE = "Melee";
	protected const string ATTACK = "Attack";

	public bool HasAttackedDuringTurn{get; protected set;}

	protected bool canBeAttacked;

	Color canAttackColor = Color.red;
	SpriteRenderer spriteR;

	protected int remainingAgilityForTurn;
	protected int remainingHealth;

	protected TurnModule turns;
	protected MovementModule movement;
	protected CombatModule combat;
	protected StatModule stats;
	protected AbilitiesModule abilities;
	MapLocation prevLoc;

	public abstract AgentType GetAgentType();

	public void Init (
		TurnModule turns,
		MovementModule movement,
		CombatModule combat,
		StatModule stats,
		AbilitiesModule abilities
	){
		this.turns = turns;
		this.movement = movement;
		this.combat = combat;
		this.stats = stats;
		this.abilities = abilities;
		turns.SubscribeToTurnSwitch(delegate(AgentType type)
			{ReplenishAtTurnStart(type);});
	}

	public int Health () {
		return remainingHealth;
	}

	public virtual void UpdateRemainingHealth(int healthRemaing) {
		this.remainingHealth = healthRemaing;
	}

	public bool HasUnit {
		get {
			return GetUnit() != null;
		}
	}

	public virtual void SetUnit(Unit unit) {
		this.remainingHealth = unit.RemainingHealth;	
		unit.LinkToAgent(this);
	}

	public virtual bool ReplenishAtTurnStart (AgentType type) {
		if (GetAgentType() == type) {
			remainingAgilityForTurn = GetUnit().GetSpeed();
			HasAttackedDuringTurn = false;
			return true;
		} else {
			return false;
		}
	}

	protected Map map {
		get {
			return GetUnit().Map;
		}
	}

	protected MapLocation currentLoc {
		get {
			return GetUnit().GetLocation();
		}
	}

	protected override void SetReferences()
	{
		base.SetReferences();
		spriteR = GetComponent<SpriteRenderer>();
	}

	public virtual void Attack () {
		HasAttackedDuringTurn = true;
	}

	public void SetSprite(Sprite sprite) {
		this.spriteR.sprite = sprite;
	}

	public MapLocation GetStartLocation() {
		return GetUnit().StartingLocation;
	}

	public virtual Unit GetUnit () {
		// Overriden in subclass
		return null;
	}

	public void SetLocation(MapTileBehaviour tile) {
		this.GetUnit().SetTile(tile.Tile);
		MoveToPos(tile.WorldPos());
	}

	// Returns true if animated
	public virtual bool MoveToPos(Vector3 pos) {
		if(movement && movement.IsSetup) {
			moveTo(pos, movement.TimeToMove, stopMoving);
			return remainingAgilityForTurn > 0;
		} else {
			this.transform.position = pos;
			return false;
		}
	}
		
	protected virtual void stopMoving(){
		// NOTHING
	}
	public virtual bool MoveX(int dir) { 
		return move(dir, 0);
	}

	public virtual bool MoveY(int dir) {
		return move(0, dir);
	}

	public void HighlightToAttack () {
		spriteR.color = canAttackColor;
		canBeAttacked = true;
	}

	public void Unhighlight () {
		spriteR.color = Color.white;
		canBeAttacked = false;
	}
		
	protected string getAttackKey(AttackType type) {
		switch(type) {
			case AttackType.Magic:
				return string.Concat(MAGIC, ATTACK);
			case AttackType.Melee:
				return string.Concat(MELEE, ATTACK);
			default:
				return ATTACK;	
		}
	}

	protected bool move (int deltaX, int deltaY) {
		if (movement.CanMove(this)) {
			prevLoc = currentLoc;
			MapLocation newLoc = currentLoc.Translate(deltaX, deltaY);
			if (map.CoordinateIsInBounds(newLoc)) {
				int agilityCost = map.TravelTo(this, newLoc);
				if (trySpendAgility(agilityCost)) {
					movement.Move(this);
					return true;
				} else {
					map.TravelTo(this, prevLoc);
					return false;
				}
			} else {
				return false;
			}
		} else {
			return false;
		}
	}

	protected virtual bool trySpendAgility (int agilityPointsReq) {
		if (remainingAgilityForTurn >= agilityPointsReq) {
			remainingAgilityForTurn -= agilityPointsReq;
			return true;
		} else {
			return false;
		}
	}

	public static int AgentTypeCount () {
		return Enum.GetNames(typeof(AgentType)).Length;
	}
		
	void OnMouseUp () {
		if (canBeAttacked) {
			combat.HandleAttackByPlayer(GetUnit() as IUnit);
		}
	}
}

public enum AgentType {
	Player,
	Enemy,
}