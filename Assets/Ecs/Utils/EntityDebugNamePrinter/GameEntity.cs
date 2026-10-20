#if UNITY_EDITOR

using System;
using System.Linq;
using Ecs.Utils.EntityDebugNamePrinter;
using JCMG.EntitasRedux;
using UnityEngine;

// ReSharper disable once CheckNamespace
public sealed partial class GameEntity
{
    private EntityPrinter _printer;

    public override string ToString()
    {
        // Более показать не могу
        return default;
    }

    public static string EntityName(IComponent[] components, int id)
    {
        // Более показать не могу
        return default;
    }

    public string BaseToString() {
        // Более показать не могу
        return default;
    }
}

#endif