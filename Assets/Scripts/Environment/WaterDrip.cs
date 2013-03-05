using UnityEngine;
using System.Collections;

public class WaterDrip : MonoBehaviour {

	public Section mySection;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(mySection == TowerSelection.GetSelectedSection()) {
			particleSystem.Play();
		} else {
			particleSystem.Stop();
		}
	}
}
