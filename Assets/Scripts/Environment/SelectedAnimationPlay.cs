using UnityEngine;
using System.Collections;

public class SelectedAnimationPlay : MonoBehaviour {

	public Section mySection;
	private bool playedAnimation = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(TowerSelection.GetSelectedSection() == mySection && !playedAnimation) {
			animation.Play();
			playedAnimation = true;
		} else if(TowerSelection.GetSelectedSection() != mySection && playedAnimation) {
			animation.Rewind();
			playedAnimation = false;
		}
	}
}
