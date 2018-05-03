[UnityEngine.CreateAssetMenu(fileName = "Sequence_", menuName = "Framework/AI/New Sequence", order = 1)]
public class Sequence : SOTreeNode {
    // running in range returns true, but since simple firing is called on all tags children, it's triggered on all fighters, every sequence
    // solution: make tags run all only once, with diff results for all(dict for list nodes)
    // solution 2(taken): run only tags that are under some root.(compare root)
    public override NodeResult Execute() {
        if (nodes.Count == 0)
            return NodeResult.None;
        for (int i = 0; i < nodes.Count; i++) {
            NodeResult r =  nodes[i].StandardNodeExecute();
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
