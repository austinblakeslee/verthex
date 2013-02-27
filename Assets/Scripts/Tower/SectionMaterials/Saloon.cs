using UnityEngine;
using System.Collections;

public class Saloon : SectionMaterial {
    public Saloon () {
        this.initialSP = 275;
        this.maxSP = 550;
        this.cost = 150;
        this.weightPerSP = 0.05;
        this.SPPerRepair = 75;
        this.costPerRepair = 75;
        this.mtype = "Saloon";
    }
}
