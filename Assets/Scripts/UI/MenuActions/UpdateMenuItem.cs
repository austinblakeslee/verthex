using UnityEngine;
using System.Collections;

public class UpdateMenuItem : DefaultMenuAction,MenuAction {
	public MenuItem toChange;
	public string textToSet;
	public override void Action() {
		toChange.text = textToSet;
	}
}
