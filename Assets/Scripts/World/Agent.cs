/*
 * Author(s): Isaiah Mann
 * Description: Super class for all agents operating in the game world
 */

using System;
using UnityEngine;
using System.Collections;

public abstract class Agent : MobileObjectBehaviour {
	protected bool canBeAttacked;
	Color canAttackColor = Color.red;
	SpriteRenderer spriteR;

	protected int remainingAgilityForTurn;

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
			{ReplenishAgility(type);});
	}

	public bool HasUnit {
		get {
			return GetUnit() != null;
		}
	}

	public virtual bool ReplenishAgility (AgentType type) {
		if (GetAgentType() == type) {
			remainingAgilityForTurn = GetUnit().GetSpeed();
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
		SetPos(tile.WorldPos());
	}

	public void SetPos(Vector3 pos) {
		this.transform.position = pos;
	}
		
	public bool MoveX(int dir) { 
		return move(dir, 0);
	}

	public bool MoveY(int dir) {
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