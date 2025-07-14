using System;

public class DeploySlotModel
{
    public CreatureStack StackInTheSlot { get; private set; }

    public DeploySlotModel(CreatureStack creatureStackToShow)
    {
        StackInTheSlot = creatureStackToShow;
    }
}