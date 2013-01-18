using UnityEngine;
using System.Collections;

public class Section {
    private SectionMaterial material;
    private SectionWeapon weapon;
    private int sp;
    private int maxSP;
    private int height;

    public Section(SectionMaterial newMat, SectionWeapon newWeapon){
        this.material = newMat;
        this.weapon = newWeapon;
        this.sp = this.material.GetInitialSP();
        this.maxSP = this.material.GetMaxSP() - this.weapon.GetSPCost();
        this.height = 0;
    }
    
    public int GetSP() {
    	return this.sp;
    }
	
	public int GetInitialSP() {
		return this.material.GetInitialSP();
	}
	
	public int GetMaxSP() {
    	return this.maxSP;
    }

    public SectionMaterial GetMaterial() {
        return this.material;
    }

    public SectionWeapon GetWeapon() {
        return this.weapon;
    }

    public void ChangeMaterial(SectionMaterial newMat) {
        material = newMat;
    }

    public void ChangeWeapon(SectionWeapon newWeapon) {
        weapon = newWeapon;
    }

    public int GetWeight() {
        return (int)(this.material.GetWeightPerSP() * this.sp) + this.weapon.GetWeight();
    }

    public int GetCost() {
        return this.material.GetCost() + this.weapon.GetCost();
    }

    public int GetCostPerRepair() {
        return this.material.GetCostPerRepair();
    }

    public void Repair() {
        this.sp += this.material.GetSPPerRepair();
        if(this.sp > this.maxSP) {
            this.sp = this.maxSP;
        }
    }

    public void SubtractSP(int i) {
        this.sp -= i;
    }

    public void AddSP(int i) {
        this.sp += i;
    }
    
    public void SetHeight(int newHeight) {
    	this.height = newHeight;
    }
    
    public int GetHeight() {
    	return this.height;
    }
    
    public bool IsOverloaded(int stress) {
    	return stress > sp;
    }
}
