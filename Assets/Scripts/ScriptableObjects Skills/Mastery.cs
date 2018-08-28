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
        if (AffectedMastery == "MoreGears")
        {
            player.moregears = true;
        }
        if (AffectedMastery == "focus")
        {
            player.focus = true;
        }
        if (AffectedMastery == "energy")
        {
            player.staffheal = true;
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
        if (AffectedMastery == "MoreGears")
        {
            player.moregears = false;
        }
        if (AffectedMastery == "focus")
        {
            player.moregears = false;
        }
        if (AffectedMastery == "energy")
        {
            player.staffheal = false;
        }
    }

}