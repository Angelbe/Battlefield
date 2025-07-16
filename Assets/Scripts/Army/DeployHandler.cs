using System;
using System.Collections.Generic;

public class DeployHandler
{
    private BattlefieldController bfController;
    public ReserveHandler ArmyReserve { get; private set; }
    public Dictionary<Guid, CreatureController> Deployed { get; private set; } = new();

    public DeployHandler(BattlefieldController newBFController, ReserveHandler newArmyReserve)
    {
        bfController = newBFController;
        ArmyReserve = newArmyReserve;
    }

    public void AddStackToDeploy(CreatureStack stack, CreatureController controller)
    {
        Deployed[stack.ID] = controller;
    }

    public bool TryGetController(Guid id, out CreatureController controller)
    {
        return Deployed.TryGetValue(id, out controller);
    }

}
