using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class TreePointer {
    object data;
    Action evt;

    List<TreePointer> children;
}
[System.Serializable]
public abstract class SOTree {
    public static object source;
    public abstract void Execute();

    public static T DeepCopy<T>(T other) where T : SOTree {
        using (MemoryStream ms = new MemoryStream()) {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(ms, other);
            ms.Position = 0;
            return (T)formatter.Deserialize(ms);
        }
    }

    public static T DeepCopyTree<T>(T other) where T : SOTree {
        if (other as SOTreeNode != null) {
            // creates copies of children, then copies node, and reconnects new children to new node.
            SOTreeNode o = other as SOTreeNode;
            List<SOTree> newChildren = new List<SOTree>();
            for (int i = 0; i < o.nodes.Count; i++) {
                newChildren.Add(DeepCopyTree(o.nodes[i]));
            }
            o = DeepCopy(o);
            o.nodes.Clear();
            for (int i = 0; i < newChildren.Count; i++) {
                o.nodes.Add(newChildren[i]);
            }
            return o as T;
        }
        if (other as SOParentNode != null) {
            SOParentNode o = other as SOParentNode;
            SOTree oc = DeepCopyTree(o.child);
            o = DeepCopy(o);
            o.child = oc;
            return o as T;
        }
        if (other as SOTreeLeaf != null) {
            return DeepCopy(other) as T;
        }
        return null;
    }

}