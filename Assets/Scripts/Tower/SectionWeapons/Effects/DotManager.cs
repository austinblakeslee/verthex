using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DotManager {

	public DotManager() {
	
	}

	private Dictionary<Section, Dot> dotRegistry = new Dictionary<Section, Dot>();
	private int DOT_DURATION = 3;
	
	public void NextTurn() {
		List<Section> keysToRemove = new List<Section>();
		foreach(KeyValuePair<Section, Dot> pair in dotRegistry) {
			Section sc = pair.Key;
			Dot dot = pair.Value;
			int damage = dot.Advance();
			sc.attributes.sp -= damage;
			if(dot.Done()) {
				keysToRemove.Add(sc);
				sc.DotFinished();
			}
		}
		foreach(Section sc in keysToRemove) {
			dotRegistry.Remove(sc);
		}
	}
	
	public void RegisterDot(Section sc, int damage) {
		if(dotRegistry.ContainsKey(sc)) {
			dotRegistry.Remove(sc);
		}
		dotRegistry.Add(sc, new Dot(DOT_DURATION, damage/DOT_DURATION));
	}
	
	public void RemoveDot(Section sc) {
		if(dotRegistry.ContainsKey(sc)) {
			dotRegistry.Remove(sc);
			sc.DotFinished();
		}
	}
	
	public Dot GetDot(Section sc) {
		return dotRegistry[sc];
	}
}
