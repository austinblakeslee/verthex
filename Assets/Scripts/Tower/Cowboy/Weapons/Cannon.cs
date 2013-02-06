using UnityEngine;
using System.Collections;

public class Cannon : SectionWeapon {
    public Cannon () {
        this.damage = 100;
        this.spcost = 200;
        this.cost = 400;
        this.weight = 80;
        this.range = 3;
        this.wtype = "Cannon";
		this.range = 1;
		this.upgradeEffect = new DamageOverTime();
    }
	public Cannon(GameObject _model):this(){
		model = _model;
	}
}
