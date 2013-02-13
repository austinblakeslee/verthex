using UnityEngine;
using System.Collections;

public class SelectedFactionUpdate : DefaultMenuAction,MenuAction {

	public string factionLabel;

	// Use this for initialization
	public override void Action() {
		GameValues.myFaction = factionLabel;
	}
}
