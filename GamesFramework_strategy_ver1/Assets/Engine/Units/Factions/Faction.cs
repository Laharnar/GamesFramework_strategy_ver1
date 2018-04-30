using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Standard faction access, one in scene per faction.
/// </summary>
public partial class Faction :MonoBehaviour{
    public StringData factionName;
    public int money;
    private void Start() {
        Faction.Register(this);
    }

}

// Global faction data access
public partial class Faction {
    static Dictionary<StringData, Faction> factions = new Dictionary<StringData, Faction>();
    public static int GetMoney(StringData factionName) {
        return ((Faction)factions[factionName]).money;
    }

    private static void Register(Faction faction) {
        if (faction.factionName == null) {
            Debug.LogError("Null value err.", faction.gameObject);
        }
        factions.Add(faction.factionName, faction);
    }

    internal static void UseMoney(StringData factionName, int cost) {
        ((Faction)factions[factionName]).money -= cost;
    }
}