using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Standard faction access, OPTIONAL one in scene per faction.
/// 
/// Needed for global faction stuff(player) like money.
/// </summary>
public partial class Faction :MonoBehaviour{
    public StringData factionName;
    public int money;
    private void Start() {
        RegisterFaction(this);
    }
    private void OnDestroy() {
        UnregisterFaction(this);
    }
}

/// <summary>
/// Stores global faction resources data.
/// </summary>
public partial class Faction {
    static Dictionary<StringData, Faction> factions = new Dictionary<StringData, Faction>();

    private static void RegisterFaction(Faction faction) {
        if (faction.factionName == null) {
            Debug.LogError("Null value err.", faction.gameObject);
        }
        factions.Add(faction.factionName, faction);
    }

    private static void UnregisterFaction(Faction factionAccess) {
        factions.Remove(factionAccess.factionName);
    }

    public static int GetMoney(AITargeter source) {
        return ((Faction)factions[source.stats.faction]).money;
    }

    internal static void UseMoney(AITargeter source, int cost) {
        ((Faction)factions[source.stats.faction]).money -= cost;
    }
}
