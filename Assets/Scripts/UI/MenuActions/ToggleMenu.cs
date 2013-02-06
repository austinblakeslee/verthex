using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ToggleMenu : DefaultMenuAction,MenuAction {
	public Menu theMenu = null;
	public List<Menu> offMenus = new List<Menu>(); // optional - menus to turn off

	// This menu action changes toggles a menu on/off
	public override void Action() {
		theMenu.on = !theMenu.on;
		foreach (Menu m in offMenus){
			m.on = false;
		}
		if(theMenu && theMenu.on && GetComponent<AudioSource>()) {
			audio.Play();
		} else {
			PlayClickSound();
		}
		ValueStore.helpMessage = "";
	}
}