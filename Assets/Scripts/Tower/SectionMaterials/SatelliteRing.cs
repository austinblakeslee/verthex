using UnityEngine;
using System.Collections;

public class SatelliteRing : SectionMaterial {
    public SatelliteRing () {
        this.initialSP = 200;
        this.maxSP = 300;
        this.cost = 75;
        this.weight = 16;
        this.SPPerRepair = 100;
        this.costPerRepair = 50;
        this.mtype = "Satellite Ring";
    }
}
