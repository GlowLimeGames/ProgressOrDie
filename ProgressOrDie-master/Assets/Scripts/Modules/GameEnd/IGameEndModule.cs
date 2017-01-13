/*
 * Authors: Isaiah Mann
 * Description: An interface to end of game logic
 */

public interface IGameEndModule {

	void ProcessGameEnd(IGameSave gameProgress, GameEndType endType);

}
