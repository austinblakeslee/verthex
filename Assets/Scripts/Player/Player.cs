using UnityEngine;
using System.Collections;

public class Player {
	public int playerNumber;
	private Tower[] towers;
	private int resources;
	public int number;
	public Color color;
	public Faction faction;

	public Player(int number, Color c, int r) {
		this.color = c;
		this.playerNumber = number;
	    this.resources = r;
		this.towers = new Tower[3];
	}
	
	public void AddTower(TowerBase b, Faction f, int i) {
		this.towers[i] = new Tower(b, f);
	}

	public Tower GetTower(int i) {
	    return this.towers[i];
	}
	
	public void NextTurn() {
		foreach(Tower t in towers) {
			t.AdvanceDots();
		}
	}
	
	public void AccrueResources() {
		Debug.LogWarning("ACCRUE RESOURCES NEEDS TO BE FIXED");
		int towerEarnings = GameValues.intValues["resourcesPerSection"];;
		AddResources(GameValues.intValues["resourcesPerTurn"] + towerEarnings);
	}
	
	public void AddResources(int add) {
		this.resources += add;
	}
	
	public void RemoveResources(int remove) {
		this.resources -= remove;
	}

	public int GetResources() {
	    return this.resources;
	}

	public void Build(Section section, Tower t) {
		t.AddSection(section);
		this.resources -= section.attributes.GetCost();
	}        

	public void RepairSection(int sectionIndex, int i) {
	    int cost = this.towers[i].RepairSection(sectionIndex);
	    this.resources -= cost;
	}

	public void TakeDamage(int sectionIndex, int damage, int i) {
	    this.towers[i].DamageSection(sectionIndex, damage);
	}
	
	public bool Loses() {
		foreach(Tower t in towers) {
			if(t.alive) {
				return false;
			}
		}
		return true;
	}
}
