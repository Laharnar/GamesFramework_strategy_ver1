[System.Serializable]
public abstract class SOParentNode :SOTree {
    public SOTree child;
}

/*one options is to make all these nodes non scriptable object, and then have a NODE SO plus an editor for
    every node type, or a general reflection based editor for any class. every SO node can become any node.
        Can be used for story?

Another is to separate nodes and so, by having a general non SO "event+data" node
that can be copied freely
So - non SO serialization solution
function calls.
vars.

    */