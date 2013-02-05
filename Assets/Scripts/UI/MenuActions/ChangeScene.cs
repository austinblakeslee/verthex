using UnityEngine;
using System.Collections;

public class ChangeScene : DefaultMenuAction,MenuAction {
	public string scene = "";
	public string gameType = "";

	// This menu action changes the scene
	public override void Action() {
		PlayClickSound();
		GameType.setGameType(gameType);
		Application.LoadLevel(scene);
	}
}
