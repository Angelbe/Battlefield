using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


using System;

[Serializable]
public class DeploymentZone
{
    public string level;
    public bool attacker;
    public CubeCoordDTO[] tiles;
}
[Serializable]
public class DeploymentZonesWrapper
{
    public DeploymentZone[] deploymentZones;
}
