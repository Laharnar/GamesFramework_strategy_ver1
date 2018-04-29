[System.Serializable]
[UnityEngine.CreateAssetMenu(fileName = "New RunAll", menuName = "Framework/AI/New RunAll", order = 1)]
public class RunAll : SOTreeNode,ISOTagNode {
    public string _tag;
    public string tag {
        get {
            return _tag;
        }

        set {
            _tag = value;
        }
    }

    public override NodeResult Execute() {
        for (int i = 0; i < nodes.Count; i++) {
            nodes[i].StandardNodeExecute();
        }
        return NodeResult.Success;
    }
}