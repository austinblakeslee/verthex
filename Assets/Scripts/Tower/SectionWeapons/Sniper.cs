using UnityEngine;
using System.Collections;

public class Sniper : SectionWeapon {
    public Sniper () {
        this.damage = 110;
        this.spcost = 50;
        this.cost = 800;
        this.weight = 30;
		this.range = 2;
        this.wtype = "Sniper";
		this.range = 7;
		this.maxUpgradeEffect = new DamageOverTime();
    }
}
