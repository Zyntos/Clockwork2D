using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Mastery/Create Mastery")]
public class Mastery : ScriptableObject
{
    public string Description;
    public Sprite Icon;
    public string AffectedMastery;
    

    

    public void GetSkill(CharController player)
    {
       if(AffectedMastery == "DoubleJump")
        {
            player.doublejumpEnabled = true;
        }
       if(AffectedMastery == "GearHeal")
        {
            player.gearheal = true;
        }
    }

    public void LoseSkill(CharController player)
    {
        if(AffectedMastery == "DoubleJump")
        {
            player.doublejumpEnabled = false;
            player.secondjump = false;
        }
        if (AffectedMastery == "GearHeal")
        {
            player.gearheal = false;
        }
    }

}