using UnityEngine;
using System.Collections;

public class Wood : SectionMaterial {
    public Wood () {
        this.initialSP = 100;
        this.maxSP = 300;
        this.cost = 50;
        this.weightPerSP = 0.01;
        this.SPPerRepair = 100;
        this.costPerRepair = 50;
        this.mtype = "Wood";
    }
	public Wood(GameObject _model):this(){
		model = _model;
	}
}
