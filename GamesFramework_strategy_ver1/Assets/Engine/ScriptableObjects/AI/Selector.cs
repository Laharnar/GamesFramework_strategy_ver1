using System;

[UnityEngine.CreateAssetMenu(fileName = "New Selector", menuName = "Framework/AI/New Selector", order = 1)]
public class Selector : SOTreeNode {
    public override NodeResult Execute() {
        for (int i = 0; i < nodes.Count; i++) {
            NodeResult r = nodes[i].Execute();
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
[UnityEngine.CreateAssetMenu(fileName = "New Sequence", menuName = "Framework/AI/New Sequence", order = 1)]
public class Sequence : SOTreeNode {
    public override NodeResult Execute() {
        for (int i = 0; i < nodes.Count; i++) {
            NodeResult r = nodes[i].Execute();
            switch (r) {
                case NodeResult.Success:
                    break;
                case NodeResult.Failure:
                    return NodeResult.Failure;
                case NodeResult.Running:
                    break;
                case NodeResult.None:
                    break;
                default:
                    break;
            }
        }
        return NodeResult.Success;
    }
}
