using UnityEngine;
using System.Collections;

public class SelectedFactionLabel : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		GetComponent<MenuItem>().text = GameValues.myFaction;
	}
}
