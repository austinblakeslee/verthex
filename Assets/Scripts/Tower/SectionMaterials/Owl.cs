using UnityEngine;
using System.Collections;

public class Owl : SectionMaterial {
    public Owl () {
        this.initialSP = 150;
        this.maxSP = 300;
        this.cost = 50;
        this.weight = 15;//.02
        this.SPPerRepair = 100;
        this.costPerRepair = 50;
        this.mtype = "Owl";
    }
}
