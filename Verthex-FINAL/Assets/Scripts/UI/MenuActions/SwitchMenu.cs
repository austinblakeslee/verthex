using UnityEngine;
using System.Collections;

public class SwitchMenu : DefaultMenuAction,MenuAction {
	public GameObject fromMenu = null;
	public GameObject toMenu = null;

	// This menu action changes the menu that is being displayed
	public override void Action() {
		fromMenu.GetComponent<Menu>().on = false;
		toMenu.GetComponent<Menu>().on = true;
		PlayClickSound();
	}
}
