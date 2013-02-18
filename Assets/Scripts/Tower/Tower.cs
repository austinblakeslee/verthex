using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tower {
    private List<GameObject> sections;
	private DotManager dotManager;

    public Tower() {
        sections = new List<GameObject>();
		dotManager = new DotManager();
    }
	
	public int GetHeight() {
		return sections.Count;
	}
	
	public void AdvanceDots() {
		this.dotManager.NextTurn();
	}
	
	public Dot GetDot(SectionController sc) {
		return dotManager.GetDot(sc);
	}

    public List<GameObject> GetSections() {
        return this.sections;
    }

    public Section GetSection(int index) {
    	SectionController c = (SectionController)this.sections[index].GetComponent("SectionController");
        return c.GetSection();
    }
    
    public GameObject GetTopSection() {
    	if(sections.Count == 0) {
    		return null;
    	} else {
			return sections[sections.Count-1];
		}
    }

//returns the cost of the newly added section
    public void AddSection(GameObject s) {
        this.sections.Add(s);
        this.sections[this.sections.Count-1].GetComponent<SectionController>().SetHeight(this.sections.Count);
        StressCheck();
    }

    public int RepairSection(int i) {
		SectionController sc = sections[i].GetComponent<SectionController>();
		Section s = sc.GetSection();
		if(sc.HasDot()) {
			dotManager.RemoveDot(sc);
		} else {
			s.Repair();
		}
        return s.GetCostPerRepair();
    }

    public void RetrofitSection(int i, GameObject section) {
    	SectionController c = section.GetComponent<SectionController>();
    	c.SetHeight(i+1);
    	this.sections[i] = section;
    }

    public void DamageSection(int i, int damage) {
		if (i < sections.Count)	{
	        GetSection (i).GetMaterial().GetSectionEffect().ApplyDamage(GetSection(i), damage);//poor workflow... fix later...
			//GetSection(i).SubtractSP(damage);
	        StressCheck();
		}
    }
	
	public void ApplyDot(int i, int dotDamage) {
		if(i < sections.Count) {
			SectionController sc = sections[i].GetComponent<SectionController>();
			sc.DotApplied();
			dotManager.RegisterDot(sc, dotDamage);
		}
	}

    public int GetTotalWeight() {
        return SumWeight(0);
    }

    public int GetWeightAboveSection(int index) {
        return SumWeight(index+1);
    }

    public int SumWeight(int index) {
    	int weight = 0;
    	for(int i=index; i < sections.Count; i++) {
    		weight += GetSection(i).GetWeight();
    	}
    	return weight;
    }
    
    public SectionController StressCheck() {
    	for(int i=0; i < sections.Count; i++) {
    		int stress = GetWeightAboveSection(i);
    		if(GetSection(i).IsOverloaded(stress)) {
    			return sections[i].GetComponent<SectionController>();
    		}
    	}
		return null;
    }
    
    public void Collapse(int index) {
    	GameObject o = sections[index];
    	sections.RemoveAt(index);
    	GameObject.Destroy(o);
		CombatLog.addLine("Section " + (index+1) + " destroyed!!!");
    	for(int i=index; i < sections.Count; i++) {
    		sections[i].GetComponent<SectionController>().SetHeight(i+1);
    	}
    }
}
