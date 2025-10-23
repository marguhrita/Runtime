using System;
using System.Text;
using NUnit.Framework.Constraints;

public class Scanner
{
    private StringBuilder Buf;
    public string Alphanumeric { get; private set; } = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    Scanner(string code)
    {
        this.Buf = new StringBuilder(code);
    }

    public char? Peek()
    {
        // Check if string is empty
        if (Buf.Length == 0)
        {
            return null;
        }

        // Return next character
        return Buf[0];
    }

    public void Consume(int n = 1)
    {
        // consumes the first n letters in the buffer, 1 by default
        Buf.Remove(0, n);
    }

    public string peekString()
    {
        StringBuilder s = new StringBuilder("");

        if (Peek() is not char)
        {
            return null;
        }
        char c = (char)Peek();
        while (Alphanumeric.Contains(c))
        {
            s.Append(c);
            Consume();

            if (Peek() is not char)
            {
                break;
            }
            c = (char)Peek();
        }

        return s.ToString();
    }


}
