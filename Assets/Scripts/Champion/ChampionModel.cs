public class ChampionModel
{
    public string Name;
    public EDeploymentLevel DeploymentLevel;

    public ChampionModel(string newName, EDeploymentLevel newDeploymentLevel = EDeploymentLevel.Master)
    {
        Name = newName;
        DeploymentLevel = newDeploymentLevel;
    }
}