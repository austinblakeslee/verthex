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
	public static Dictionary<string, object> visualEffects = new Dictionary<string, object>(); 
	public static Dictionary<string, Texture> textures = new Dictionary<string, Texture>();
	public static bool loaded = false;
	public static string myFaction = "Cowboys";
	public static string player1Faction = "EMPTY";
	public static string player2Faction = "EMPTY";
	
	//Section Effect GameObjects and Prefabs to be called by WeaponEffect and SectionEffect and set in the scene
	public Material paralyzedVisual;
	public GameObject forceFieldVisual;
	public GameObject blindedVisual;
	public GameObject taggedVisual;
	public GameObject poisonedVisual;
	public Texture owl;
	public Texture bison;
	public Texture coyote;
	public Texture gooTube;
	public Texture sateliteRing;
	public Texture ufo;
	public Texture waterTower;
	public Texture saloon;
	public Texture jailCell;
	
	
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
			
			textures.Add("owl", owl);
			textures.Add ("bison", bison);
			textures.Add("coyote",coyote);
			textures.Add("gooTube", gooTube);
			textures.Add("sateliteRing", sateliteRing);
			textures.Add("ufo", ufo);
			textures.Add("waterTower", waterTower);
			textures.Add("saloon", saloon);
			textures.Add ("jailCell", jailCell);
			
			floatValues.Add("gravity", gravity);
			floatValues.Add("powerBarSpeed", powerBarSpeed);
			loaded = true;
		
			DontDestroyOnLoad(gameObject);
		}
	}
}