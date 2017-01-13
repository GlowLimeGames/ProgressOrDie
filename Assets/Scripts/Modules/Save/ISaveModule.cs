/*
 * Author: Isaiah Mann
 * Description: Handles data saving
 */

public interface ISaveModule {

	IGameSave GetSave();
	void Save();
	IGameSave Load();
}
