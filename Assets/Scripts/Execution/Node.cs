using System.Collections.Generic;

namespace AppleCore.Node
{
    public enum NodeType
    {
        Call,
        Assign,
        // StringVar,
        // IntVar,
        Literal,
        IfStmt,
        Var
    }


    public abstract class Node
    {
        public NodeType type { get; }

        public Node(NodeType type)
        {
            this.type = type;
        }

    }

    public class IfStmt : Node
    {
        public Node condition;
        public List<Node> body;

        public IfStmt(Node condition, List<Node> body) : base(NodeType.IfStmt)
        {
            this.condition = condition;
            this.body = body;
        }

         public override string ToString()
        {
            return $"Node({this.condition.ToString()}, args = {body})";
        }
    }


    public class Call : Node
    {
        public string identifier;
        public List<Node> args;
        public Call(string identifier, List<Node> args) : base(NodeType.Call) { this.identifier = identifier; this.args = args; }

        public override string ToString()
        {
            return $"Node({this.type}, call_id: {this.identifier?.ToString() ?? "null"}, args = {args.ToString()})";
        }
    }

    // public class StringVar : Node
    // {
    //     public string identifier;
    //     public string data;

    //     StringVar(string identifier, string data) : base(NodeType.StringVar)
    //     {
    //         this.identifier = identifier;
    //         this.data = data;
    //     }
    // }

    // public class IntVar : Node
    // {
    //     public string identifier;
    //     public string data;

    //     IntVar(string identifier, string data) : base(NodeType.IntVar)
    //     {
    //         this.identifier = identifier;
    //         this.data = data;
    //     }
    // }

    public class Var : Node
    {
        public string identifier;
        public Var(string identifier) : base(NodeType.Var)
        {
            this.identifier = identifier;
        }
    }

    public abstract class Literal<T> : Node
    {
        public T value;
        protected Literal(T value) : base(NodeType.Literal)
        {
            this.value = value;
        }
    }

    public class IntLit : Literal<int>
    {
        public IntLit(int value) : base(value) { }
    }

    public class StrLit : Literal<string>
    {   
        public StrLit(string value) : base(value){}
    }
    
    public class BoolLit : Literal<bool>
    {   
        public BoolLit(bool value) : base(value){}
    }


    public class Assign : Node
    {
        public string identifier;
        public int data;

        Assign(string identifier, int data) : base(NodeType.Assign)
        {
            this.identifier = identifier;
            this.data = data;

        }
    }
}