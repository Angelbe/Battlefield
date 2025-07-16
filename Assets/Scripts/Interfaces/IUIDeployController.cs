using System;

public interface IUIDeployController
{
    public Army Attacker { get; }
    public Army Defender { get; }
    public event Action<DeploySlotController> OnSlotClicked;
}