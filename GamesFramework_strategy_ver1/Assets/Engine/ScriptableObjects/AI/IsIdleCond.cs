using System;
[UnityEngine.CreateAssetMenu(fileName = "New IsIdleCond", menuName = "Framework/AI/New IsIdleCond", order = 1)]
public class IsIdleCond : SOParentNode {
    public override void Execute() {
        if ((source as AITargeter).moving.IsIdle) {
            child.StandardNodeExecute();
        }
    }
}
