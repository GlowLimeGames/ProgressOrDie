/*
 * Author(s): Isaiah Mann
 * Description: Meta class that all controllers inherit from (all controllers are singletons)
 */

using UnityEngine;
using System.IO;

public abstract class Module : MonoBehaviourExtended {
	protected const string SPRITE_DIR = "Sprites";
	protected const string JSON_DIR = "JSON";
	protected const string CSV_DIR = "CSV";
	protected const string AUDIO_DIR = "Audio";

	[SerializeField]
	protected string id;

	public virtual string GetId () {
		return id;
	}

	protected override void SetReferences () {
		// Nothing
	}

	protected override void FetchReferences () {
		// Nothing
	}

	protected override void HandleNamedEvent (string eventName) {
		// Nothing
	}

	protected override void CleanupReferences () {
		// Nothing
	}

	protected string spritePath(string fileName)
	{
		return Path.Combine(SPRITE_DIR, fileName);
	}

	protected string jsonPath(string fileName)
	{
		return Path.Combine(JSON_DIR, fileName);
	}

	protected string csvPath(string fileName)
	{
		return Path.Combine(CSV_DIR, fileName);
	}

	protected string audioPath(string fileName)
	{
		return Path.Combine(AUDIO_DIR, fileName);
	}
}
