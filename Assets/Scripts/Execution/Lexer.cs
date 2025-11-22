using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine;

public class Lexer
{
    private Scanner scanner;
    private char c;

    public Lexer() { }
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
            //Debug.Log(iters);
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
            // CHeck for numerical values
            else if (char.IsDigit(c))
            {
                float num = scanner.peekFloat();
                tokens.Add(new Token(TokenType.NUMBER, num));
            }
            //math
else if (c == '-')
            {
                tokens.Add(new Token(TokenType.MINUS));
                scanner.Consume();
            }

            else if (scanner.Alphanumeric.Contains(c))
            {

                string name = scanner.peekWord().ToLower();
                if (name == "variable")
                {
                    tokens.Add(new Token(TokenType.VAR, "variable"));
                }
                else if (name == "function")
                {
                    tokens.Add(new Token(TokenType.FUNCTION, "function"));
                }
                else if (name == "if")
                {
                    tokens.Add(new Token(TokenType.IF, "if"));
                }
                else
                {
                    tokens.Add(new Token(TokenType.ID, name));
                }

            }
            // String
            else if (c == '"')
            {
                scanner.Consume();  // Eat the quote
                string s = scanner.peekString(); // returns up to the end of the string (not including ending quote)
                scanner.Consume(); // Eat the other quote
                tokens.Add(new Token(TokenType.STRING, s));
            }
            //spaces
            else if (char.IsWhiteSpace(c))
            {
                scanner.Consume(); // Do nothing for spaces
            }
            else
            {
                Debug.Log("Unmatched Item: " + c);
                //scanner.Consume();
            }

            if (scanner.Peek() == null)
            {
            // End of file!
            tokens.Add(new Token(TokenType.EOF));
            end = true;
            }
            else
            {
                // Incrememnt C
                c = (char)scanner.Peek();
            }

            
        }

        return tokens;
    }

    
}