using UnityEngine;
using System.Collections;

public class JailCell : SectionMaterial {
    public JailCell () {
        this.initialSP = 350;
        this.maxSP = 700;
        this.cost = 200;
        this.weight = 38;//0.1;
        this.SPPerRepair = 50;
        this.costPerRepair = 75;
        this.mtype = "Jail Cell";
    		this.texture = GameValues.textures["jailCell"];

	}
	
}
