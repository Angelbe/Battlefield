using System;

public class DeploySlotModel
{
    public CreatureStack CreatureStack { get; private set; }
    public bool IsDefender;

    public DeploySlotModel(CreatureStack creatureStackToShow, bool isDefender)
    {
        CreatureStack = creatureStackToShow;
        IsDefender = isDefender;
    }
}