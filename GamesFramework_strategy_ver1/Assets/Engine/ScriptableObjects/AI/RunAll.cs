[System.Serializable]
[UnityEngine.CreateAssetMenu(fileName = "New RunAll", menuName = "Framework/AI/New RunAll", order = 1)]
public class RunAll : SOTreeNode {
    public override void Execute() {
        for (int i = 0; i < nodes.Count; i++) {
            nodes[i].StandardNodeExecute();
        }
    }
}