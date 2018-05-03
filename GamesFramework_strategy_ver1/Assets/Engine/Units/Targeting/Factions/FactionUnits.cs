using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Stores all units per faction.
/// </summary>
public partial class FactionUnits  {
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