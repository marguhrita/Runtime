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

    //Unary
    NEGATE,
    BANG, // logical not

    // Math
    MINUS
}

public class Token
{
    public TokenType type;
    public object value; // trust me bro 

    public Token(TokenType type, object value = null) { this.type = type; this.value = value; }

    public override string ToString()
    {
        return  $"Token({this.type}, Value: {this.value?.ToString() ?? "null"})";
    }
}

