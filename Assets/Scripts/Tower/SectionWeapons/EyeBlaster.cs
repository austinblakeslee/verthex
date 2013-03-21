using UnityEngine;
using System.Collections;

public class EyeBlaster : SectionWeapon {
    public EyeBlaster () {
        this.damage = 160;
        this.spcost = 200;
        this.cost = 350;
        this.weight = 60;
        this.wtype = "Eye Blaster";
		this.range = 1;
    	this.maxUpgradeEffect = new AreaOfEffect(this);
	}
}
