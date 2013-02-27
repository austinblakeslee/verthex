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

	// Use this for initialization
	void Start () {
		attackMelody.volume = 0.0f;
		attackDrums.volume = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
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
