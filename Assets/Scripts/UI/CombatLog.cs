using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CombatLog : MonoBehaviour {

	public static int NUM_LINES = 50;
	public static float PER_LINE = 15.2f;

	public static List<string> messages;
	
	public static void addLine(string m) {
		messages.RemoveAt(0);
		string message = m;
		messages.Add(message);
		MenuItem combatLogMenu = GameObject.FindWithTag("CombatLog").GetComponent<MenuItem>();
		combatLogMenu.text = GetString();
		combatLogMenu.scrollPosition = new Vector2(0, PER_LINE * NUM_LINES);
	}
	
	public static void addLineNoPlayer(string m) {
		messages.RemoveAt(0);
		messages.Add(m);
		MenuItem combatLogMenu = GameObject.FindWithTag("CombatLog").GetComponent<MenuItem>();
		combatLogMenu.text = GetString();
		combatLogMenu.scrollPosition = new Vector2(0, PER_LINE * NUM_LINES);
	}
	
	void Start() {
		messages = new List<string>();
		for(int i=0; i<NUM_LINES; i++) {
			messages.Add("");
		}
		MenuItem combatLogMenu = GameObject.FindWithTag("CombatLog").GetComponent<MenuItem>();
		combatLogMenu.scrollBoxHeight = (int)(NUM_LINES * PER_LINE);
		combatLogMenu.scrollPosition = new Vector2(0, PER_LINE * NUM_LINES);
	}
	
	private static string GetString() {
		return string.Join("\n", messages.ToArray());
	}
}
