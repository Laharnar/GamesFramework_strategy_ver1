using System;

[UnityEngine.CreateAssetMenu(fileName = "New Selector", menuName = "Framework/AI/New Selector", order = 1)]
public class Selector : SOTreeNode {
    public override NodeResult Execute() {
        for (int i = 0; i < nodes.Count; i++) {
            NodeResult r = nodes[i].StandardNodeExecute();
            switch (r) {
                case NodeResult.Success:
                    return NodeResult.Success;
                case NodeResult.Failure:
                    break;
                case NodeResult.Running:
                    break;
                case NodeResult.None:
                    break;
                default:
                    break;
            }
        }
        return NodeResult.Failure;
    }

}
