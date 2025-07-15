using System;

public class DeploySlotModel
{
    public CreatureStack CreatureStack { get; private set; }

    public DeploySlotModel(CreatureStack creatureStackToShow)
    {
        CreatureStack = creatureStackToShow;
    }
}