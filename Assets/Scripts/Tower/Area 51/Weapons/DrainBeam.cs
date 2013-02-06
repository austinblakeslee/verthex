using UnityEngine;
using System.Collections;

public class DrainBeam : SectionWeapon {
    public DrainBeam () {
        this.damage = 75;
        this.spcost = 200;
        this.cost = 400;
        this.weight = 80;
        this.range = 3;
        this.wtype = "Drain Beam";
		this.range = 1;
    }
	public DrainBeam(GameObject _model):this(){
		model = _model;
	}
}
