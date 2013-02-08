using UnityEngine;
using System.Collections;

public class Player {
	public int playerNumber;
	private Tower tower;
	private int resources;
	public int number;
	public TowerBase towerBase;
	public Color color;
	public Faction faction;

	public Player(int number, Color c, Tower newTower, int r, Faction f) {
		this.color = c;
		this.playerNumber = number;
	    this.tower = newTower;
	    this.resources = r;
		this.faction = f;
	}

	public Tower GetTower() {
	    return this.tower;
	}
	
	public void SetTowerLocation(GameObject towerBase, GameObject other) {
		this.towerBase = towerBase.GetComponent<TowerBase>();
		this.towerBase.SetColor(color);
		Vector3 positionToLookAt = other.transform.position;
		positionToLookAt.y = towerBase.transform.position.y;
		towerBase.transform.LookAt(positionToLookAt);
	}
	
	public void NextTurn() {
		this.tower.AdvanceDots();
	}
	
	public void AccrueResources() {
		int towerEarnings = GameValues.intValues["resourcesPerSection"] * tower.GetHeight();
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

	public void Build(GameObject section) {
		SectionController c = section.GetComponent<SectionController>();
		Section s = c.GetSection();
		this.tower.AddSection(section);
		this.resources -= s.GetCost();
	}        

	public void RepairSection(int sectionIndex) {
	    int cost = this.tower.RepairSection(sectionIndex);
	    this.resources -= cost;
	}

	public void Retrofit(int index, GameObject section) {
		this.tower.RetrofitSection(index, section);
	}

	public void TakeDamage(int sectionIndex, int damage) {
	    this.tower.DamageSection(sectionIndex, damage);
	}
}
