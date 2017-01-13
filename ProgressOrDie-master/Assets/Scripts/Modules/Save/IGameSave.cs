/*
 * Author(s): Isaiah Mann
 * Desc:
 */

public interface IGameSave {

	int GetDeathStreak();
	int GetWinStreak();

	IPlayerCharacter GetPlayer();

	IGameSession GetMostRecentSession();

}
