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
    COMMA,

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
    public TokenType type;
    public object value; // trust me bro 

    public Token(TokenType token, object value = null) { this.type = token; this.value = value; }

    public override string ToString()
    {
        return  $"Token({this.token}, Value: {this.value?.ToString() ?? "null"})";
    }
}

