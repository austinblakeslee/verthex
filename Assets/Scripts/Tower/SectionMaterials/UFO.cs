using UnityEngine;
using System.Collections;

public class UFO : SectionMaterial {
    public UFO () {
        this.initialSP = 300;
        this.maxSP = 600;
        this.cost = 200;
        this.weightPerSP = 0.1;
        this.SPPerRepair = 50;
        this.costPerRepair = 75;
        this.mtype = "UFO";
    }
}
