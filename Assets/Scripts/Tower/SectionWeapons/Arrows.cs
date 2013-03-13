using UnityEngine;
using System.Collections;

public class Arrows : SectionWeapon {
    public Arrows() {
        this.damage = 30;
        this.spcost = 50;
        this.cost = 75;
        this.weight = 50;
        this.range = 1; //what exactly is range?
        this.wtype = "Arrows";
    	this.range = 3;
		this.maxUpgradeEffect = new PoisonSplash(this);
		this.weaponEffect = new PoisonSplash(this);
	}
}
