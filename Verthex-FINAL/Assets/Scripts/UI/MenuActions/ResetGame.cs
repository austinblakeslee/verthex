using UnityEngine;
using System.Collections;

public class ResetGame : DefaultMenuAction,MenuAction {
	// This menu action changes the scene
	public override void Action() {
		Application.LoadLevel(0);
	}
}
