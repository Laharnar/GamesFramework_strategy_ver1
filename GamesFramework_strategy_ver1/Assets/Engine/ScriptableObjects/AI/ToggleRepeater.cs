using UnityEngine;

[CreateAssetMenu(fileName = "New ToggleRepeater", menuName = "Framework/AI/New ToggleRepeater", order = 1)]
public class ToggleRepeater : SOTreeNode {
    //int active = 0;
    public override NodeResult Execute() {
        if (source != null && !actives.ContainsKey((AITargeter)source))
            actives.Add((AITargeter)source, 0);

        int active = actives[(AITargeter)source];
        active = (active + 1) % nodes.Count;
        actives[(AITargeter)source] = active;
        return nodes[active].StandardNodeExecute();
    }

    // source/active int
    public static System.Collections.Generic.Dictionary<AITargeter, int> actives = new System.Collections.Generic.Dictionary<AITargeter, int>();

}


