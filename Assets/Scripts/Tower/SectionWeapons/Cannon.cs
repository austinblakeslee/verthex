using UnityEngine;
using System.Collections;

public class Cannon : SectionWeapon {
    public Cannon () {
        this.damage = 90;
        this.spcost = 200;
        this.cost = 400;
        this.weight = 80;
        this.wtype = "Cannon";
		this.range = 1;
		this.maxUpgradeEffect = new Burn();
    }
}
