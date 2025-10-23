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
    SEMI,
    RCURLY,
    LCURLY,

    // FUNCTIONAL KEYWORDS
    VAR,
    FUNCTION,
    IF,
    ID
}

public class Token
{
    public TokenType token;
    public dynamic value; // trust me bro 

    Token(TokenType token, dynamic value = null){ this.token = token; this.value = value; }
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

