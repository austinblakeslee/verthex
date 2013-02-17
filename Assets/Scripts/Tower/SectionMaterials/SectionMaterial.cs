using UnityEngine;
using System.Collections;

public class SectionMaterial {
	protected int initialSP;
	protected int maxSP;
	public int cost;
	protected double weightPerSP;
	protected int SPPerRepair;
	protected int costPerRepair;
	protected SectionEffect sectionEffect = new DefaultSectionEffect();//will this give the sub-material as the param?
	public string mtype = "abstract";
	
	public static string[] materials = new string[3] {"Wood", "Stone", "Steel"};
	
	public virtual GameObject GetPrefab() {
		return Resources.Load(mtype) as GameObject;
	}

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
	public void SetSectionEffect(SectionEffect secEffect)
	{
		sectionEffect = secEffect;
	}
	public SectionEffect GetSectionEffect()
	{
		return sectionEffect;
	}
}
