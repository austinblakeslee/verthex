using UnityEngine;
using System.Collections;

public class Cannon : SectionWeapon {
    public Cannon () {
        this.damage = 80;
        this.spcost = 200;
        this.cost = 400;
        this.weight = 80;
        this.range = 3;
        this.wtype = "Cannon";
    }
}
