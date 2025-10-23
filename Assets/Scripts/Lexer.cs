using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;

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
EOF,

    // FUNCTIONAL KEYWORDS
    VAR,
    FUNCTION,
    IF,
    ID
}


public class Lexer
{
    private Scanner scanner;
    private char c;
    public List<TokenType> tokenize(string code)
    {
        List<TokenType> tokens = new List<TokenType>();
        scanner = new Scanner(code);
        bool end = false;
        
        if (scanner.Peek() == null)
        {
            // File is empty!
            tokens.Add(TokenType.EOF);
            return tokens;
        }
        c = (char)scanner.Peek();

        while (!end)
        {

            if (c == '(')
            {
                tokens.Add(TokenType.LPAR);
                scanner.Consume();
            }
            else if (c == ')')
            {
                tokens.Add(TokenType.RPAR);
                scanner.Consume();
            }
            else if (c == '{')
            {
                tokens.Add(TokenType.LCURLY);
                scanner.Consume();

            }
            else if (c == '}')
            {
                tokens.Add(TokenType.RCURLY);
                scanner.Consume();
            }
            else if (c == ';')
            {
                tokens.Add(TokenType.EOL);
                scanner.Consume();
            }
            else if (c == '=')
            {
                tokens.Add(TokenType.ASSIGN);
                scanner.Consume();
            }

            else if (scanner.Alphanumeric.Contains(c))
            {
                string name = scanner.peekString().ToLower();
                if (name == "variable")
                {
                    tokens.Add(TokenType.VAR, "variable");
                    scanner.Consume();
                }
                else if (name == "function")
                {
                    tokens.Add(TokenType.FUNCTION, "function");
                    scanner.Consume();
                }
                else if (name == "if")
                {
                    tokens.Add(TokenType.IF, "if");
                    scanner.Consume();
                }
                else
                {
                    tokens.Add(TokenType.identifier, name);
                    scanner.Consume();
                }
            }



            // String
            else if (c == '"')
            {
                scanner.Consume();  // Eat the quote
                string s = scanner.peekString(); // returns up to the end of the string (not including ending quote)
                scanner.Consume(s.Length + 1); // Consume the string from buffer, plus the ending quote
                tokens.Add(TokenType.STRING, s);
            }

            if (scanner.Peek() == null)
            {
            // End of file!
            tokens.Add(TokenType.EOF);
            end = true;
        }

            // Incrememnt C
            c = (char)scanner.Peek();
        }

        return tokens;
    }

    
}