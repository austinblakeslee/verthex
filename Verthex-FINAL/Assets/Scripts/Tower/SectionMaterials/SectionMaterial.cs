using UnityEngine;
using System.Collections;

public class SectionMaterial {
	protected int initialSP;
	protected int maxSP;
	public int cost;
	protected double weightPerSP;
	protected int SPPerRepair;
	protected int costPerRepair;
	public string mtype = "abstract";

	public int GetInitialSP() {
	    return initialSP;
	}

	public int GetMaxSP() {
	    return maxSP;
	}

	public int GetCost() {
	    return cost;
	}

	public double GetWeightPerSP() {
	    return weightPerSP;
	}

	public int GetSPPerRepair() {
	    return SPPerRepair;
	}

	public int GetCostPerRepair() {
	    return costPerRepair;
	}

	public string GetMaterialType() {
	    return mtype;
	}
}
