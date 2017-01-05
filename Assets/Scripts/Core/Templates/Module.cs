﻿/*
 * Author(s): Isaiah Mann
 * Description: Meta class that all controllers inherit from (all controllers are singletons)
 */

using UnityEngine;

public abstract class Module : MonoBehaviourExtended {

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
}