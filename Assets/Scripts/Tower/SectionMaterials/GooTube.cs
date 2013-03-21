using UnityEngine;
using System.Collections;

public class GooTube : SectionMaterial {
    public GooTube () {
        this.initialSP = 300;
        this.maxSP = 500;
        this.cost = 175;
        this.weight = 25;//0.05;
        this.SPPerRepair = 75;
        this.costPerRepair = 150;
        this.mtype = "Goo Tube";
 		this.texture = GameValues.textures["gooTube"];

	}
}
