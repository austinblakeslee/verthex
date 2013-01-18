using UnityEngine;
using System.Collections;

public class ChangeScene : DefaultMenuAction,MenuAction {
	public string scene = "";

	// This menu action changes the scene
	public override void Action() {
		PlayClickSound();
		Application.LoadLevel(scene);
	}
}
