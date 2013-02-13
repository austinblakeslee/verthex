using UnityEngine;
using System.Collections;

public class Blaster : SectionWeapon {
    public Blaster() {
        this.damage = 40;
        this.spcost = 50;
        this.cost = 100;
        this.weight = 50;
        this.range = 1; 
        this.wtype = "Blaster";
    	this.range = 3;
		this.maxUpgradeEffect = new AreaOfEffect();
	}
}
