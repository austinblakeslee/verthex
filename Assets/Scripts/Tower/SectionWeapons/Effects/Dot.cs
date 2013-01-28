using UnityEngine;
using System.Collections;

public class Dot  {

	public int turnsRemaining;
	public int damagePerTurn;
	
	public Dot(int turns, int damagePerTurn) {
		this.turnsRemaining = turns;
		this.damagePerTurn = damagePerTurn;
	}
	
	public int Advance() {
		turnsRemaining--;
		return damagePerTurn;
	}
	
	public bool Done() {
		return turnsRemaining <= 0;
	}
}
