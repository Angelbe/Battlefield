using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattlefieldDeploymentZones
{
    private BattlefieldConfig bfConfig;
    private BattlefieldController bfController;
    private Dictionary<EDeploymentLevel, List<CubeCoord>> _deploymentDict;
    public IReadOnlyDictionary<EDeploymentLevel, List<CubeCoord>> DeploymentDict => _deploymentDict;

    private Dictionary<EDeploymentLevel, List<CubeCoord>> attackerZones = new();
    private Dictionary<EDeploymentLevel, List<CubeCoord>> defenderZones = new();

    public IReadOnlyDictionary<EDeploymentLevel, List<CubeCoord>> AttackerZones => attackerZones;
    public IReadOnlyDictionary<EDeploymentLevel, List<CubeCoord>> DefenderZones => defenderZones;

    public BattlefieldDeploymentZones(BattlefieldController newBfController, BattlefieldConfig newBfConfig)
    {
        bfController = newBfController;
        bfConfig = newBfConfig;
        LoadDeploymentZones();
    }

    public void LoadDeploymentZones()
    {
        attackerZones = new();
        defenderZones = new();

        TextAsset bfDeploymentZibesJson = bfConfig.deploymentZonesJson;
        if (bfDeploymentZibesJson == null || string.IsNullOrWhiteSpace(bfDeploymentZibesJson.text)) return;

        var wrapper = JsonUtility.FromJson<DeploymentZonesWrapper>(bfDeploymentZibesJson.text);
        if (wrapper == null || wrapper.deploymentZones == null) return;

        foreach (var zone in wrapper.deploymentZones)
        {
            if (!Enum.TryParse<EDeploymentLevel>(zone.level, out var parsedLevel))
            {
                Debug.LogWarning($"[DeployZone] Nivel desconocido: {zone.level}");
                continue;
            }

            var dict = zone.attacker ? attackerZones : defenderZones;

            if (!dict.TryGetValue(parsedLevel, out var list))
            {
                list = new List<CubeCoord>();
                dict[parsedLevel] = list;
            }

            foreach (var dto in zone.tiles)
                list.Add(dto.ToModel());
        }
    }

}