using UnityEngine;
using System.Collections;

public class WaterTower : SectionMaterial {
    public WaterTower () {
        this.initialSP = 150;
        this.maxSP = 400;
        this.cost = 50;
        this.weightPerSP = 0.03;
        this.SPPerRepair = 100;
        this.costPerRepair = 50;
        this.mtype = "Water Tower";
    }
}
