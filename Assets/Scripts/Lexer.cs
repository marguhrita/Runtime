using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;




public class Lexer
{
    private Scanner scanner;
    private char c;
    public List<Token> tokenize(string code)
    {
        List<Token> tokens = new List<Token>();
        scanner = new Scanner(code);
        bool end = false;
        
        if (scanner.Peek() == null)
        {
            // File is empty!
            tokens.Add(new Token(TokenType.EOF));
            return tokens;
        }
        c = (char)scanner.Peek();

        while (!end)
        {

            if (c == '(')
            {
                tokens.Add(new Token(TokenType.LPAR));
                scanner.Consume();
            }
            else if (c == ')')
            {
                tokens.Add(new Token(TokenType.RPAR));
                scanner.Consume();
            }
            else if (c == '{')
            {
                tokens.Add(new Token(TokenType.LCURLY));
                scanner.Consume();

            }
            else if (c == '}')
            {
                tokens.Add(new Token(TokenType.RCURLY));
                scanner.Consume();
            }
            else if (c == ';')
            {
                tokens.Add(new Token(TokenType.EOL));
                scanner.Consume();
            }
            else if (c == '=')
            {
                tokens.Add(new Token(TokenType.ASSIGN));
                scanner.Consume();
            }

            else if (scanner.Alphanumeric.Contains(c))
            {
                string name = scanner.peekString().ToLower();
                if (name == "variable")
                {
                    tokens.Add(new Token(TokenType.VAR, "variable"));
                    scanner.Consume();
                }
                else if (name == "function")
                {
                    tokens.Add(new Token(TokenType.FUNCTION, "function"));
                    scanner.Consume();
                }
                else if (name == "if")
                {
                    tokens.Add(new Token(TokenType.IF, "if"));
                    scanner.Consume();
                }
                else
                {
                    tokens.Add(new Token(TokenType.identifier, name));
                    scanner.Consume();
                }
            }



            // String
            else if (c == '"')
            {
                scanner.Consume();  // Eat the quote
                string s = scanner.peekString(); // returns up to the end of the string (not including ending quote)
                scanner.Consume(s.Length + 1); // Consume the string from buffer, plus the ending quote
                tokens.Add(new Token(TokenType.STRING, s));
            }

            if (scanner.Peek() == null)
            {
            // End of file!
            tokens.Add(new Token(TokenType.EOF));
            end = true;
        }

            // Incrememnt C
            c = (char)scanner.Peek();
        }

        return tokens;
    }

    
}