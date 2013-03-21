using UnityEngine;
using System.Collections;

public class WaterTower : SectionMaterial {
    public WaterTower () {
        this.initialSP = 150;
        this.maxSP = 400;
        this.cost = 50;
        this.weight = 20;//0.03;
        this.SPPerRepair = 100;
        this.costPerRepair = 50;
        this.mtype = "Water Tower";
		this.texture = GameValues.textures["waterTower"];

    }
}
