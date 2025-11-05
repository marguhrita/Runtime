using System.Collections.Generic;
using System.Data.Common;

public enum TokenType
{
    // Literal Types
    STRING,
    NUMBER,

    // Syntax things
    LPAR, //(
    RPAR, //)
    EOL, // ;
    RCURLY,
    LCURLY,
    ASSIGN,
    EOF,

    // FUNCTIONAL KEYWORDS
    VAR,
    FUNCTION,
    IF,
    ID,

    // Math
    MINUS
}

public class Token
{
    public TokenType token;
    public object value; // trust me bro 

    public Token(TokenType token, object value = null) { this.token = token; this.value = value; }

    public override string ToString()
    {
        return  $"Token({this.token}, Value: {this.value?.ToString() ?? "null"})";
    }
}

public enum NodeType
{
    Call,
    Assign,
    StringVar,
    IntVar
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
    public List<string> args;
    Call(string identifier, List<string> args) : base(NodeType.Call) { this.identifier = identifier; this.args = args; }
}

public class StringVar : Node
{
    public string identifier;
    public string data;

    StringVar(string identifier, string data) : base(NodeType.StringVar)
    {
        this.identifier = identifier;
        this.data = data;
    }
}

public class IntVar : Node
{
    public string identifier;
    public string data;

    IntVar(string identifier, string data) : base(NodeType.IntVar)
    {
        this.identifier = identifier;
        this.data = data;
    }
}

