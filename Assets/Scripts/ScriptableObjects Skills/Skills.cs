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
	
}
