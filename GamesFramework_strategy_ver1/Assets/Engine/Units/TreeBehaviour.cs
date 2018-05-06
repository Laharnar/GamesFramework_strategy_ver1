using System;

// Ai root.
public partial class TreeBehaviour : AITargeter {
    public SOTree tree;

    private void Start() {
        AITargeter[] t = gameObject.GetComponentsInChildren<AITargeter>();
        foreach (var item in t) {
            if (item == this)
                continue;
            AddToLib(item);
            GlobalAiData.AddToLib(item.tagTarget, this);
        }
    }

    private void Update() {
        SOTree.source = this;
        if (tree)
            tree.StandardNodeExecute();
    }

}
