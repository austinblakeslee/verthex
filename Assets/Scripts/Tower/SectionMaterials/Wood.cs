using UnityEngine;
using System.Collections;

public class Wood : SectionMaterial {
    public Wood () {
        this.initialSP = 100;
        this.maxSP = 300;
        this.cost = 50;
        this.weight = 0.01;
        this.SPPerRepair = 100;
        this.costPerRepair = 50;
        this.mtype = "Wood";
    }
}
