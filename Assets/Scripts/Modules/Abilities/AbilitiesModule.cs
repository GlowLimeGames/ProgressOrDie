/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

using System.Collections.Generic;

public class AbilitiesModule : Module
{
	Dictionary<string, PlayerAbility> abilityLookup;

	public void Init(AbilityData data) {
		this.abilityLookup = createLookup(data);
	}

	Dictionary<string, PlayerAbility> createLookup (AbilityData data) {
		Dictionary<string, PlayerAbility> lookup = new Dictionary<string, PlayerAbility>();
		foreach (PlayerAbility ability in data.Abilities) {
			lookup.Add(ability.Ability, ability);
		}
		return lookup;
	}

	public bool TryGetAbility (string abilityName, out PlayerAbility ability) {
		return abilityLookup.TryGetValue(abilityName, out ability);
	}

}
