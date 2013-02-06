using UnityEngine;
using System.Collections;

public class Nothing : SectionWeapon {
    public Nothing () {
        this.damage = 0;
        this.spcost = 0;
        this.cost = 0;
        this.weight = 0;
        this.range = 0;
		this.range = 0;
        this.wtype = "Nothing";
    }
	public Nothing(GameObject _model):this(){
		model = _model;
	}
}

