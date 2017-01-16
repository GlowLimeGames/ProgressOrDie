/*
 * Authors: Xijie Guo
 * Description: An interface to handle the turn logic
 */

public interface ITurnModule { 

	void SwitchToMnstrTurn();

	bool CheckToSwapToNextMnsTurn(IUnit monster);

	bool CheckForLastMnstr(IUnit monster);

	void SwitchToPlayerTurn();

	int SwapToNextMnsTurn(IUnit monster, int numMonster);
}
