/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

using System.Collections.Generic;
using UnityEngine;

public class PrefabModule : Module
{
	Dictionary<string, EnemyNPCBehaviour> enemyLookup = new Dictionary<string, EnemyNPCBehaviour>();

	public EnemyNPCBehaviour GetEnemy(string enemyKey) {
		EnemyNPCBehaviour enemy;
		if(!enemyLookup.TryGetValue(enemyKey, out enemy)) {
			enemy = loadEnemyFromRresources(enemyKey);
			enemyLookup.Add(enemyKey, enemy);
		}
		return Instantiate(enemy);
	}

	EnemyNPCBehaviour loadEnemyFromRresources(string enemyKey) {
		return loadGameObjectFromResources(getEnemyPath(enemyKey)).GetComponent<EnemyNPCBehaviour>();
	}

	string getEnemyPath(string enemyKey) {
		return FileUtil.CreatePath(PREFABS_DIR, ENEMIES_DIR, enemyKey);
	}

	GameObject loadGameObjectFromResources(string path) {
		return Resources.Load<GameObject>(path);
	}
}
