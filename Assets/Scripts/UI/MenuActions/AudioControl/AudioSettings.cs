using UnityEngine;
using System.Collections;

public class AudioSettings : MonoBehaviour {
	public bool BGM = false;
	
	// Check if BGM is muted or not
	void Start () {
		if(BGM){
			audio.mute = AudioControl.getBgmOnOff();
		}
	}
}
