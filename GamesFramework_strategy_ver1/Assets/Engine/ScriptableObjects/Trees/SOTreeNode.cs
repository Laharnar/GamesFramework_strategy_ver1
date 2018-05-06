using System.Collections.Generic;

public abstract class SOTreeNode : SOTree {
    public List<SOTree> nodes;

    internal void Add(SOTree node) {
        if (nodes == null) {
            nodes = new List<SOTree>();
        }
        nodes.Add(node);
    }
}
