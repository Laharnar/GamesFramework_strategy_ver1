using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New ToggleRepeater", menuName = "Framework/AI/New ToggleRepeater", order = 1)]
public class ToggleRepeater : SOTreeNode {
    int active = 0;
    public override void Execute() {
        active = (active + 1) % nodes.Count;
        nodes[active].StandardNodeExecute();
    }
}
