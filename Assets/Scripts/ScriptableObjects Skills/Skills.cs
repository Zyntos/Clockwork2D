using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Talents/Create Skill")]
public class Skills : ScriptableObject
{
    public string Description;
    public Sprite Icon;
    public int GearsNeeded;

    public List<PlayerAttributes> AffectedAttributes = new List<PlayerAttributes>();

    public void GetSkill(CharController player)
    {
        List<PlayerAttributes>.Enumerator attributes = AffectedAttributes.GetEnumerator();
        while (attributes.MoveNext())
        {
            List<PlayerAttributes>.Enumerator PlayerAttr = player.Attributes.GetEnumerator();
            while (PlayerAttr.MoveNext())
            {
                if(attributes.Current.attribute.name.ToString() == PlayerAttr.Current.attribute.name.ToString())
                {
                    PlayerAttr.Current.amount += attributes.Current.amount;
                }
            }
        }
    }
	
}
