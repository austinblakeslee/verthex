using UnityEngine;
using System.Collections;

public class UFO : SectionMaterial {
    public UFO () {
        this.initialSP = 400;
        this.maxSP = 650;
        this.cost = 250;
        this.weight = 37;//.1
        this.SPPerRepair = 50;
        this.costPerRepair = 125;
        this.mtype = "UFO";
		
		this.texture = GameValues.textures["ufo"];

    }
}
