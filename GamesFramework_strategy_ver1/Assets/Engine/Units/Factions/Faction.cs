using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Standard faction access, one in scene per faction.
/// 
/// Needed for global faction stuff(player) like money.
/// </summary>
public partial class Faction :MonoBehaviour{
    public StringData factionName;
    public int money;
    private void Start() {
        Faction.Register(this);
    }
    private void OnDestroy() {
        Faction.UnregisterFaction(this);
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

    internal static void UnregisterFaction(Faction factionAccess) {
        factions.Remove(factionAccess.factionName);
    }
}
public partial class Faction  {
    static Dictionary<StringData, List<FactionAccess>> factionUnits = new Dictionary<StringData, List<FactionAccess>>();

    public static void RegisterUnit(FactionAccess unit) {
        if (unit.faction == null) {
            Debug.LogError("Null value err.", unit.gameObject);
        }
        if (!factionUnits.ContainsKey(unit.faction)) {
            factionUnits.Add(unit.faction, new List<FactionAccess>());
        }
        factionUnits[unit.faction].Add(unit);
    }

    internal static AITargeter FindClosestEnemy(AITargeter aiTargeter) {
        FactionAccess fa = FindClosestEnemy(aiTargeter.transform.position, aiTargeter.stats.faction);
        if (fa != null)
            return fa.GetComponent<AITargeter>();
        return null;
    }
    internal static FactionAccess FindClosestEnemy(Vector3 pt, StringData faction) {
        FactionAccess ntarget = null;
        foreach (var item in factionUnits) {
            if (item.Key.s == faction.s)
                continue;
            float minDist = float.MaxValue;
            for (int i = 0; i < item.Value.Count; i++) {
                float dist = Vector3.Distance(item.Value[i].transform.position, pt);
                if (dist < minDist) {
                    minDist = dist;
                    ntarget = item.Value[i];
                }
            }
        }
        return ntarget;
    }


    internal static bool NoEnemies(FactionAccess source) {
        foreach (var item in factionUnits) {
            if (item.Key.s == source.faction.s)
                continue;
            return false;
        }
        return true;
    }

    internal static void UnregisterUnit(FactionAccess factionAccess) {
        factionUnits[factionAccess.faction].Remove(factionAccess);
        if (factionUnits[factionAccess.faction].Count == 0) {
            factionUnits.Remove(factionAccess.faction);
        }
    }
}