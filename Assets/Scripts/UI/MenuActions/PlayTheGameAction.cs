using UnityEngine;
using System.Collections;

public class PlayTheGameAction : DefaultMenuAction,MenuAction {

	public override void Action() {
		Application.LoadLevel(1);
	}
}