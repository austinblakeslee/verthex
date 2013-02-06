using UnityEngine;
using System.Collections;

public class Pistols : SectionWeapon {
    public Pistols() {
        this.damage = 60;
        this.spcost = 50;
        this.cost = 100;
        this.weight = 50;
        this.range = 1;
        this.wtype = "Pistols";
    	this.range = 3;
		this.upgradeEffect = new DamageOverTime();   	
	}
	public Pistols(GameObject _model):this(){
		model = _model;
	}	
}
