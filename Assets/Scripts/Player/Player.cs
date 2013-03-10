using UnityEngine;
using System.Collections;

public class Player {
	public int playerNumber;
	private Tower[] towers;
	private int resources;
	public int number;
	public Color color;

	public Player(int number, Color c, int r) {
		this.color = c;
		this.playerNumber = number;
	    this.resources = r;
		this.towers = new Tower[3];
		Debug.Log(towers.Length);
	}
	
	public void AddTower(TowerBase b, Faction f, int i) {
		this.towers[i] = new Tower(b, f, i);
	}

	public Tower GetTower(int i) {
	    return this.towers[i];
	}
	
	public Tower[] GetTowers() {
		return towers;
	}
	
	public void NextTurn() {
		foreach(Tower t in towers) {
			t.AdvanceDots();
		}
	}
	
	public void AccrueResources() {
		int towerEarnings = GameValues.intValues["resourcesPerTurn"];
		foreach(Tower t in towers) {
			towerEarnings += t.GetHeight() * GameValues.intValues["resourcesPerSection"];
		}
		AddResources(towerEarnings);
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
	}        

	public void RepairSection(int sectionIndex, int i) {
	    this.towers[i].RepairSection(sectionIndex);
	}

	public void TakeDamage(int sectionIndex, int damage, int i) {
	    this.towers[i].DamageSection(sectionIndex, damage);
	}
	
	public bool Loses() {
		bool lost = false;
		int loseCount = 0;
		foreach(Tower t in towers) {
			t.CheckAlive();
			if(!t.alive) {
				loseCount++;
			}	
		}
		
		if(loseCount == towers.Length){
			lost = true;
		}

		return lost;
	}
}
