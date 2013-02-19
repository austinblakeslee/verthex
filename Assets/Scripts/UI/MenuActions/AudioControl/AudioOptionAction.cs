using UnityEngine;
using System.Collections;

public class AudioOptionAction : DefaultMenuAction,MenuAction {
	
	public override void Action() {
		//Will find Title Menu Main Camera and mute the BGM
		GameObject.FindWithTag("MainCamera").audio.mute = !GameObject.FindWithTag("MainCamera").audio.mute;
		
		//Toggles In Game BGM On/Off
		AudioControl.bgmOnOff();
	}
	
	void Update () {
	
	}
}
