using UnityEngine;
using System.Collections;

public class Spirit2 : SectionWeapon {
    public Spirit2 () {
        this.damage = 70;
        this.spcost = 200;
        this.cost = 300;
        this.weight = 80;
        this.range = 3;
        this.wtype = "Spirit 2";
		this.range = 2;
		this.maxUpgradeEffect = new PoisonSplash();

    }
}
