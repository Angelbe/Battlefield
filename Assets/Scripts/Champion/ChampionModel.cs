public class ChampionModel
{
    public string Name;
    public EDeploymentLevel DeploymentLevel;

    public ChampionModel(string newName, EDeploymentLevel newDeploymentLevel = EDeploymentLevel.Basic)
    {
        Name = newName;
        DeploymentLevel = newDeploymentLevel;
    }
}