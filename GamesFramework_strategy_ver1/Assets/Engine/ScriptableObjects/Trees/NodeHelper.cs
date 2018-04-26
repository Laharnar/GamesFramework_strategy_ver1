public static class NodeHelper {
    public static void StandardNodeExecute(this SOTree node) {
        ISOTagNode istn = node as ISOTagNode;
        if (istn != null)
            TagExecute(node, istn);
        else 
            node.Execute();
    }

    public static void TagExecute(SOTree node, object ISOtagNode) {
        // Execute node n times on every source, with a copy of a node.
        // Performance: Create copy of the tree only once, then replace that node with reference node to the library which contains 1 subtree for every targeter.
        if (ISOtagNode as ISOTagNode != null) {
            AITargeter[] sources = UnitAI.GetAllTargetersByTag(((ISOTagNode)ISOtagNode).tag);
            for (int i = 0; i < sources.Length; i++) {
                SOTree.source = sources[i];
                SOTree.DeepCopyTree(node).Execute();
            }
        }
    }
}
