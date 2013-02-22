using UnityEngine;
using System.Collections;

//Holds audio values from Options Menu

public class AudioControl : MonoBehaviour {
	private static bool bgmOn;
	
	public static void bgmOnOff() {
		bgmOn = !bgmOn;
	}
	
	public static bool getBgmOnOff() {
		return bgmOn;
	}

}
