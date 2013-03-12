using UnityEngine;
using System.Collections;

public class SectionMaterial {
	public int initialSP;
	public int maxSP;
	public int cost;
	public double weight;
	public int SPPerRepair;
	public int costPerRepair;
	public string mtype = "abstract";
	public SectionEffect sectionEffect = new DefaultSectionEffect(new Section());

	
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

	public double GetWeight() {
	    return weight;
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
