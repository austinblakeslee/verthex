using UnityEngine;
using System.Collections;

public class PrevAction : DefaultMenuAction,MenuAction {

	public override void Action() {
		GameObject.FindWithTag("MainCamera").transform.Translate(-1600,0,0);
		GameObject.FindWithTag("MainCamera").GetComponent<TutorialCamera>().slideNumDown();
	}
}
