using System.Collections.Generic;

public interface ISOTagNode {
    string tag { get; set; }
}

public abstract class SOTreeNode : SOTree {
    public List<SOTree> nodes;
}
