using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ValueStore : MonoBehaviour {

	public static SectionMaterial selectedMaterial = null;
	public static SectionWeapon selectedWeapon = null;
	public static bool buttonWasClicked = false;
	public static string helpMessage = "";
	public static string combatLog = "";
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		buttonWasClicked = false;
	}
}
