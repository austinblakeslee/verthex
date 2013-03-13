using UnityEngine;
using System.Collections;

public class AudioTowers : MonoBehaviour {
	
	public AudioSource Standby;
	public AudioSource Attack;
	
	private float fadeInVolume = 0.0f;
	private float fadeOutVolume = 1.0f;
	
	public bool BGM = false;
	// Use this for initialization
	void Start () {
		Standby.volume = 0.0f;
		Attack.volume = 0.0f;
		BGM = AudioControl.getBgmOnOff();
	}
	
	// Update is called once per frame
	void Update () {
		if(!BGM)
		{
			fadeInVolume = Mathf.Min(fadeInVolume + Time.deltaTime, 1.0f);
			fadeOutVolume = Mathf.Max(fadeOutVolume - Time.deltaTime, 0.0f);
			
			if(TurnOrder.IsBattlePhase())
			{
				Attack.volume = fadeInVolume;
				Standby.volume = fadeOutVolume;
			}
			else
			{
				Standby.volume = fadeInVolume;
				Attack.volume = fadeOutVolume;
			}
				
		}
		else
		{
			Standby.audio.mute = AudioControl.getBgmOnOff();
			Attack.audio.mute = AudioControl.getBgmOnOff();
		}	
	}
}
