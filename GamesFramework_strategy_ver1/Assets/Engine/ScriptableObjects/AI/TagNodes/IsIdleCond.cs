using System;
[System.Serializable]
[UnityEngine.CreateAssetMenu(fileName = "New IsIdleCond", menuName = "Framework/AI/New IsIdleCond", order = 1)]
public class IsIdleCond : SOParentNode ,ISOTagNode {
    public string _tag;
    public string tag {
        get {
            return _tag;
        }

        set {
            _tag = value;
        }
    }
    public string tool = "[Obsolete]DO NOT USE THIS NODE.";

    public override NodeResult Execute() {
        if ((source as AITargeter).movingData.IsIdle) {
            return child.StandardNodeExecute();
        }
        return NodeResult.Failure;
    }
}
