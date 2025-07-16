public interface ICreatureController
{
    public CreatureView View { get; }
    public CreatureModel Model { get; }
    public void addQuantity(int newCreaturesToAdd);
    public void removeQuantity(int newCreaturesToAdd);
}