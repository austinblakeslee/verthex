using UnityEngine;
using System.Collections;

public class Arrows : SectionWeapon {
    public Arrows() {
        this.damage = 40;
        this.spcost = 50;
        this.cost = 100;
        this.weight = 50;
        this.range = 1; //what exactly is range?
        this.wtype = "Arrows";
    	this.range = 3;
	}
}
