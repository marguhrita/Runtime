using System.Data.Common;

public enum NodeType
{
    Variable,
    Function,
    Conditional,
Call

}

public abstract class Node
{
    public NodeType type { get; }

    public Node(NodeType type)
    {
        this.type = type;
    }
}


public class Call : Node
{
    public string identifier;
    Call(string identifier) : base(NodeType.Call) { this.identifier = identifier; }
}