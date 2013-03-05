using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tower {
    private List<Section> sections;
	public TowerBase towerBase;
	public Faction faction;
	public bool alive = true;
	public int towerNum;
	private DotManager dotManager;

    public Tower(TowerBase towerBase, Faction faction, int _towerNum) {
        sections = new List<Section>();
		dotManager = new DotManager();
		this.towerBase = towerBase;
		this.faction = faction;
		towerNum = _towerNum;

    }
	
	public TowerBase GetTowerBase(){
		return this.towerBase;
	}
	
	public int GetHeight() {
		return sections.Count;
	}
	
	public void AdvanceDots() {
		this.dotManager.NextTurn();
	}
	
	public Dot GetDot(Section sc) {
		return dotManager.GetDot(sc);
	}

    public List<Section> GetSections() {
        return this.sections;
    }

    public Section GetSection(int index) {
        return sections[index];
    }
    
    public Section GetTopSection() {
    	if(sections.Count == 0) {
    		return null;
    	} else {
			return sections[sections.Count-1];
		}
    }
	public int GetPlayerNum()
	{
		return TurnOrder.myPlayer.GetTower(towerNum) == this ? TurnOrder.myPlayer.playerNumber : TurnOrder.otherPlayer.playerNumber;
	}

//returns the cost of the newly added section
    public void AddSection(Section s) {
        this.sections.Add(s);
        s.attributes.height = this.sections.Count-1;
		s.attributes.myTower = this;
        StressCheck();
    }

    public int RepairSection(int i) {
		Section s = sections[i];
		if(s.HasDot()) {
			dotManager.RemoveDot(s);
		} else {
			s.attributes.Repair();
		}
        return s.attributes.material.costPerRepair;
    }

    public void DamageSection(int i, int damage) {
		if (i < sections.Count)	{
	        GetSection(i).attributes.sp -= damage;
	        StressCheck();
		}
    }
	
	public void ApplyDot(int i, int dotDamage) {
		if(i < sections.Count) {
			Section s = sections[i];;
			s.DotApplied();
			dotManager.RegisterDot(s, dotDamage);
		}
	}

    public int GetTotalWeight() {
        return SumWeight(0);
    }

    public int GetWeightAboveSection(int index) {
        return SumWeight(index+1);
    }

    public int SumWeight(int index) {
    	int weight = 0;
    	for(int i=index; i < sections.Count; i++) {
    		weight += GetSection(i).attributes.GetWeight();
    	}
    	return weight;
    }
    
    public Section StressCheck() {
    	for(int i=0; i < sections.Count; i++) {
    		int stress = GetWeightAboveSection(i);
    		if(GetSection(i).attributes.IsOverloaded(stress)) {
    			return sections[i];;
    		}
    	}
		return null;
    }
    
    public void Collapse(int index) {
    	Section o = sections[index];
    	sections.RemoveAt(index);
    	GameObject.Destroy(o.gameObject);
		CombatLog.addLine("Section " + (index+1) + " destroyed!!!");
    	for(int i=index; i < sections.Count; i++) {
    		sections[i].attributes.height = i;
    	}
		if(sections.Count <= 0) {
			alive = false;
		}
    }
}
