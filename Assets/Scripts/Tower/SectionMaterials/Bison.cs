using UnityEngine;
using System.Collections;

public class Bison : SectionMaterial {
    public Bison () {
        this.initialSP = 350;
        this.maxSP = 600;
        this.cost = 250;
        this.weight = 35; //.1
        this.SPPerRepair = 50;
        this.costPerRepair = 150;
        this.mtype = "Bison";
		//this.sectionEffect = new Burned(20);
		this.texture = GameValues.textures["bison"];
    }
}
