[System.Serializable]
public abstract class SOParentNode :SOTree {
    public SOTree child;
}

one options is to make all these nodes non scriptable object, and then have a NODE SO plus an editor for
    every node type, or a general reflection based editor. every SO node can become any node.
        Can be used for story?