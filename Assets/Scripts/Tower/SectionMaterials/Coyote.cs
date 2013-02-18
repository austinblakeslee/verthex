using UnityEngine;
using System.Collections;

public class Coyote : SectionMaterial {
    public Coyote () {
        this.initialSP = 200;
        this.maxSP = 400;
        this.cost = 150;
        this.weightPerSP = 0.05;
        this.SPPerRepair = 75;
        this.costPerRepair = 75;
        this.mtype = "Coyote";
		this.sectionEffect = new ForceFieldEffect(80);
    }
}