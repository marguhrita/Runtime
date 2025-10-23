using System.ComponentModel;
using System.Text; 
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

public class Scanner()
{
    int i = 0;
    private StringBuilder buf = new StringBuilder();

    public string peek()
    {
        // Check if string is empty
        if (buf.Length == 0)
        {
            return null;
        }

        // Return next character, and increment i
        i += 1;
        return buf[i - 1];
    }

    public void consume(int n = 1)
    {
        // consumes the first n letters in the buffer, 1 by default
        buf.Remove(0, n);
        i == 0;
    }
}

public class Lexer
{
    private Scanner scanner = new Scanner();
    private StringBuilder c = new StringBuilder();
    public List<TokenType> tokenize(string code)
    {
        List<TokenType> tokens;
        c = scanner.peek();
        while (c)
        {

            if (c == "(")
            {
                tokens.Add(TokenType.LCURLY);
                scanner.consume();
            }

            // Incrememnt C
            c = scanner.peek();
        }
    }

    
}