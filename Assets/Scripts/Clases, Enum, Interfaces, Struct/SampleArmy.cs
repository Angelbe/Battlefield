using System;
using System.Collections.Generic;
using System.Drawing;

public static class CreatureShapes
{
    public static readonly CubeCoord[] Single =
        { new CubeCoord(0,0,0) };

    public static readonly CubeCoord[] Line =
    {
        new CubeCoord(0,0,0),
        new CubeCoord(0,1,-1)
    };
}

public static class SampleChampion
{
    public static ChampionModel ChampionOne = new("championNameOne");
    public static ChampionModel ChampionTwo = new("championNameOne", EDeploymentLevel.Advanced);
}

public static class CreatureModelLibrary
{
    public static CreatureModel Llum = new("Llum", CreatureShapes.Single, 5, 10, 20, EAttackType.melee, 10, 20, 0, 0, 5, 10, EMovementType.Walk, 1);
    public static CreatureModel Bandit = new("Bandit", CreatureShapes.Single, 7, 10, 15, EAttackType.melee, 10, 20, 0, 0, 10, 10, EMovementType.Walk, 1);
    public static CreatureModel Knight = new("Knight", CreatureShapes.Single, 20, 5, 10, EAttackType.melee, 10, 20, 0, 10, 5, 8, EMovementType.Walk, 1);
    public static CreatureModel Mage = new("Mage", CreatureShapes.Single, 20, 5, 10, EAttackType.melee, 10, 20, 0, 10, 5, 8, EMovementType.Walk, 1);
    public static CreatureModel Fighter = new("fighter", CreatureShapes.Single, 20, 5, 10, EAttackType.melee, 10, 20, 0, 10, 5, 8, EMovementType.Walk, 1);
    public static CreatureModel Archer = new("Archer", CreatureShapes.Single, 20, 5, 10, EAttackType.melee, 10, 20, 0, 10, 5, 8, EMovementType.Walk, 1);
}

public static class SampleCreatureReserve
{
    public static Dictionary<int, CreatureModel> CreatureReserveOne =
        new()
        {
            { 1, CreatureModelLibrary.Llum },
            { 2, CreatureModelLibrary.Archer },
            { 3, CreatureModelLibrary.Fighter }
        };

    public static Dictionary<int, CreatureModel> CreatureReserveTwo =
        new()
        {
            { 1, CreatureModelLibrary.Mage },
            { 2, CreatureModelLibrary.Knight }
        };
}


public static class SampleArmy
{
    public static Army ArmyOne = new Army("championArmyNameOne", Color.Red, SampleCreatureReserve.CreatureReserveOne, SampleChampion.ChampionOne);
    public static Army ArmyTwo = new Army("championArmyNameTwo", Color.Blue, SampleCreatureReserve.CreatureReserveTwo, SampleChampion.ChampionTwo);
}