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

