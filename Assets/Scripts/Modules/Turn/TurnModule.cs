﻿/*
 * Authors: Xijie Guo
 * Description: An interface to handle the turn logic
 */

public class TurnModule : Module, ITurnModule {
	public void SwitchToMonTurn() {
		throw new System.NotImplementedException ();
	}

	public bool CheckMonsterAttack(IUnit monster, IUnit player) {
		throw new System.NotImplementedException ();
	}

	public bool CheckForLastMon(IUnit monster, IUnit player) {
		throw new System.NotImplementedException ();
	}

	public void SwitchToPlayerTurn() {
		throw new System.NotImplementedException ();
	}

	public int updateMonster(IUnit monster, int numMonster) {
		throw new System.NotImplementedException ();
	}
}