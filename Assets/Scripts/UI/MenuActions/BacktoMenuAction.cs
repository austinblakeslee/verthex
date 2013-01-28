using UnityEngine;
using System.Collections;

public class BacktoMenuAction : DefaultMenuAction,MenuAction {

	public override void Action() {
		Application.LoadLevel(0);
	}
}