using UnityEngine;
using System.Collections;

public class QuitGame : DefaultMenuAction,MenuAction {
	// This menu action changes the scene
	public override void Action() {
		Application.Quit();
	}
}
