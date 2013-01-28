using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DotManager {

	public DotManager() {
	
	}

	private Dictionary<SectionController, Dot> dotRegistry = new Dictionary<SectionController, Dot>();
	private int DOT_DURATION = 3;
	
	public void NextTurn() {
		List<SectionController> keysToRemove = new List<SectionController>();
		foreach(KeyValuePair<SectionController, Dot> pair in dotRegistry) {
			SectionController sc = pair.Key;
			Dot dot = pair.Value;
			int damage = dot.Advance();
			sc.GetPlayer().GetTower().DamageSection(sc.GetHeight()-1, damage);
			if(dot.Done()) {
				keysToRemove.Add(sc);
				sc.DotFinished();
			}
		}
		foreach(SectionController sc in keysToRemove) {
			dotRegistry.Remove(sc);
		}
	}
	
	public void RegisterDot(SectionController sc, int damage) {
		if(dotRegistry.ContainsKey(sc)) {
			dotRegistry.Remove(sc);
		}
		dotRegistry.Add(sc, new Dot(DOT_DURATION, damage/DOT_DURATION));
	}
	
	public void RemoveDot(SectionController sc) {
		if(dotRegistry.ContainsKey(sc)) {
			dotRegistry.Remove(sc);
			sc.DotFinished();
		}
	}
	
	public Dot GetDot(SectionController sc) {
		return dotRegistry[sc];
	}
}
