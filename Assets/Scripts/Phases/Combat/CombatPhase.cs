using System.Collections.Generic;
using UnityEngine;

public class CombatPhase : IBattlePhase
{
    private BattlefieldController bfController;
    private UICombatController uICombatController;
    public TurnHandler TurnHandler;
    public DeployHandler AttackerCreatures;
    public DeployHandler DefenderCreatures;
    public CreatureController ActiveCreature;
    public Army ActiveArmy;

    public CombatPhase(BattlefieldController newBFController, UIController newUIController)
    {
        bfController = newBFController;
        uICombatController = newUIController.UICombatController;
    }


    public void UpdateUITurnOrder()
    {
        var upcoming = TurnHandler.GetNextTurnOrder(15);
        uICombatController.ShowTurnOrder(upcoming);
    }

    public void SetNewActiveCreature(CreatureController newActiveCreature)
    {
        ActiveCreature = newActiveCreature;
        ActiveCreature.Movement.SetReachableTiles();
    }

    public void HandleCreatureFinishedTurn()
    {
        SetNewActiveCreature(TurnHandler.GetNextCreature());
        UpdateUITurnOrder();
    }

    private void HandleClickTile(TileController tileClicked)
    {
        CubeCoord destination = tileClicked.Model.Coord;

        if (!ActiveCreature.Movement.IsTileReachable(destination)) return;

        List<CubeCoord> path = ActiveCreature.Movement.Pathfinder.GetPath(ActiveCreature.Positions[0], destination);
        //Hay que limpiar las tiles de posible movimiento aquí antes de empezar a moverse
        //Hay que limpiar las tiles ocupadas antes de empezar a moverse
        ActiveCreature.Movement.MoveAlongPath(path);
    }


    public void StartPhase()
    {
        AttackerCreatures = bfController.bfModel.Attacker.Deployed;
        DefenderCreatures = bfController.bfModel.Defender.Deployed;
        TurnHandler = new(AttackerCreatures, DefenderCreatures);
        uICombatController.Init();
        SetNewActiveCreature(TurnHandler.PeekCurrentCreature());
        UpdateUITurnOrder();
        bfController.BfMouse.OnTileClickedCombatPhase += HandleClickTile;

    }

    public void ExitPhase()
    {
        uICombatController.Shutdown();
        bfController.BfMouse.OnTileClickedCombatPhase -= HandleClickTile;
    }
}