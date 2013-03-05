using UnityEngine;
using System.Collections;

public class Coyote : SectionMaterial {
    public Coyote () {
        this.initialSP = 275;
        this.maxSP = 450;
        this.cost = 175;
        this.weight = 27; //.05
        this.SPPerRepair = 75;
        this.costPerRepair = 75;
        this.mtype = "Coyote";
		//this.sectionEffect = new ForceFieldEffect(80);
    }
}