using UnityEngine;
using System.Collections;

public class Stone : SectionMaterial {
    public Stone () {
        this.initialSP = 200;
        this.maxSP = 400;
        this.cost = 150;
        this.weight = 0.05;
        this.SPPerRepair = 75;
        this.costPerRepair = 75;
        this.mtype = "Stone";
    }
}
