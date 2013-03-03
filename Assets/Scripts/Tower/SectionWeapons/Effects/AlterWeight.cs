using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AlterWeight : WeaponEffect
{
	protected List<Section> taggedSections = new List<Section>();
	protected int weightPercentageModifier = 20;//Percentage of weight shift
	protected int numTimesModifiable = 2;
	protected Dictionary<Section, int> modifiedSecs;
			
	public AlterWeight() : base() {
		this.effectType = "Weight Modification";
		canAttackSelf = true;
		modifiedSecs = new Dictionary<Section, int>();
	}
	public override List<Section> GetDamagedSections(Tower t, int center) {
		List<Section> secs = new List<Section>();
		secs.Add(t.GetSection(center));
		return secs;
	}
		
	public override void DoDamage(Tower t, int center, int damage, Tower self, int firingSection) {
		if(t.GetSections().Count >= 1) {		
			//t.DamageSection(center, damage);
			Section s = t.GetSection (center);
			SectionEffect eff = s.attributes.material.GetSectionEffect();
			eff.ApplyDamage(s, damage);
			if (!modifiedSecs.ContainsKey(s)) //if not modified before...
			{
				ModifyWeight(s, t, damage);
				modifiedSecs.Add(s, 1);

			}
			else if (modifiedSecs[s] >= numTimesModifiable)
			{
				ModifyWeight(s, t, damage);
				modifiedSecs[s]++;
			}
			else
			{
				//TODO: Make this a warning and let them go back to their turn
				CombatLog.addLine("This section has already altered the weight " + modifiedSecs[s] + "times.");
			}
		}
		else if(center < 0) {
			CombatLog.addLine("Attack was too low");
			CombatLog.addLine("Fill the aim bar more.");
		} else if(center >= t.GetSections().Count) {
			CombatLog.addLine("Attack was too high");
			CombatLog.addLine("Lower the aim bar.");
		}
	}
	
	public void ModifyWeight(Section s, Tower t, int damage)
	{
		double weightPerSP = s.attributes.material.GetWeightPerSP();

		if (TurnOrder.myPlayer.GetTower(t.towerNum) == t) //If you're attacking your own tower
		{
			s.attributes.material.weightPerSP -= weightPerSP*weightPercentageModifier/100;
			CombatLog.addLine("own " + s.attributes.myTower.faction + " section from " + weightPerSP + " to " + s.attributes.material.weightPerSP);


		}
		else
		{
			s.attributes.material.weightPerSP += weightPerSP*weightPercentageModifier/100;
			s.attributes.material.GetSectionEffect().ApplyDamage(s, damage);
			CombatLog.addLine("opponent's " + s.attributes.myTower.faction + " section from " + weightPerSP + " to " + s.attributes.material.weightPerSP);
		}	
	}
	
	public override string GetInfo(int damage)
	{
		return "Deals " + damage + " single target damage, and tags the target section for bonus damage on the next attack.";

	}
}