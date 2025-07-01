using System.Collections.Generic;
using System.Drawing;

public class Army
{
    public string Name;                      // “Attacker”, “Defender” o IA vs jugador
    public bool IsAttacker;                  // ayuda a calcular la zona
    public Color ArmyColor;                  //Color del ejercito
    public EDeploymentLevel DeploymentLevel; // Basic / Advanced / …

    public List<CreatureModel> Reserve = new(); // criaturas sin desplegar
    public List<CreatureModel> Deployed = new(); // criaturas ya puestas

    // Helper para devolver la zona que puede usar este ejército
    public IEnumerable<CubeCoord> GetDeploymentZone()
        => DeploymentZone.GetZone(IsAttacker, DeploymentLevel);

    public void addCreatureToArmy(CreatureModel newCreatureToAdd)
    {
        Reserve.Add(newCreatureToAdd);
    }
}
