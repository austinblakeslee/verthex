using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlindedEffect : SectionEffect
{
	public int missPercentage = 50;
	public int numTurns = 3;
	
	public BlindedEffect(Section effectedSection, int blindPercentage) : base(effectedSection)
	{
		this.effectType = "Blinded";
		missPercentage = blindPercentage;

	}
		
	public override void ApplyDamage(Section s, int power)
	{
		int ranNum1 = Random.Range(1, Mathf.RoundToInt(100/missPercentage));
		bool missedCompletely = false;
		if (ranNum1 == 1)//If missed, miss
		{
			int ranNum2 = Random.Range(1, 2);
			if (ranNum2 == 1)//miss high
			{
				CombatLog.addLine("Miss Higher (blind)");
				if (s.attributes.height + 1 > s.attributes.myTower.GetHeight())//Missed above tower
				{
					//Above tower
					missedCompletely = true;
				}
				else{
					s = s.attributes.myTower.GetSection(s.attributes.height + 1);
				}
			}
			else//miss low
			{
				CombatLog.addLine("Miss Lower (blind)");
				if (s.attributes.height - 1 < 0) //Missed below tower
				{
					missedCompletely = true;
				}
				else{
					s = s.attributes.myTower.GetSection(s.attributes.height - 1);
				}
			}
		}
		else
		{
			CombatLog.addLine ("Didn't Miss (blind)");
		}
		if (!missedCompletely)
			base.ApplyDamage(s, power);
	}
	
	public override void EndTurnEffect(){
		if (numTurns <= 0)
		{
			appliedSection.attributes.material.SetSectionEffect(new DefaultSectionEffect());
		}
		numTurns --;
		Debug.Log("Num turns Blinded left: " + numTurns);

	}
	public override string GetInfo ()
	{
		return "Blinded (" + missPercentage + "%) for " + (numTurns+1) + " more turns.";
	}
}

