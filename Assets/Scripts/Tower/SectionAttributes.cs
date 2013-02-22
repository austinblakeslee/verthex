using UnityEngine;
using System.Collections;

public class SectionAttributes {
    public SectionMaterial material;
    public SectionWeapon weapon;
    public int sp;
    public int maxSP;
    public int height;
	public Tower myTower;

    public SectionAttributes(SectionMaterial newMat, SectionWeapon newWeapon){
        this.material = newMat;
        this.weapon = newWeapon;
        this.sp = this.material.GetInitialSP();
        this.maxSP = this.material.GetMaxSP() - this.weapon.GetSPCost();
        this.height = 0;
    }

    public int GetWeight() {
        return (int)(this.material.GetWeightPerSP() * this.sp) + this.weapon.GetWeight();
    }
	public bool HasWeapon()
	{
		if (weapon.wtype == "Nothing")
			return false;
		return true;
	}

    public int GetCost() {
        return this.material.GetCost() + this.weapon.GetCost();
    }

    public void Repair() {
        this.sp += this.material.GetSPPerRepair();
        if(this.sp > this.maxSP) {
            this.sp = this.maxSP;
        }
    }
    
    public bool IsOverloaded(int stress) {
    	return stress > sp;
    }
}
