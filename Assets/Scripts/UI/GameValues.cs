using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameValues : MonoBehaviour {

	//player editable values
	public int baseResources = 1200;
    public int resourcesPerTurn = 50;
    public int resourcesPerSection = 50;
	public int retrofitCost = 300;
    public int actionsPerTurn = 2;
	public float gravity = 0.5f;
	public float powerBarSpeed = 0.01f;
	
	// dictionaries of editable values for menu inspector integration
	public static Dictionary<string, int> intValues = new Dictionary<string, int>();
	public static Dictionary<string, float> floatValues = new Dictionary<string, float>();
	public static Dictionary<string, GameObject> visualEffects = new Dictionary<string, GameObject>(); 
	public static bool loaded = false;
	public static string myFaction = "Cowboys";
	public static string player1Faction = "EMPTY";
	public static string player2Faction = "EMPTY";
	
	//Section Effect GameObjects and Prefabs to be called by WeaponEffect and SectionEffect and set in the scene
	public GameObject paralyzedVisual;
	public GameObject forceFieldVisual;
	public GameObject blindedVisual;
	public GameObject taggedVisual;
	public GameObject poisonedVisual;
	
	
	void Awake () {
		if(!loaded) {
			intValues.Add("baseResources", baseResources);
			intValues.Add("retrofitCost", retrofitCost);
			intValues.Add("actionsPerTurn", actionsPerTurn);
			intValues.Add("resourcesPerTurn", resourcesPerTurn);
			intValues.Add("resourcesPerSection", resourcesPerSection);
			
			visualEffects.Add ("paralyzedVisual", paralyzedVisual);
			visualEffects.Add ("forceFieldVisual", forceFieldVisual);
			visualEffects.Add ("blindedVisual", blindedVisual);
			visualEffects.Add ("taggedVisual", taggedVisual);
			visualEffects.Add ("poisonedVisual", poisonedVisual);
			
			floatValues.Add("gravity", gravity);
			floatValues.Add("powerBarSpeed", powerBarSpeed);
			loaded = true;
		
			DontDestroyOnLoad(gameObject);
		}
	}
}