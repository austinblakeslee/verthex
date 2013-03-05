using UnityEngine;
using System.Collections;

public class Pistols : SectionWeapon {
    public Pistols() {
        this.damage = 50;
        this.spcost = 50;
        this.cost = 125;
        this.weight = 50;
        this.wtype = "Pistols";
    	this.range = 2;
		this.maxUpgradeEffect = new Burn();  
	}
}