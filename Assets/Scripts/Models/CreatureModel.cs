// CreatureModel.cs
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CreatureModel
{
    public string Name { get; }
    public IReadOnlyList<CubeCoord> Shape { get; }
    public Sprite Sprite { get; }

    public CreatureModel(string newName, Sprite newSpriteModel, IEnumerable<CubeCoord> shape = null)
    {
        Name = newName;
        Sprite = newSpriteModel;
        var defaultShape = new[] { new CubeCoord(0, 0, 0) };
        Shape = (shape == null ? defaultShape : shape.ToArray()).ToList().AsReadOnly();

        if (!Shape.Contains(new CubeCoord(0, 0, 0)))
            throw new ArgumentException("Shape must include the origin (0,0,0)");
    }
}
