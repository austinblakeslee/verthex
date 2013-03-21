using UnityEngine;
using System.Collections;

public class Spirit1 : SectionWeapon {
    public Spirit1 () {
        this.damage = 100;
        this.spcost = 50;
        this.cost = 200;
        this.weight = 30;
        this.range = 2;
		this.range = 2;
        this.wtype = "Spirit 1";
		this.maxUpgradeEffect = new PoisonSplash(this);
    }
}
