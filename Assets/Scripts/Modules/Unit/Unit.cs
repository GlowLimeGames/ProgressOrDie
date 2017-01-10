/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

public abstract class Unit
{
	protected UnitModule parentModule;

	public bool HasAgentLink {
		get {
			return agent != null;
		}
	}

	protected Agent agent;
	public int RemainingHealth {get; private set;}

	public bool IsDead {
		get {
			return RemainingHealth <= 0;
		}
	}

	public void LinkToAgent (Agent agent) {
		this.agent = agent;
	}

	public void UnlinkFromAge () {
		this.agent = null;
	}

	int getMaxHealth {
		get {
			return (int) (parentModule.BulkToHPRatio * (float) GetConstitution()); 
		}
	}

	public abstract int GetSpeed();
	public abstract int GetMagic ();
	public abstract int GetConstitution();
	public abstract int GetStrength ();
	public abstract int GetSkill ();

	public virtual int ModSpeed(int delta) {
		return GetSpeed();
	}

	public virtual int ModMagic (int delta) {
		return GetMagic();
	}

	public virtual int ModConstitution(int delta) {
		return GetConstitution();
	}

	public virtual int ModStrength (int delta) {
		return GetStrength();
	}

	public virtual int ModSkill (int delta) {
		return GetSkill();
	}

	public int X {
		get {
			return this.GetLocation().X;
		}
	}

	public int Y {
		get {
			return this.GetLocation().Y;	
		}
	}

	public Map Map{get; private set;}
	public MapLocation StartingLocation {get; private set;}

	public Unit(UnitModule parent, MapLocation location, Map map) {
		this.parentModule = parent;
		this.StartingLocation = location;
		this.Map = map;
	}

	public void ResetStats () {
		this.RemainingHealth = getMaxHealth;
	}

	public void HighlightToAttack () {
		if (HasAgentLink) {
			agent.HighlightToAttack();
		}
	}

	public void Unhighlight () {
		if (HasAgentLink) {
			agent.Unhighlight();
		}
	}

	MapTile occupiedTile;
	public void SetTile (MapTile tile) {
		this.occupiedTile = tile;
	}

	public MapLocation GetLocation () {
		return this.occupiedTile.GetLocation();
	}

	public AttackType[] GetAvailableAttacks() {
		throw new System.NotImplementedException();
	}

	public void Damage (int damage) {
		this.RemainingHealth -= damage;
		if (IsDead) {
			Kill();
		}
	}

	public bool CanMoveTo (IMapTile tile) {
		throw new System.NotImplementedException();
	}

	public bool CanAttack (IUnit unit, AttackType attack) {
		throw new System.NotImplementedException();
	}

	public bool CanMeleeAttack (IUnit unit) {
		throw new System.NotImplementedException();
	}

	public bool CanMagicAttack (IUnit unit) {
		throw new System.NotImplementedException();
	}

	public void MoveTo (IMapTile tile) {
		throw new System.NotImplementedException();
	}

	public void Attack(IUnit unit, AttackType attack) {
		if (attack == AttackType.Melee) {
			MeleeAttack(unit);
		} else if (attack == AttackType.Magic) {
			MagicAttack(unit);
		}
	}

	public void MeleeAttack(IUnit unit) {
		parentModule.MeleeAttack(this as IUnit, unit);
	}

	public void MagicAttack(IUnit unit) {
		parentModule.MagicAttack(this as IUnit, unit);
	}

	public void Kill () {
		parentModule.HandleUnitDestroyed(this);		
		if (HasAgentLink) {
			UnityEngine.Object.Destroy(agent.gameObject);
		}
	}
}
