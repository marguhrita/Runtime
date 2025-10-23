using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;

public enum TokenType
{
    // Literal Types
    STRING,
    NUMBER,

    // Syntax things
    LPAR, //(
    RPAR, //)
    EOL,
    RCURLY,
    LCURLY,
ASSIGN,

    // FUNCTIONAL KEYWORDS
    VAR,
    FUNCTION,
    IF,
    ID
}


public class Lexer
{
    private Scanner scanner;
    private StringBuilder c = new StringBuilder();
    public List<TokenType> tokenize(string code)
    {
        List<TokenType> tokens;
        scanner = Scanner(code);
        c = scanner.peek();
        while (c)
        {

            if (c == "(")
            {
                tokens.Add(TokenType.LPAR);
                scanner.consume();
            }
            else if (c == ")")
            {
                tokens.Add(TokenType.RPAR);
                scanner.consume();
            }
            else if (c == "{")
            {
                tokens.Add(TokenType.LCURLY);
                scanner.consume();

            }
            else if (c == "}")
            {
                tokens.Add(TokenType.RCURLY);
                scanner.consume();
            }
            else if (c == ";")
            {
                tokens.Add(TokenType.EOL);
                scanner.consume();
            }
            else if (c == "=")
            {
                tokens.Add(TokenKind.ASSIGN);
                scanner.consume();
            }

            else if (scanner.alphanumeric.Contains(c))
            {
                string name = scanner.peekString().ToLower();
                if (name == "variable")
                {
                    tokens.Add(TokenKind.VAR, "variable");
                    scanner.consume();
                }
                else if (name == "function")
                {
                    tokens.Add(TokenKind.FUNCTION, "function");
                    scanner.consume();
                }
                else if (name == "if")
                {
                    tokens.Add(TokenKind.IF, "if");
                    scanner.consume();
                }
                else
                {
                    tokens.Add(TokenKind.identifier, namw);
                    scanner.consume();
                }
            }



            // String
            else if (c == '"')
            {
                scanner.consume();  // Eat the quote
                string s = scanner.peekString(); // returns up to the end of the string (not including ending quote)
                scanner.consume(s.Length + 1); // consume the string from buffer, plus the ending quote
                tokens.Add(TokenType.STRING, s);
            }
            
            // Incrememnt C
            c = scanner.peek();
        }
    }

    
}