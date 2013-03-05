using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {

	public AudioSource bass;
	public AudioSource attackMelody;
	public AudioSource attackDrums;
	public AudioSource normalMelody;
	public AudioSource normalDrums;
	
	private float fadeInVolume = 0.0f;
	private float fadeOutVolume = 1.0f;
	
	public bool BGM = false;

	// Use this for initialization
	void Start () {
		attackMelody.volume = 0.0f;
		attackDrums.volume = 0.0f;
		BGM = AudioControl.getBgmOnOff();	
	}
	
	// Update is called once per frame
	void Update () {
		
		//Check if music was muted from the Options menu or not
		if(BGM){
			bass.audio.mute = AudioControl.getBgmOnOff();
			attackMelody.audio.mute = AudioControl.getBgmOnOff();
			attackDrums.audio.mute = AudioControl.getBgmOnOff();
			normalMelody.audio.mute = AudioControl.getBgmOnOff();
			normalDrums.audio.mute = AudioControl.getBgmOnOff();
		} 
		else{
			if(TurnOrder.IsBattlePhase()) {
			fadeInVolume = Mathf.Min(fadeInVolume + Time.deltaTime, 1.0f);
			fadeOutVolume = Mathf.Max(fadeOutVolume - Time.deltaTime, 0.0f);
			
			attackMelody.volume = fadeInVolume;
			normalMelody.volume = fadeOutVolume;
			
			attackDrums.volume = fadeInVolume;
			normalDrums.volume = fadeOutVolume;
			}
		}
	}
}
