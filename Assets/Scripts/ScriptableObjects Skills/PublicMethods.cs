using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerAttributes
{
    public Attributes attribute;
    public int amount;

    public PlayerAttributes(Attributes attribute, int amount)
    {
        this.attribute = attribute;
        this.amount = amount;
    }
}

[System.Serializable]
public class PlayerMasterys
{
    public MasteryEnabler mastery;
    public int amount;

    public PlayerMasterys(MasteryEnabler mastery, int amount)
    {
        this.mastery = mastery;
        this.amount = amount;
    }
}
