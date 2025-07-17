using System.Collections.Generic;
using UnityEngine;

public class BattlefieldHighlightHandler
{
    private readonly Dictionary<CubeCoord, TileController> tileControllers;
    private BattlefieldDeploymentZones deploymentZones;
    private BattlefieldConfig bfConfig;
    private BattlefieldModel bfModel;

    public BattlefieldHighlightHandler(Dictionary<CubeCoord, TileController> tileControllersFromBattlefield, BattlefieldDeploymentZones zones, BattlefieldConfig battlefieldConfig, BattlefieldModel model)
    {
        tileControllers = tileControllersFromBattlefield;
        deploymentZones = zones;
        bfConfig = battlefieldConfig;
        bfModel = model;
    }

    public void AddColorToLevel(int level, CubeCoord coord, Color color)
    {
        if (tileControllers.TryGetValue(coord, out var tile))
            tile.Highlight.AddColor(level, color);
    }

    public void RemoveColorFromLevel(int level, CubeCoord coord, Color color)
    {
        if (tileControllers.TryGetValue(coord, out var tile))
            tile.Highlight.RemoveColor(level, color);
    }

    public void ClearLevel(int level, CubeCoord coord)
    {
        if (tileControllers.TryGetValue(coord, out var tile))
            tile.Highlight.ClearLevel(level);
    }

    public void ClearLevelForAll(int level)
    {
        foreach (var tile in tileControllers.Values)
            tile.Highlight.ClearLevel(level);
    }

    public void ClearAll()
    {
        foreach (var tile in tileControllers.Values)
            tile.Highlight.ClearAll();
    }

    public void AddColorToLevel(int level, IEnumerable<CubeCoord> coords, Color color)
    {
        foreach (var coord in coords)
            AddColorToLevel(level, coord, color);
    }

    public void RemoveColorFromLevel(int level, IEnumerable<CubeCoord> coords, Color color)
    {
        foreach (var coord in coords)
            RemoveColorFromLevel(level, coord, color);
    }

    public void ClearLevel(int level, IEnumerable<CubeCoord> coords)
    {
        foreach (var coord in coords)
            ClearLevel(level, coord);
    }
    public void ShowAttackerDeploymentZone(EDeploymentLevel level)
    {
        if (!deploymentZones.AttackerZones.TryGetValue(level, out var coords)) return;
        var color = bfConfig.GetColor(ETileHighlightType.DeployZone);
        AddColorToLevel(2, coords, color);
    }

    public void ShowDefenderDeploymentZone(EDeploymentLevel level)
    {
        if (!deploymentZones.DefenderZones.TryGetValue(level, out var coords)) return;
        var color = bfConfig.GetColor(ETileHighlightType.DeployZone);
        AddColorToLevel(2, coords, color);
    }
    public void ClearDeploymentZones()
    {
        Color deployColor = bfConfig.GetColor(ETileHighlightType.DeployZone);

        foreach (var tile in tileControllers.Values)
        {
            tile.Highlight.RemoveColor(2, deployColor);
        }
    }

}
