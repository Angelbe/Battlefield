using UnityEngine;

public class CreatureSpawner
{
    private readonly BattlefieldController bfController;
    private readonly CreatureCatalog creatureCatalog;
    private readonly Transform attackerParent;
    private readonly Transform defenderParent;

    public CreatureSpawner(
        BattlefieldController controller,
        CreatureCatalog catalog,
        Transform attackerUnitsParent,
        Transform defenderUnitsParent)
    {
        bfController = controller;
        creatureCatalog = catalog;
        attackerParent = attackerUnitsParent;
        defenderParent = defenderUnitsParent;
    }

    public CreatureController SpawnCreature(CreatureStack stack, TileController tile, Army army)
    {
        bool isDefender = army.IsDefender;
        CreatureModel model = stack.Creature;
        int quantity = stack.Quantity;

        GameObject prefab = creatureCatalog.GetCombatPrefab(model.Name);
        Transform parent = isDefender ? defenderParent : attackerParent;
        GameObject creatureGO = GameObject.Instantiate(prefab, parent);

        creatureGO.transform.position = tile.Model.WorldPosition;

        CreatureController controller = creatureGO.GetComponent<CreatureController>();
        controller.Init(model, bfController, army, stack.ID, tile, quantity, isDefender);

        tile.SetOcupantCreature(controller);
        army.Deployed.AddStackToDeploy(stack, controller);

        return controller;
    }
}
