using UnityEngine;
using System.Collections;

public class DisintegrationBeam : SectionWeapon {
    public DisintegrationBeam
     () {
        this.damage = 60;
        this.spcost = 50;
        this.cost = 250;
        this.weight = 30;
        this.range = 2;
		this.range = 2;
        this.wtype = "Disintegration Beam";
    }
	public DisintegrationBeam(GameObject _model):this(){
		model = _model;
	}
}
