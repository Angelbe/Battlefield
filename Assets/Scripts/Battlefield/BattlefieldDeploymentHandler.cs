using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


//TODO: Arreglar esto con deploymentController
public class BattlefieldDeploymentHandler
{
    private BattlefieldConfig bfConfig;
    private BattlefieldController bfController;
    private Dictionary<EDeploymentLevel, List<CubeCoord>> _deploymentDict;
    public IReadOnlyDictionary<EDeploymentLevel, List<CubeCoord>> DeploymentDict => _deploymentDict;

    private Dictionary<EDeploymentLevel, List<CubeCoord>> _attackerZones = new();
    private Dictionary<EDeploymentLevel, List<CubeCoord>> _defenderZones = new();

    public IReadOnlyDictionary<EDeploymentLevel, List<CubeCoord>> AttackerZones => _attackerZones;
    public IReadOnlyDictionary<EDeploymentLevel, List<CubeCoord>> DefenderZones => _defenderZones;

    public BattlefieldDeploymentHandler(BattlefieldController newBfController, BattlefieldConfig newBfConfig)
    {
        bfController = newBfController;
        bfConfig = newBfConfig;
    }

    private void DebugPrintZones(string label, Dictionary<EDeploymentLevel, List<CubeCoord>> zones)
    {
        Debug.Log($"── {label} ──");

        foreach (var pair in zones)
        {
            string level = pair.Key.ToString();
            string coords = string.Join(", ", pair.Value.Select(c => c.ToString()));
            Debug.Log($"Level: {level} → [{coords}]");
        }
    }


    public void LoadDeploymentZones()
    {
        _attackerZones = new();
        _defenderZones = new();

        TextAsset bfDeploymentZibesJson = bfConfig.deploymentZonesJson;
        if (bfDeploymentZibesJson == null || string.IsNullOrWhiteSpace(bfDeploymentZibesJson.text)) return;

        var wrapper = JsonUtility.FromJson<DeploymentZonesWrapper>(bfDeploymentZibesJson.text);
        if (wrapper == null || wrapper.deploymentZones == null) return;

        foreach (var zone in wrapper.deploymentZones)
        {
            // Elegir diccionario según sea atacante o defensor
            var dict = zone.attacker ? _attackerZones : _defenderZones;

            if (!dict.TryGetValue(zone.level, out var list))
            {
                list = new List<CubeCoord>();
                dict[zone.level] = list;
            }

            // Aquí ocurre la conversión DTO → modelo de dominio
            foreach (var dto in zone.tiles)
                list.Add(dto.ToModel());
        }

        // Opcional: imprime para verificar
        DebugPrintZones("Attacker Zones", _attackerZones);
        DebugPrintZones("Defender Zones", _defenderZones);
    }

    public void PaintDeploymentZone(List<CubeCoord> deploymentCoords, Color ArmyColor)
    {
        bfController.PaintManyTiles(deploymentCoords, ETileHighlightType.DeployZone);
    }

}