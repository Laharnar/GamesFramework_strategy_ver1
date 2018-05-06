using System;

public static class NodeHelper {
    public static NodeResult StandardNodeExecute(this SOTree node) {
        if (node == null) {
            UnityEngine.Debug.Log("Empty node slot in the tree");
            return NodeResult.None;
        }
        if (!node.isActive) {
            return NodeResult.Disabled;
        }
        ISOTagNode istn = node as ISOTagNode;
        if (istn != null && istn.tag != "") {
            //UnityEngine.Debug.Log(node.GetType());
            TagExecute(node, istn);
            return NodeResult.None;
        } else
            return node.Execute();
        //return NodeResult.Success;
    }

    public static NodeResult TagExecute(SOTree node, object ISOtagNode) {
        TreeBehaviour source = SOTree.source as TreeBehaviour;
        if (source==null) {
            UnityEngine.Debug.LogError("A tagged node under tagged node is not supported, because tagged nodes assign non-TreeBehaviour sources." +
                "Sources can currently be accessed only by root, not by child AiTargetrs.");
            return NodeResult.None;
        }
        // Execute node n times on every source, with a copy of a node.
        // Performance: Create copy of the tree only once, then replace that node with reference node to the library which contains 1 subtree for every targeter.
        AITargeter[] sources = GlobalAiData.GetAiSourcesByTagUnderRoot(source, ((ISOTagNode)ISOtagNode).tag);
        if (sources.Length == 0)
            UnityEngine.Debug.Log("No sources with tag("+ ((ISOTagNode)ISOtagNode).tag);


        for (int i = 0; i < sources.Length; i++) {
            SOTree.source = sources[i];
            node.Execute();
        }
        SOTree.source = source;
        return NodeResult.None;
    }
}
